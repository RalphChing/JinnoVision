using JinnoVision.Models.Vision;

namespace JinnoVision.Services.Camera
{
    public interface IJakaLens2DService
    {
        bool IsConnected { get; }

        void Connect(string ip, int port);
        void Disconnect();

        JakaLens2DResult CaptureAndGetResult();
    }
}