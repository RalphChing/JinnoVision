using System;

namespace JinnoVision.Services.Cobot
{
    public interface ICobotService : IDisposable
    {
        event Action ProgramStartRequested;
        event Action NextStepRequested;

        bool IsConnected { get; }

        void Connect(string ip, int port);
        void StartListening();
        void StopListening();

        void SetBusy();
        void SetPass();
        void SetFail();
        void ResetSignals();
    }
}