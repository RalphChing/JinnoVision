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

        private GroupBox grpVision;
        private Button btnRunCodeRead;
        private TextBox txtVisionResult;
        private GroupBox grpCapture;
        private PictureBox picCapture;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

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

            this.grpVision = new GroupBox();
            this.btnRunCodeRead = new Button();
            this.txtVisionResult = new TextBox();
            this.grpCapture = new GroupBox();
            this.picCapture = new PictureBox();

            this.grpCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.grpVision.SuspendLayout();
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

            this.grpCamera.Controls.Add(this.btnConnect);
            this.grpCamera.Controls.Add(this.btnStart);
            this.grpCamera.Controls.Add(this.btnStop);
            this.grpCamera.Controls.Add(this.btnCaptureInspect);
            this.grpCamera.Controls.Add(this.btnDisconnect);
            this.grpCamera.Controls.Add(this.lblStatus);
            this.grpCamera.Controls.Add(this.picCamera);
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
            // grpVision
            // ======================
            this.grpVision.Text = "Vision Test";
            this.grpVision.Font = new Font("Segoe UI", 10F);
            this.grpVision.Location = new Point(1530, 10);
            this.grpVision.Size = new Size(350, 600);

            // btnRunCodeRead
            this.btnRunCodeRead.Text = "Run Code Read";
            this.btnRunCodeRead.Location = new Point(20, 30);
            this.btnRunCodeRead.Size = new Size(150, 35);

            // txtVisionResult
            this.txtVisionResult.Location = new Point(20, 80);
            this.txtVisionResult.Size = new Size(300, 450);
            this.txtVisionResult.Multiline = true;
            this.txtVisionResult.ScrollBars = ScrollBars.Vertical;
            this.txtVisionResult.Font = new Font("Consolas", 10F);

            this.grpVision.Controls.Add(this.btnRunCodeRead);
            this.grpVision.Controls.Add(this.txtVisionResult);

            // ======================
            // DashboardControl
            // ======================
            this.Controls.Add(this.grpCamera);
            this.Controls.Add(this.grpCapture);
            this.Controls.Add(this.grpVision);
            this.Size = new Size(1900, 620);
            this.BackColor = Color.WhiteSmoke;

            this.grpCamera.ResumeLayout(false);
            this.grpCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.grpVision.ResumeLayout(false);
            this.grpVision.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}