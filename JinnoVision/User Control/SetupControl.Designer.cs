using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    partial class SetupControl
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
            this.SuspendLayout();
            // 
            // SetupControl
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Name = "SetupControl";
            this.Size = new System.Drawing.Size(2199, 1087);
            this.ResumeLayout(false);

        }
        #endregion
    }
}
