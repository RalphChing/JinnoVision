using JinnoVision.App.Interfaces;
using JinnoVision.App.Models;
using System;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    public partial class LoginControl : UserControl
    {
        private readonly IAuthService _authService;

        public event EventHandler<AuthenticatedUser> LoginSucceeded;

        public LoginControl(IAuthService authService)
        {
            _authService = authService;
            InitializeComponent();

            btnLogin.Click += BtnLogin_Click;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter username.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter password.");
                return;
            }

            btnLogin.Enabled = false;

            try
            {
                var user = await _authService.LoginAsync(username, password);

                if (user == null)
                {
                    MessageBox.Show("Invalid username or password.");
                    txtPassword.Clear();
                    return;
                }

                LoginSucceeded?.Invoke(this, user);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }
    }
}