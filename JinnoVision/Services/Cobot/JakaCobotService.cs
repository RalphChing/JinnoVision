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

        private bool _lastCaptureRequest;
        private bool _isProcessingTrigger;

        public event Action CaptureRequested;

        public bool IsConnected =>
            _modbus != null && _modbus.Connected;

        // Coil mapping
        private const int TriggerInput = 12; // Robot -> PC
        private const int CoilBusy = 1;           // PC -> Robot
        private const int CoilPass = 2;           // PC -> Robot
        private const int CoilFail = 3;           // PC -> Robot

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
                bool[] inputs = _modbus.ReadDiscreteInputs(TriggerInput, 1);

                bool captureRequest = inputs[0];

                if (captureRequest && !_lastCaptureRequest && !_isProcessingTrigger)
                {
                    _isProcessingTrigger = true;

                    System.Diagnostics.Debug.WriteLine("Robot trigger received");

                    CaptureRequested?.Invoke();
                }

                _lastCaptureRequest = captureRequest;

                // Allow next trigger after robot clears signal
                if (!captureRequest)
                {
                    _isProcessingTrigger = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    "JAKA polling failed: " + ex.Message);
            }
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