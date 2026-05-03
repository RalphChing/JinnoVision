using System;
using System.Drawing;

namespace JinnoVision.Services.Camera
{
    public interface ICameraService : IDisposable
    {
        event EventHandler<CameraFrameEventArgs> FrameReceived;
        Bitmap CaptureFrame();
        bool InitializeAndOpenFirstCamera();
        bool Start(IntPtr displayHandle = default);
        void Stop();
        void Close();
    }
}