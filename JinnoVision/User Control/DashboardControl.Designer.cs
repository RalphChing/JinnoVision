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
        private Label lblPlcStatus;
        private GroupBox grpCapture;
        private PictureBox picCapture;
        private Panel panelHeader;

        private System.Windows.Forms.Label lblCobotStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
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

            this.cboRecipes = new ComboBox();
            this.btnLoadRecipe = new Button();
            this.lblLoadedRecipe = new Label();
            this.lblCurrentStep = new Label();
            this.panelInspectionResults = new FlowLayoutPanel();
            this.panelHeader = new Panel();

            //panelHeader
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 80;
            this.panelHeader.BackColor = Color.White;
            this.panelHeader.Padding = new Padding(20);

            // cboRecipes
            this.cboRecipes.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRecipes.Location = new Point(20, 35);
            this.cboRecipes.Size = new Size(250, 30);

            // btnLoadRecipe
            this.btnLoadRecipe.Text = "Load Recipe";
            this.btnLoadRecipe.Location = new Point(285, 30);
            this.btnLoadRecipe.Size = new Size(120, 30);

            // lblLoadedRecipe
            this.lblLoadedRecipe.Text = "Loaded Recipe: -";
            this.lblLoadedRecipe.Location = new Point(435, 30);
            this.lblLoadedRecipe.AutoSize = true;

            // lblCurrentStep
            this.lblCurrentStep.Text = "Current Step: -";
            this.lblCurrentStep.Location = new Point(435, 50);
            this.lblCurrentStep.AutoSize = true;

            // panelInspectionResults
            this.panelInspectionResults.Location = new Point(20, 140);
            this.panelInspectionResults.Size = new Size(500, 300);
            this.panelInspectionResults.FlowDirection = FlowDirection.TopDown;
            this.panelInspectionResults.WrapContents = false;
            this.panelInspectionResults.AutoScroll = true;
            lblPlcStatus = new Label
            {
                Text = "PLC Disconnected",
                AutoSize = false,
                Width = 220,
                Height = 32,
                Location = new Point(420, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            //Controls.Add(lblPlcStatus);
            panelHeader.Controls.Add(this.cboRecipes);
            panelHeader.Controls.Add(this.btnLoadRecipe);
            panelHeader.Controls.Add(this.lblLoadedRecipe);
            panelHeader.Controls.Add(this.lblCurrentStep);
            this.Controls.Add(panelHeader);
            //this.Controls.Add(this.panelInspectionResults);

            this.grpCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.SuspendLayout();

            // ======================
            // grpCamera
            // ======================
            this.grpCamera.Text = "Live Camera";
            this.grpCamera.Font = new Font("Segoe UI", 10F);
            this.grpCamera.Location = new Point(10, 85);
            this.grpCamera.Size = new Size(800, 600);

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
            //this.btnCaptureInspect.Text = "Capture + Inspect";
            //this.btnCaptureInspect.Location = new Point(315, 30);
            //this.btnCaptureInspect.Size = new Size(160, 30);

            // btnDisconnect
            this.btnDisconnect = new Button();
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Location = new Point(315, 30);
            this.btnDisconnect.Size = new Size(100, 30);

            // lblStatus
            this.lblStatus.Text = "Idle";
            this.lblStatus.Location = new Point(420, 35);
            this.lblStatus.AutoSize = true;

            // picCamera
            this.picCamera.Location = new Point(15, 70);
            this.picCamera.Size = new Size(760, 500);
            this.picCamera.BackColor = Color.Black;
            this.picCamera.SizeMode = PictureBoxSizeMode.Zoom;

            //cobot connection

            this.lblCobotStatus = new System.Windows.Forms.Label();
            this.lblCobotStatus.Location = new System.Drawing.Point(555, 25);
            this.lblCobotStatus.Name = "lblCobotStatus";
            this.lblCobotStatus.Size = new System.Drawing.Size(220, 35);
            this.lblCobotStatus.Text = "Cobot Connecting...";
            this.lblCobotStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCobotStatus.BackColor = System.Drawing.Color.Gray;
            this.lblCobotStatus.ForeColor = System.Drawing.Color.White;


            this.grpCamera.Controls.Add(this.btnConnect);
            this.grpCamera.Controls.Add(this.btnStart);
            this.grpCamera.Controls.Add(this.btnStop);
            //this.grpCamera.Controls.Add(this.btnCaptureInspect);
            this.grpCamera.Controls.Add(this.btnDisconnect);
            this.grpCamera.Controls.Add(this.lblStatus);
            this.grpCamera.Controls.Add(this.picCamera);
            this.grpCamera.Controls.Add(this.lblCobotStatus);
            // ======================
            // grpCapture
            // ======================
            this.grpCapture.Text = "Previous Inspection";
            this.grpCapture.Font = new Font("Segoe UI", 10F);
            this.grpCapture.Location = new Point(820, 85);
            this.grpCapture.Size = new Size(800, 600);

            // picCapture
            this.picCapture.Location = new Point(15, 70);
            this.picCapture.Size = new Size(760, 500);
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