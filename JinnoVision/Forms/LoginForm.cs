using JinnoVision.App.Models;
using JinnoVision.App.Services;
using JinnoVision.App.Interfaces;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace JinnoVision.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _authService;

        public LoginForm(IAuthService authService)
        {
            _authService = authService;
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter your username.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your password.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            btnLogin.Enabled = false;

            try
            {
                AuthenticatedUser user = await _authService.LoginAsync(username, password);

                if (user != null)
                {
                    MessageBox.Show($"Welcome, {user.Username}", "Login Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var mainForm = new MainForm(user);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username/password or inactive account.",
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during login:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }
    }
}
