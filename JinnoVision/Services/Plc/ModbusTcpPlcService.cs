using EasyModbus;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace JinnoVision.Services.Plc
{
    public class ModbusTcpPlcService : IPlcService
    {
        private ModbusClient _modbus;
        private Timer _pollTimer;

        private bool _lastStart;
        private bool _lastNext;

        public bool IsConnected =>
            _modbus != null && _modbus.Connected;

        public event Action StartInspectionRequested;
        public event Action NextStepRequested;

        private const int DI_Start = 8192; 
        private const int DI_Next = 8194;  


        public void Connect(string ip, int port)
        {
            _modbus = new ModbusClient(ip, port);

            _modbus.ConnectionTimeout = 3000;
            _modbus.Connect();

        }

        public void StartListening()
        {
            if (!IsConnected)
                throw new InvalidOperationException("PLC not connected.");

            if (_pollTimer != null)
                return;

            _pollTimer = new Timer();
            _pollTimer.Interval = 500;
            _pollTimer.Tick += PollTimer_Tick;
            _pollTimer.Start();
        }

        public void StopListening()
        {
            if (_pollTimer == null)
                return;

            _pollTimer.Stop();
            _pollTimer.Tick -= PollTimer_Tick;
            _pollTimer.Dispose();
            _pollTimer = null;
        }
        
        private void PollTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                bool start = ReadInput(DI_Start);
                bool next = ReadInput(DI_Next);

                if (start && !_lastStart)
                {
                    Debug.WriteLine("PLC Start Trigger");
                    StartInspectionRequested?.Invoke();
                }

                if (next && !_lastNext)
                {
                    Debug.WriteLine("PLC Next Trigger");
                    NextStepRequested?.Invoke();
                }

                _lastStart = start;
                _lastNext = next;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("PLC Poll Error: " + ex.Message);
            }
        }

        private bool ReadInput(int address)
        {
            try
            {
                bool[] values = _modbus.ReadCoils(address, 1);

                return values != null &&
                       values.Length > 0 &&
                       values[0];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"ReadDiscreteInput failed at {address}: {ex.Message}");

                return false;
            }
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