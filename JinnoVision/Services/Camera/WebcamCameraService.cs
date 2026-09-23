using AForge.Video;
using AForge.Video.DirectShow;
using JinnoVision.Services.Camera;
using System;
using System.Diagnostics;
using System.Drawing;

namespace JinnoVision.Services.Camera
{
    public sealed class WebcamCameraService : ICameraService
    {
        private FilterInfoCollection _deviceList;
        private VideoCaptureDevice _device;
        private Bitmap _latestFrame;
        private readonly object _frameLock = new object();

        // Throttling / smoothing
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private long _lastDeliverMs = 0;
        private int _targetFps = 25; // adjust for smoother preview (25 - 30 recommended)
        private int _minIntervalMs => Math.Max(1, 1000 / Math.Max(1, _targetFps));

        public event EventHandler<CameraFrameEventArgs> FrameReceived;

        public bool InitializeAndOpenFirstCamera()
        {
            try
            {
                _deviceList = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (_deviceList == null || _deviceList.Count == 0)
                    return false;

                var fi = _deviceList[0];
                _device = new VideoCaptureDevice(fi.MonikerString);

                // Optionally set a lower resolution to reduce processing overhead:
                // choose first capability with reasonable width (if capabilities exist).
                try
                {
                    var caps = _device.VideoCapabilities;
                    if (caps != null && caps.Length > 0)
                    {
                        // Prefer 1280x720 or closest; otherwise keep default.
                        VideoCapabilities best = null;
                        foreach (var c in caps)
                        {
                            if (best == null) best = c;
                            else
                            {
                                // prefer resolutions <= 1280x720 and larger over smaller
                                var bestScore = Math.Abs(best.FrameSize.Width - 1280) + Math.Abs(best.FrameSize.Height - 720);
                                var curScore = Math.Abs(c.FrameSize.Width - 1280) + Math.Abs(c.FrameSize.Height - 720);
                                if (curScore < bestScore)
                                    best = c;
                            }
                        }
                        if (best != null)
                            _device.VideoResolution = best;
                    }
                }
                catch
                {
                    // non-fatal if VideoCapabilities not available
                }

                _device.NewFrame += Device_NewFrame;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Clone once for storage, clone again only when delivering to subscribers.
            Bitmap cloned = null;
            try
            {
                cloned = (Bitmap)eventArgs.Frame.Clone();
            }
            catch
            {
                // If clone fails, just skip this frame
                cloned?.Dispose();
                return;
            }

            // Update latest frame (atomic under lock)
            lock (_frameLock)
            {
                _latestFrame?.Dispose();
                _latestFrame = cloned; // ownership transferred to _latestFrame
            }

            // Throttle delivery to UI/subscribers to target FPS
            long now = _stopwatch.ElapsedMilliseconds;
            if (now - _lastDeliverMs < _minIntervalMs)
            {
                // skip raising event this time — we already updated _latestFrame for CaptureFrame()
                return;
            }

            // Clone once for delivery so subscribers may dispose safely
            Bitmap deliverBmp = null;
            try
            {
                // Quick clone of the stored bitmap for delivery
                lock (_frameLock)
                {
                    if (_latestFrame != null)
                        deliverBmp = (Bitmap)_latestFrame.Clone();
                }

                if (deliverBmp != null)
                {
                    _lastDeliverMs = now;
                    FrameReceived?.Invoke(this, new CameraFrameEventArgs(deliverBmp));
                }
            }
            catch
            {
                deliverBmp?.Dispose();
            }
        }

        public Bitmap CaptureFrame()
        {
            lock (_frameLock)
            {
                if (_latestFrame == null)
                    return null;
                return (Bitmap)_latestFrame.Clone();
            }
        }

        public bool Start(IntPtr displayHandle = default)
        {
            if (_device == null)
                return false;

            if (!_device.IsRunning)
            {
                try
                {
                    _device.Start();
                    // reset throttling timer so first frame displays immediately
                    _lastDeliverMs = 0;
                    _stopwatch.Restart();
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public void Stop()
        {
            if (_device != null && _device.IsRunning)
            {
                try
                {
                    _device.NewFrame -= Device_NewFrame;
                    _device.SignalToStop();
                    _device.WaitForStop();
                }
                catch
                {
                    // best-effort stop for testing
                }
            }
        }

        public void Close()
        {
            Stop();

            lock (_frameLock)
            {
                _latestFrame?.Dispose();
                _latestFrame = null;
            }

            _device = null;
            _deviceList = null;
        }

        public void Dispose()
        {
            Close();
        }
    }
}