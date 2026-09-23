using System;
using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    public partial class SettingsControl : UserControl
    {
        private static readonly Color ColorTitleText = Color.FromArgb(30, 41, 59);
        private static readonly Color ColorSubtleText = Color.FromArgb(100, 116, 139);
        private static readonly Color ColorPositive = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorNegative = Color.FromArgb(220, 38, 38);

        public SettingsControl()
        {
            InitializeComponent();
        }

        private void TxtFullName_Enter(object sender, EventArgs e)
        {
            if (txtFullName.Text == "Enter full name")
            {
                txtFullName.Text = "";
                txtFullName.ForeColor = ColorTitleText;
            }
        }
        private void TxtFullName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                txtFullName.Text = "Enter full name";
                txtFullName.ForeColor = ColorSubtleText;
            }
        }

        private void TxtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "user@company.com")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = ColorTitleText;
            }
        }
        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmail.Text = "user@company.com";
                txtEmail.ForeColor = ColorSubtleText;
            }
        }

        private void TxtDepartment_Enter(object sender, EventArgs e)
        {
            if (txtDepartment.Text == "Quality Control, Production, etc.")
            {
                txtDepartment.Text = "";
                txtDepartment.ForeColor = ColorTitleText;
            }
        }
        private void TxtDepartment_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDepartment.Text))
            {
                txtDepartment.Text = "Quality Control, Production, etc.";
                txtDepartment.ForeColor = ColorSubtleText;
            }
        }

        private void TxtCurrentPassword_Enter(object sender, EventArgs e)
        {
            if (txtCurrentPassword.Text == "Enter current password")
            {
                txtCurrentPassword.Text = "";
                txtCurrentPassword.ForeColor = ColorTitleText;
                txtCurrentPassword.PasswordChar = '\u25CF';
            }
        }
        private void TxtCurrentPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text))
            {
                txtCurrentPassword.PasswordChar = '\0';
                txtCurrentPassword.Text = "Enter current password";
                txtCurrentPassword.ForeColor = ColorSubtleText;
            }
        }

        private void TxtNewPassword_Enter(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == "Enter new password (min. 6 characters)")
            {
                txtNewPassword.Text = "";
                txtNewPassword.ForeColor = ColorTitleText;
                txtNewPassword.PasswordChar = '\u25CF';
            }
        }
        private void TxtNewPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                txtNewPassword.PasswordChar = '\0';
                txtNewPassword.Text = "Enter new password (min. 6 characters)";
                txtNewPassword.ForeColor = ColorSubtleText;
            }
        }

        private void TxtConfirmPassword_Enter(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text == "Confirm new password")
            {
                txtConfirmPassword.Text = "";
                txtConfirmPassword.ForeColor = ColorTitleText;
                txtConfirmPassword.PasswordChar = '\u25CF';
            }
        }
        private void TxtConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                txtConfirmPassword.PasswordChar = '\0';
                txtConfirmPassword.Text = "Confirm new password";
                txtConfirmPassword.ForeColor = ColorSubtleText;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnSaveUserSettings_Click(object sender, EventArgs e)
        {
            
        }

        private void CboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public void SetHardwareStatus(string deviceName, bool connected)
        {
            Label dot;
            Label value;

            switch (deviceName)
            {
                case "PLC Controller":
                    dot = lblStatusDotPlc; value = lblStatusValuePlc; break;
                case "Camera":
                    dot = lblStatusDotCamera; value = lblStatusValueCamera; break;
                case "I/O Module":
                    dot = lblStatusDotIoModule; value = lblStatusValueIoModule; break;
                case "Lighting":
                    dot = lblStatusDotLighting; value = lblStatusValueLighting; break;
                default:
                    return;
            }

            dot.ForeColor = connected ? ColorPositive : ColorNegative;
            value.Text = connected ? "Connected" : "Not Connected";
        }
    }
}