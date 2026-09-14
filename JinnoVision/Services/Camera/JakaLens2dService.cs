using JinnoVision.Models.Vision;
using System;
using System.Configuration;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace JinnoVision.Services.Camera
{
    public class JakaLens2DService : IJakaLens2DService
    {
        private TcpClient _client;
        private NetworkStream _stream;

        private string _captureCommand;
        private string _getResultCommand;

        public bool IsConnected =>
            _client != null && _client.Connected;

        public void Connect(string ip, int port)
        {
            Disconnect();

            _captureCommand = ConfigurationManager.AppSettings["JakaLens2D.CaptureCommand"] ?? "CAPTURE";
            _getResultCommand = ConfigurationManager.AppSettings["JakaLens2D.GetResultCommand"] ?? "GET_RESULT";

            _client = new TcpClient();
            _client.ReceiveTimeout = 3000;
            _client.SendTimeout = 3000;

            _client.Connect(ip, port);
            _stream = _client.GetStream();
        }

        public void Disconnect()
        {
            try { _stream?.Close(); } catch { }
            try { _client?.Close(); } catch { }

            _stream = null;
            _client = null;
        }

        public JakaLens2DResult CaptureAndGetResult()
        {
            if (!IsConnected)
                throw new InvalidOperationException("JAKA Lens 2D is not connected.");

            SendCommand(_captureCommand);

            // Small delay for Lens 2D processing.
            Thread.Sleep(300);

            string response = SendCommand(_getResultCommand);

            return ParseResult(response);
        }

        private string SendCommand(string command)
        {
            byte[] data = Encoding.ASCII.GetBytes(command + "\r\n");
            _stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);

            return Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
        }

        private JakaLens2DResult ParseResult(string response)
        {
            // Example expected response:
            // OK,1,123.45,67.89,10.5
            //
            // Format:
            // status,count,x,y,rz

            var result = new JakaLens2DResult
            {
                RawResponse = response
            };

            if (string.IsNullOrWhiteSpace(response))
                return result;

            string[] parts = response.Split(',');

            if (parts.Length < 5)
                return result;

            result.Success = parts[0].Equals("OK", StringComparison.OrdinalIgnoreCase);

            int.TryParse(parts[1], out int count);
            double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double x);
            double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double y);
            double.TryParse(parts[4], NumberStyles.Any, CultureInfo.InvariantCulture, out double rz);

            result.TargetCount = count;
            result.X = x;
            result.Y = y;
            result.Rz = rz;

            return result;
        }
    }
}