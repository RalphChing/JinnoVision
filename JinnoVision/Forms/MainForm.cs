using JinnoVision.App.Models;
using System;
using System.Windows.Forms;

namespace JinnoVision.Forms
{
    public partial class MainForm : Form
    {
        private readonly AuthenticatedUser _currentUser;

        public MainForm(AuthenticatedUser currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();

            this.Text = $"Main - {_currentUser.Username} ({_currentUser.Role})";
        }
    }
}
