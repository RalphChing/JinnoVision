using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace JinnoVision.User_Control
{
    public partial class DashboardControl : UserControl
    {
        private FilterInfoCollection _videoDevices;
        private VideoCaptureDevice _videoSource;

        public DashboardControl()
        {
            InitializeComponent();

            LoadAvailableCameras();

            btnStartCamera.Click += BtnStartCamera_Click;
            btnStopCamera.Click += BtnStopCamera_Click;

            this.Disposed += DashboardControl_Disposed;
        }

        private void LoadAvailableCameras()
        {
            try
            {
                _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                cboCameras.Items.Clear();

                if (_videoDevices.Count == 0)
                {
                    cboCameras.Items.Add("No cameras found");
                    cboCameras.SelectedIndex = 0;
                    btnStartCamera.Enabled = false;
                    return;
                }

                foreach (FilterInfo device in _videoDevices)
                {
                    cboCameras.Items.Add(device.Name);
                }

                cboCameras.SelectedIndex = 0;
                btnStartCamera.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cameras: " + ex.Message,
                    "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStartCamera.Enabled = false;
            }
        }

        private void BtnStartCamera_Click(object sender, EventArgs e)
        {
            if (_videoDevices == null || _videoDevices.Count == 0)
                return;

            // Stop previous source if running
            StopCamera();

            int index = cboCameras.SelectedIndex;
            if (index < 0 || index >= _videoDevices.Count)
                return;

            _videoSource = new VideoCaptureDevice(_videoDevices[index].MonikerString);
            _videoSource.NewFrame += VideoSource_NewFrame;
            _videoSource.Start();
        }

        private void BtnStopCamera_Click(object sender, EventArgs e)
        {
            StopCamera();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                // Clone the frame because AForge reuses the buffer
                Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

                // Update the PictureBox on the UI thread
                if (picCamera.InvokeRequired)
                {
                    picCamera.BeginInvoke(new Action(() =>
                    {
                        var old = picCamera.Image;
                        picCamera.Image = frame;
                        old?.Dispose();
                    }));
                }
                else
                {
                    var old = picCamera.Image;
                    picCamera.Image = frame;
                    old?.Dispose();
                }
            }
            catch
            {
                // swallow frame errors to avoid crashing the UI
            }
        }

        private void StopCamera()
        {
            if (_videoSource != null)
            {
                try
                {
                    if (_videoSource.IsRunning)
                    {
                        _videoSource.SignalToStop();
                        _videoSource.WaitForStop();
                    }
                }
                catch
                {
                    // ignore shutdown exceptions
                }
                finally
                {
                    _videoSource.NewFrame -= VideoSource_NewFrame;
                    _videoSource = null;
                }
            }

            // Optional: clear the last frame
            if (picCamera.Image != null)
            {
                picCamera.Image.Dispose();
                picCamera.Image = null;
            }
        }

        private void DashboardControl_Disposed(object sender, EventArgs e)
        {
            StopCamera();
        }
    }
}
