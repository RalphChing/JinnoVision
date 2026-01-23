using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    partial class FoldersControl
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private TextBox txtScene;

        private void InitializeComponent()
        {
            this.txtScene = new TextBox();
            this.SuspendLayout();
            // 
            // txtScene
            // 
            this.txtScene.Dock = DockStyle.Top;
            this.txtScene.Font = new Font("Segoe UI", 12F);
            this.txtScene.ReadOnly = true;
            this.txtScene.BorderStyle = BorderStyle.None;
            this.txtScene.BackColor = Color.WhiteSmoke;
            this.txtScene.Text = "Folders scene loaded";
            this.txtScene.Margin = new Padding(20);
            this.txtScene.Height = 40;
            // 
            // FoldersControl
            // 
            this.BackColor = Color.WhiteSmoke;
            this.Controls.Add(this.txtScene);
            this.Name = "FoldersControl";
            this.Dock = DockStyle.Fill;
            this.ResumeLayout(false);
        }
        #endregion
    }
}
