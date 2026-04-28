using System;
using System.Drawing;

namespace JinnoVision.Services.Camera
{
    public sealed class CameraFrameEventArgs : EventArgs
    {
        public Bitmap Frame { get; }

        public CameraFrameEventArgs(Bitmap frame)
        {
            Frame = frame;
        }
    }
}