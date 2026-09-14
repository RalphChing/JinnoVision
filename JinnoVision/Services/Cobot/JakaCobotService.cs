using EasyModbus;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace JinnoVision.Services.Cobot
{
    public class JakaCobotService : ICobotService
    {
        private ModbusClient _modbus;
        private Timer _pollTimer;

        private bool _lastProgramStartRequest;
        private bool _lastNextStepRequest;

        private bool _isProcessingProgramStart;
        private bool _isProcessingNextStep;

        public event Action ProgramStartRequested;
        public event Action NextStepRequested;

        public bool IsConnected =>
            _modbus != null && _modbus.Connected;

        // Robot -> PC discrete inputs
        private const int ProgramStartInput = 8;
        private const int NextStepInput = 9;

        // PC -> Robot coils
        private const int CoilBusy = 1;
        private const int CoilPass = 2;
        private const int CoilFail = 3;

        public void Connect(string ip, int port)
        {
            _modbus = new ModbusClient(ip, port);
            _modbus.UnitIdentifier = 1;
            _modbus.ConnectionTimeout = 3000;

            _modbus.Connect();

            ResetSignals();
        }

        public void StartListening()
        {
            if (!IsConnected)
                throw new InvalidOperationException("JAKA cobot is not connected.");

            if (_pollTimer != null)
                return;

            _pollTimer = new Timer();
            _pollTimer.Interval = 100;
            _pollTimer.Tick += PollTimer_Tick;
            _pollTimer.Start();
        }

        public void StopListening()
        {
            if (_pollTimer != null)
            {
                _pollTimer.Stop();
                _pollTimer.Tick -= PollTimer_Tick;
                _pollTimer.Dispose();
                _pollTimer = null;
            }
        }

        private void PollTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                bool programStart = ReadInput(ProgramStartInput);
                bool nextStep = ReadInput(NextStepInput);

                if (programStart && !_lastProgramStartRequest && !_isProcessingProgramStart)
                {
                    _isProcessingProgramStart = true;

                    Debug.WriteLine("JAKA Program Start signal received");

                    ProgramStartRequested?.Invoke();
                }

                if (nextStep && !_lastNextStepRequest && !_isProcessingNextStep)
                {
                    _isProcessingNextStep = true;

                    Debug.WriteLine("JAKA Next Step signal received");

                    NextStepRequested?.Invoke();
                }

                _lastProgramStartRequest = programStart;
                _lastNextStepRequest = nextStep;

                if (!programStart)
                    _isProcessingProgramStart = false;

                if (!nextStep)
                    _isProcessingNextStep = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("JAKA polling failed: " + ex.Message);
            }
        }

        private bool ReadInput(int inputAddress)
        {
            bool[] inputs = _modbus.ReadDiscreteInputs(inputAddress, 1);
            return inputs.Length > 0 && inputs[0];
        }

        public void SetBusy()
        {
            if (!IsConnected) return;

            _modbus.WriteSingleCoil(CoilBusy, true);
            _modbus.WriteSingleCoil(CoilPass, false);
            _modbus.WriteSingleCoil(CoilFail, false);
        }

        public void SetPass()
        {
            if (!IsConnected) return;

            _modbus.WriteSingleCoil(CoilBusy, false);
            _modbus.WriteSingleCoil(CoilPass, true);
            _modbus.WriteSingleCoil(CoilFail, false);
        }

        public void SetFail()
        {
            if (!IsConnected) return;

            _modbus.WriteSingleCoil(CoilBusy, false);
            _modbus.WriteSingleCoil(CoilPass, false);
            _modbus.WriteSingleCoil(CoilFail, true);
        }

        public void ResetSignals()
        {
            if (!IsConnected) return;

            _modbus.WriteSingleCoil(CoilBusy, false);
            _modbus.WriteSingleCoil(CoilPass, false);
            _modbus.WriteSingleCoil(CoilFail, false);
        }

        public void Dispose()
        {
            StopListening();

            if (_modbus != null)
            {
                if (_modbus.Connected)
                    _modbus.Disconnect();

                _modbus = null;
            }
        }
    }
}