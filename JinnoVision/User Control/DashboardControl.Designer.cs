using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    partial class DashboardControl
    {
        private TextBox txtScene;
        private GroupBox grpCamera;
        private ComboBox cboCameras;
        private Button btnStartCamera;
        private Button btnStopCamera;
        private PictureBox picCamera;

        private void InitializeComponent()
        {
            this.txtScene = new System.Windows.Forms.TextBox();
            this.grpCamera = new System.Windows.Forms.GroupBox();
            this.cboCameras = new System.Windows.Forms.ComboBox();
            this.btnStartCamera = new System.Windows.Forms.Button();
            this.btnStopCamera = new System.Windows.Forms.Button();
            this.picCamera = new System.Windows.Forms.PictureBox();

            this.grpCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // txtScene
            // 
            this.txtScene.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtScene.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtScene.ReadOnly = true;
            this.txtScene.BorderStyle = BorderStyle.None;
            this.txtScene.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtScene.Text = "Dashboard scene loaded";
            this.txtScene.Margin = new Padding(20);
            this.txtScene.Height = 40;
            // 
            // grpCamera
            // 
            this.grpCamera.Text = "Live Camera";
            this.grpCamera.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.grpCamera.Dock = DockStyle.Fill;
            this.grpCamera.Padding = new Padding(10);
            this.grpCamera.Controls.Add(this.picCamera);
            this.grpCamera.Controls.Add(this.btnStopCamera);
            this.grpCamera.Controls.Add(this.btnStartCamera);
            this.grpCamera.Controls.Add(this.cboCameras);
            // 
            // cboCameras
            // 
            this.cboCameras.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCameras.Location = new Point(15, 25);
            this.cboCameras.Width = 250;
            this.cboCameras.Name = "cboCameras";
            // 
            // btnStartCamera
            // 
            this.btnStartCamera.Text = "Start";
            this.btnStartCamera.Location = new Point(280, 23);
            this.btnStartCamera.Size = new Size(75, 27);
            this.btnStartCamera.Name = "btnStartCamera";
            // 
            // btnStopCamera
            // 
            this.btnStopCamera.Text = "Stop";
            this.btnStopCamera.Location = new Point(365, 23);
            this.btnStopCamera.Size = new Size(75, 27);
            this.btnStopCamera.Name = "btnStopCamera";
            // 
            // picCamera
            // 
            this.picCamera.Location = new Point(15, 60);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new Size(640, 360);
            this.picCamera.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picCamera.BackColor = Color.Black;
            // 
            // DashboardControl
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.grpCamera);
            this.Controls.Add(this.txtScene);
            this.Name = "DashboardControl";
            this.Dock = DockStyle.Fill;

            this.grpCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
