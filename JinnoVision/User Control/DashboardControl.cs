using AForge.Video;
using AForge.Video.DirectShow;
using JinnoVision.App.Services;
using JinnoVision.Models;
using JinnoVision.Services;
using JinnoVision.Services.Camera;
using JinnoVision.Services.Vision;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    public partial class DashboardControl : UserControl
    {
        private ICameraService _cameraService;
        private readonly CodeReaderModule _codeReaderModule;

        public DashboardControl()
        {
            InitializeComponent();

            _codeReaderModule = new CodeReaderModule();

            btnRunCodeRead.Click += BtnRunCodeRead_Click;
        
            btnConnect.Click += BtnConnect_Click;
            btnDisconnect.Click += BtnDisconnect_Click;
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            this.Disposed += DashboardControl_Disposed;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _cameraService?.Dispose();
                _cameraService = new HikMvsCameraService();
                _cameraService.FrameReceived += CameraService_FrameReceived;

                bool ok = _cameraService.InitializeAndOpenFirstCamera();
                lblStatus.Text = ok ? "Camera connected" : "Camera not found / open failed";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Connect error";
                MessageBox.Show(ex.Message, "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _cameraService?.Stop();
                _cameraService?.Close();

                picCamera.Image = null;
                lblStatus.Text = "Disconnected";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Disconnect Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (_cameraService == null)
                return;

            bool ok = _cameraService.Start(picCamera.Handle);
            lblStatus.Text = ok ? "Live View" : "Start failed";
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            _cameraService?.Stop();
            lblStatus.Text = "Stopped";
        }

        private void BtnRunCodeRead_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.bmp;*.png;*.jpg;*.jpeg;*.tif;*.tiff";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                CodeReadResult result = _codeReaderModule.Run(ofd.FileName);

                if (result.Success)
                {
                    txtVisionResult.Text =
                        $"Type: {result.CodeType}{Environment.NewLine}" +
                        $"Text: {result.CodeText}";
                }
                else
                {
                    txtVisionResult.Text = $"Failed: {result.ErrorMessage}";
                }
            }
        }

        private void CameraService_FrameReceived(object sender, CameraFrameEventArgs e)
        {
            if (picCamera.InvokeRequired)
            {
                picCamera.BeginInvoke(new Action(() => UpdatePreview(e.Frame)));
            }
            else
            {
                UpdatePreview(e.Frame);
            }
        }

        private void UpdatePreview(Bitmap frame)
        {
            var old = picCamera.Image;
            picCamera.Image = frame;
            old?.Dispose();
        }

        private void DashboardControl_Disposed(object sender, EventArgs e)
        {
            if (_cameraService != null)
            {
                _cameraService.FrameReceived -= CameraService_FrameReceived;
                _cameraService.Dispose();
                _cameraService = null;
            }
        }
    }
}
