using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    partial class DashboardControl
    {
        private System.ComponentModel.IContainer components = null;

        private GroupBox grpCamera;
        private PictureBox picCamera;
        private Button btnConnect;
        private Button btnStart;
        private Button btnStop; 
        private Button btnDisconnect;
        private Label lblStatus;
        private Button btnCaptureInspect;

        private GroupBox grpCapture;
        private PictureBox picCapture;

        private System.Windows.Forms.Label lblCobotStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_cobotService != null)
                {
                    _cobotService.Dispose();
                    _cobotService = null;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpCamera = new GroupBox();
            this.picCamera = new PictureBox();
            this.btnConnect = new Button();
            this.btnStart = new Button();
            this.btnStop = new Button();
            this.btnDisconnect = new Button();
            this.btnCaptureInspect = new Button();
            this.lblStatus = new Label();

            this.grpCapture = new GroupBox();
            this.picCapture = new PictureBox();

            this.grpCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.SuspendLayout();

            // ======================
            // grpCamera
            // ======================
            this.grpCamera.Text = "Live Camera";
            this.grpCamera.Font = new Font("Segoe UI", 10F);
            this.grpCamera.Location = new Point(10, 10);
            this.grpCamera.Size = new Size(1000, 600);

            // btnConnect
            this.btnConnect.Text = "Connect";
            this.btnConnect.Location = new Point(15, 30);
            this.btnConnect.Size = new Size(90, 30);

            // btnStart
            this.btnStart.Text = "Start";
            this.btnStart.Location = new Point(115, 30);
            this.btnStart.Size = new Size(90, 30);

            // btnStop
            this.btnStop.Text = "Stop";
            this.btnStop.Location = new Point(215, 30);
            this.btnStop.Size = new Size(90, 30);

            // btnCaptureInspect
            this.btnCaptureInspect.Text = "Capture + Inspect";
            this.btnCaptureInspect.Location = new Point(315, 30);
            this.btnCaptureInspect.Size = new Size(160, 30);

            // btnDisconnect
            this.btnDisconnect = new Button();
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Location = new Point(515, 30);
            this.btnDisconnect.Size = new Size(100, 30);

            // lblStatus
            this.lblStatus.Text = "Idle";
            this.lblStatus.Location = new Point(630, 35);
            this.lblStatus.AutoSize = true;

            // picCamera
            this.picCamera.Location = new Point(15, 70);
            this.picCamera.Size = new Size(960, 500);
            this.picCamera.BackColor = Color.Black;
            this.picCamera.SizeMode = PictureBoxSizeMode.Zoom;

            //cobot connection

            this.lblCobotStatus = new System.Windows.Forms.Label();
            this.lblCobotStatus.Location = new System.Drawing.Point(750, 25);
            this.lblCobotStatus.Name = "lblCobotStatus";
            this.lblCobotStatus.Size = new System.Drawing.Size(220, 35);
            this.lblCobotStatus.Text = "Cobot Connecting...";
            this.lblCobotStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCobotStatus.BackColor = System.Drawing.Color.Gray;
            this.lblCobotStatus.ForeColor = System.Drawing.Color.White;


            this.grpCamera.Controls.Add(this.btnConnect);
            this.grpCamera.Controls.Add(this.btnStart);
            this.grpCamera.Controls.Add(this.btnStop);
            this.grpCamera.Controls.Add(this.btnCaptureInspect);
            this.grpCamera.Controls.Add(this.btnDisconnect);
            this.grpCamera.Controls.Add(this.lblStatus);
            this.grpCamera.Controls.Add(this.picCamera);
            this.grpCamera.Controls.Add(this.lblCobotStatus);
            // ======================
            // grpCapture
            // ======================
            this.grpCapture.Text = "Captured Inspection";
            this.grpCapture.Font = new Font("Segoe UI", 10F);
            this.grpCapture.Location = new Point(1020, 10);
            this.grpCapture.Size = new Size(500, 600);

            // picCapture
            this.picCapture.Location = new Point(15, 30);
            this.picCapture.Size = new Size(470, 540);
            this.picCapture.BackColor = Color.Black;
            this.picCapture.SizeMode = PictureBoxSizeMode.Zoom;

            this.grpCapture.Controls.Add(this.picCapture);

            // ======================
            // DashboardControl
            // ======================
            this.Controls.Add(this.grpCamera);
            this.Controls.Add(this.grpCapture);
            this.Size = new Size(1900, 620);
            this.BackColor = Color.WhiteSmoke;

            this.grpCamera.ResumeLayout(false);
            this.grpCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.ResumeLayout(false);
        }
    }
}