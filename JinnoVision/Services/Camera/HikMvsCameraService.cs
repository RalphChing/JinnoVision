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

        public bool Start(IntPtr displayHandle)
        {
            if (!_isOpen)
                return false;

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

        public void Dispose()
        {
            Close();
        }
    }
}