using System;

namespace JinnoVision.Services.Camera
{
    public interface ICameraService : IDisposable
    {
        event EventHandler<CameraFrameEventArgs> FrameReceived;

        bool InitializeAndOpenFirstCamera();
        bool Start(IntPtr displayHandle = default);
        void Stop();
        void Close();
    }
}