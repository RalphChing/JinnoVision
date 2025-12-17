using JinnoVision.Services;
using System;
using System.Windows.Forms;

namespace JinnoVision
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _auth = new AuthService();

        public LoginForm()
        {
            InitializeComponent();
            var gittest = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            var company = txtCompany.Text.Trim();
            var user = txtUsername.Text.Trim();
            var pass = txtPassword.Text;

            if (company.Length == 0 || user.Length == 0)
            {
                lblStatus.Text = "Enter company + username.";
                return;
            }

            var result = _auth.Login(company, user, pass);
            if (!result.Success)
            {
                lblStatus.Text = result.FailureReason;
                return;
            }

            // Open main form
            Hide();
            var main = new MainForm(result);
            main.FormClosed += (s, args) => Close();
            main.Show();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(720, 802);
            this.Name = "LoginForm";
            this.ResumeLayout(false);

        }
    }
}
