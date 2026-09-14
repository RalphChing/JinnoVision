using System;

namespace JinnoVision.Services.Plc
{
    public interface IPlcService : IDisposable
    {
        bool IsConnected { get; }

        event Action StartInspectionRequested;
        event Action NextStepRequested;

        void Connect(string ip, int port);
        void StartListening();
        void StopListening();
    }
}