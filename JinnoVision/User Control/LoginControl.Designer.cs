using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    partial class LoginControl
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private Panel panelLoginBox;

        private void InitializeComponent()
        {
            this.panelLoginBox = new Panel();
            this.lblTitle = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();

            this.SuspendLayout();

            this.BackColor = Color.WhiteSmoke;

            this.panelLoginBox.Size = new Size(360, 260);
            this.panelLoginBox.BackColor = Color.White;
            this.panelLoginBox.Anchor = AnchorStyles.None;

            this.lblTitle.Text = "JinnoVision Login";
            this.lblTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(70, 25);

            this.lblUsername.Text = "Username";
            this.lblUsername.Location = new Point(45, 85);
            this.lblUsername.AutoSize = true;

            this.txtUsername.Location = new Point(45, 110);
            this.txtUsername.Size = new Size(270, 25);

            this.lblPassword.Text = "Password";
            this.lblPassword.Location = new Point(45, 145);
            this.lblPassword.AutoSize = true;

            this.txtPassword.Location = new Point(45, 170);
            this.txtPassword.Size = new Size(270, 25);
            this.txtPassword.UseSystemPasswordChar = true;

            this.btnLogin.Text = "Login";
            this.btnLogin.Location = new Point(45, 215);
            this.btnLogin.Size = new Size(270, 35);

            this.panelLoginBox.Controls.Add(this.lblTitle);
            this.panelLoginBox.Controls.Add(this.lblUsername);
            this.panelLoginBox.Controls.Add(this.txtUsername);
            this.panelLoginBox.Controls.Add(this.lblPassword);
            this.panelLoginBox.Controls.Add(this.txtPassword);
            this.panelLoginBox.Controls.Add(this.btnLogin);

            this.Controls.Add(this.panelLoginBox);

            this.Load += (s, e) =>
            {
                this.panelLoginBox.Left = (this.Width - this.panelLoginBox.Width) / 2;
                this.panelLoginBox.Top = (this.Height - this.panelLoginBox.Height) / 2;
            };

            this.Resize += (s, e) =>
            {
                this.panelLoginBox.Left = (this.Width - this.panelLoginBox.Width) / 2;
                this.panelLoginBox.Top = (this.Height - this.panelLoginBox.Height) / 2;
            };

            this.ResumeLayout(false);
        }
    }
}