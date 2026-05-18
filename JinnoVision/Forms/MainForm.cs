using JinnoVision.App.Interfaces;
using JinnoVision.App.Models;
using JinnoVision.App.Services;
using JinnoVision.User_Control; // your UserControls namespace
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.Forms
{
    public partial class MainForm : Form
    {
        private readonly IAuthService _authService;
        private Button _activeButton;
        private AuthenticatedUser _currentUser;

        public MainForm(IAuthService authService)
        {
            _authService = authService;
            InitializeComponent();

            InitializeMenuButtons();
            WireHeaderEvents();
            ShowLoggedOutState();
        }

        private void ShowLoggedOutState()
        {
            panelHeader.Visible = false;

            var login = new LoginControl(_authService);
            login.LoginSucceeded += Login_LoginSucceeded;

            LoadPage(login);
        }
        private void Login_LoginSucceeded(object sender, AuthenticatedUser user)
        {
            _currentUser = user;

            panelHeader.Visible = true;
            btnRolePill.Text = user.Role ?? "USER";
            lblAppNameRight.Text = "JINNO";

            SetActive(btnDashboard);
            LoadPage(new DashboardControl());
        }
        private void InitializeMenuButtons()
        {
            ConfigureMenuButton(btnDashboard, "Dashboard");
            ConfigureMenuButton(btnReview, "Review");
            ConfigureMenuButton(btnSetup, "Setup");
            ConfigureMenuButton(btnReports, "Reports");
            ConfigureMenuButton(btnSettings, "Settings");
            ConfigureMenuButton(btnFolders, "Folders");

            // Click handlers – load different UserControls
            btnDashboard.Click += (s, e) =>
            {
                SetActive(btnDashboard);
                LoadPage(new DashboardControl());
            };

            btnReview.Click += (s, e) =>
            {
                SetActive(btnReview);
                LoadPage(new ReviewControl());
            };

            btnSetup.Click += (s, e) =>
            {
                SetActive(btnSetup);
                LoadPage(new SetupControl());
            };

            btnReports.Click += (s, e) =>
            {
                SetActive(btnReports);
                LoadPage(new ReportsControl());
            };

            btnSettings.Click += (s, e) =>
            {
                SetActive(btnSettings);
                LoadPage(new SettingsControl());
            };

            btnFolders.Click += (s, e) =>
            {
                SetActive(btnFolders);
                LoadPage(new FoldersControl());
            };

            // Default selection
            SetActive(btnDashboard);
            LoadPage(new DashboardControl());
        }

        private void ConfigureMenuButton(Button btn, string text)
        {
            btn.Text = text;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.AutoSize = true;
            btn.Margin = new Padding(15, 0, 15, 0);
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.DimGray;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Cursor = Cursors.Hand;
        }

        private void SetActive(Button btn)
        {
            if (_activeButton != null)
            {
                _activeButton.BackColor = Color.Transparent;
                _activeButton.ForeColor = Color.DimGray;
            }

            _activeButton = btn;
            _activeButton.BackColor = Color.FromArgb(66, 99, 144);  // blue pill
            _activeButton.ForeColor = Color.White;
        }

        private void LoadPage(UserControl control)
        {
            panelContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContent.Controls.Add(control);
        }

        private void WireHeaderEvents()
        {
            // Logout click – adjust behavior as needed
            btnLogout.Click += (s, e) =>
            {
                // Example: go back to login form
                // var login = new LoginForm(...);
                // login.Show();
                // this.Close();
            };

            // If you want the role pill to be non-clickable, leave it as is.
        }
    }
}
