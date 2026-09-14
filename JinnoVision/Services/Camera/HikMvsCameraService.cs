using JinnoVision.Services.Camera;
// Adjust this using to match the namespace from your installed MVS C# sample.
// Common examples use MvCamCtrl.NET or a MyCamera wrapper class supplied by Hikrobot.
using MvCamCtrl.NET;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JinnoVision.Services.Camera
{
    public sealed class HikMvsCameraService : ICameraService
    {
        private MyCamera _camera;
        private bool _isOpen;
        private bool _isGrabbing;
        private IntPtr _displayHandle = IntPtr.Zero;
        // Keep a strong reference so GC does not collect the callback delegate.
        private MyCamera.cbOutputExdelegate _imageCallback;

        public event EventHandler<CameraFrameEventArgs> FrameReceived;

        public bool InitializeAndOpenFirstCamera()
        {
            int nRet;
            _camera = new MyCamera();

            MyCamera.MV_CC_DEVICE_INFO_LIST deviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            uint layerType = MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE;

            nRet = MyCamera.MV_CC_EnumDevices_NET(layerType, ref deviceList);
            if (nRet != MyCamera.MV_OK || deviceList.nDeviceNum == 0)
                return false;

            // Use first detected camera for now.
            IntPtr pDeviceInfo = Marshal.ReadIntPtr(deviceList.pDeviceInfo, 0);
            MyCamera.MV_CC_DEVICE_INFO deviceInfo =
                (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(
                    pDeviceInfo,
                    typeof(MyCamera.MV_CC_DEVICE_INFO)
                );
            nRet = _camera.MV_CC_CreateDevice_NET(ref deviceInfo);
            if (nRet != MyCamera.MV_OK)
                return false;

            nRet = _camera.MV_CC_OpenDevice_NET();
            if (nRet != MyCamera.MV_OK)
            {
                _camera.MV_CC_DestroyDevice_NET();
                return false;
            }

            _isOpen = true;

            // Optional but useful for GigE cameras: optimize packet size if supported.
            TryOptimizeGigE();

            // Register callback mode.
            // The MVS guide says callback and polling methods should not be used together.
            _imageCallback = new MyCamera.cbOutputExdelegate(OnImageGrabbed);
            //nRet = _camera.MV_CC_RegisterImageCallBackEx_NET(_imageCallback, IntPtr.Zero);
            if (nRet != MyCamera.MV_OK)
            {
                Close();
                return false;
            }

            return true;
        }
        public Bitmap CaptureFrame()
        {
            if (!_isOpen || _camera == null)
                return null;

            bool wasGrabbing = _isGrabbing;

            if (!wasGrabbing)
            {
                int startRet = _camera.MV_CC_StartGrabbing_NET();
                if (startRet != MyCamera.MV_OK)
                {
                    MessageBox.Show($"Start grabbing failed: 0x{startRet:X}");
                    return null;
                }

                _isGrabbing = true;
            }

            MyCamera.MVCC_INTVALUE payloadSize = new MyCamera.MVCC_INTVALUE();

            int nRet = _camera.MV_CC_GetIntValue_NET("PayloadSize", ref payloadSize);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show($"Get PayloadSize failed: 0x{nRet:X}");
                return null;
            }

            int bufferSize = (int)payloadSize.nCurValue;
            byte[] buffer = new byte[bufferSize];

            MyCamera.MV_FRAME_OUT_INFO_EX frameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            try
            {
                IntPtr pData = handle.AddrOfPinnedObject();

                nRet = _camera.MV_CC_GetOneFrameTimeout_NET(
                    pData,
                    (uint)buffer.Length,
                    ref frameInfo,
                    3000
                );

                if (nRet != MyCamera.MV_OK)
                {
                    MessageBox.Show($"GetOneFrameTimeout failed: 0x{nRet:X}");
                    return null;
                }

                if (frameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    return BuildBitmapFromMono8(
                        pData,
                        frameInfo.nWidth,
                        frameInfo.nHeight);
                }

                if (frameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8)
                {
                    return ConvertToRgbBitmap(
                        pData,
                        frameInfo);
                }

                MessageBox.Show($"Unsupported pixel format: {frameInfo.enPixelType}");
                return null;
            }
            finally
            {
                handle.Free();
            }
        }
        public bool Start(IntPtr displayHandle)
        {
            if (!_isOpen)
                return false;
            _displayHandle = displayHandle;
            int nRet = _camera.MV_CC_StartGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show($"StartGrabbing failed: 0x{nRet:X}");
                return false;
            }

            nRet = _camera.MV_CC_Display_NET(displayHandle);
            if (nRet != MyCamera.MV_OK)
            {
                MessageBox.Show($"Display failed: 0x{nRet:X}");
                return false;
            }

            _isGrabbing = true;
            return true;
        }

        public void Stop()
        {
            if (!_isGrabbing)
                return;

            _camera.MV_CC_StopGrabbing_NET();
            _isGrabbing = false;
        }

        public void Close()
        {
            Stop();

            if (_camera != null)
            {
                if (_isOpen)
                {
                    _camera.MV_CC_CloseDevice_NET();
                    _isOpen = false;
                }

                _camera.MV_CC_DestroyDevice_NET();
                _camera = null;
            }
        }

        private void TryOptimizeGigE()
        {
            try
            {
                int packetSize = _camera.MV_CC_GetOptimalPacketSize_NET();
                if (packetSize > 0)
                {
                    _camera.MV_CC_SetIntValueEx_NET("GevSCPSPacketSize", (uint)packetSize);
                }
            }
            catch
            {
                // Safe to ignore for USB cameras.
            }
        }

        /// <summary>
        /// MVS callback entry point.
        /// Signature may vary slightly by SDK version; use the exact delegate type from your installed sample.
        /// </summary>
        private void OnImageGrabbed(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX frameInfo, IntPtr pUser)
        {
            try
            {
                // First workable version:
                // handle Mono8 and BGR8/RGB8-like formats.
                // Later you can expand this for Bayer or SDK pixel conversion APIs.

                Bitmap bmp = null;

                if (frameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    bmp = BuildBitmapFromMono8(pData, frameInfo.nWidth, frameInfo.nHeight);
                }
                else
                {
                    // Many color cameras deliver Bayer/raw formats by default.
                    // For a first test, either:
                    // 1) switch camera pixel format to Mono8/BGR8 in MVS,
                    // or
                    // 2) later use the SDK pixel conversion API here.
                    return;
                }

                FrameReceived?.Invoke(this, new CameraFrameEventArgs(bmp));
            }
            catch
            {
                // Swallow for first test; add logging later.
            }
        }

        private static Bitmap BuildBitmapFromMono8(IntPtr pData, int width, int height)
        {
            // Convert Mono8 to 24bpp grayscale bitmap for easy PictureBox display.
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.WriteOnly, bmp.PixelFormat);

            try
            {
                int srcStride = width; // Mono8 = 1 byte per pixel
                int dstStride = bmpData.Stride;
                int srcBytes = width * height;

                byte[] mono = new byte[srcBytes];
                Marshal.Copy(pData, mono, 0, srcBytes);

                byte[] rgb = new byte[dstStride * height];

                int srcIndex = 0;
                for (int y = 0; y < height; y++)
                {
                    int dstRow = y * dstStride;
                    for (int x = 0; x < width; x++)
                    {
                        byte v = mono[srcIndex++];
                        int dst = dstRow + x * 3;
                        rgb[dst] = v;
                        rgb[dst + 1] = v;
                        rgb[dst + 2] = v;
                    }
                }

                Marshal.Copy(rgb, 0, bmpData.Scan0, rgb.Length);
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }

            return bmp;
        }
        private Bitmap ConvertToRgbBitmap(
    IntPtr pData,
    MyCamera.MV_FRAME_OUT_INFO_EX frameInfo)
        {
            ushort width = frameInfo.nWidth;
            ushort height = frameInfo.nHeight;

            int rgbBufferSize = width * height * 3;
            byte[] rgbBuffer = new byte[rgbBufferSize];

            GCHandle rgbHandle = GCHandle.Alloc(rgbBuffer, GCHandleType.Pinned);

            try
            {
                MyCamera.MV_PIXEL_CONVERT_PARAM convertParam =
                    new MyCamera.MV_PIXEL_CONVERT_PARAM();

                convertParam.nWidth = width;
                convertParam.nHeight = height;
                convertParam.pSrcData = pData;
                convertParam.nSrcDataLen = frameInfo.nFrameLen;
                convertParam.enSrcPixelType = frameInfo.enPixelType;

                convertParam.pDstBuffer = rgbHandle.AddrOfPinnedObject();
                convertParam.nDstBufferSize = (uint)rgbBufferSize;
                convertParam.enDstPixelType =
                    MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;

                int ret = _camera.MV_CC_ConvertPixelType_NET(ref convertParam);

                if (ret != MyCamera.MV_OK)
                {
                    MessageBox.Show($"ConvertPixelType failed: 0x{ret:X}");
                    return null;
                }

                return BuildBitmapFromRgb24(
                    rgbHandle.AddrOfPinnedObject(),
                    width,
                    height);
            }
            finally
            {
                rgbHandle.Free();
            }
        }
        private static Bitmap BuildBitmapFromRgb24(
    IntPtr pRgbData,
    int width,
    int height)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(
                rect,
                ImageLockMode.WriteOnly,
                bmp.PixelFormat);

            try
            {
                int srcStride = width * 3;
                int dstStride = bmpData.Stride;

                byte[] src = new byte[srcStride * height];
                Marshal.Copy(pRgbData, src, 0, src.Length);

                byte[] dst = new byte[dstStride * height];

                for (int y = 0; y < height; y++)
                {
                    Buffer.BlockCopy(
                        src,
                        y * srcStride,
                        dst,
                        y * dstStride,
                        srcStride);
                }

                Marshal.Copy(dst, 0, bmpData.Scan0, dst.Length);
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }

            return bmp;
        }
        public void Dispose()
        {
            Close();
        }
    }
}