using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    partial class SettingsControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        // -- Root / header --
        private TableLayoutPanel tableLayoutRoot;
        private TableLayoutPanel tableLayoutHeader;
        private Panel panelHeaderIcon;
        private Label lblHeaderIconGlyph;
        private FlowLayoutPanel flowLayoutTitleBlock;
        private Label lblPageTitle;
        private Label lblPageSubtitle;
        private Panel panelHeaderSpacer;
        private FlowLayoutPanel flowLayoutHeaderRight;
        private Button btnReset;
        private Button btnSave;

        private Panel panelBody;
        private FlowLayoutPanel flowLayoutCards;

        // -- User Settings card --
        private Panel panelUserSettingsCard;
        private TableLayoutPanel tableLayoutUserSettingsHeader;
        private FlowLayoutPanel flowLayoutUserSettingsTitleBlock;
        private Label lblUserSettingsIcon;
        private Label lblUserSettingsTitle;
        private Panel panelUserSettingsHeaderSpacer;
        private Button btnSaveUserSettings;

        private TableLayoutPanel tableLayoutUserSettingsColumns;

        // -- Profile Information (left column) --
        private FlowLayoutPanel flowLayoutProfileInfo;
        private FlowLayoutPanel flowLayoutProfileInfoHeader;
        private Label lblProfileInfoIcon;
        private Label lblProfileInfoTitle;
        private Panel panelProfileInfoDivider;

        private Label lblUsernameLabel;
        private Label lblUsernameRequiredMark;
        private Panel panelUsernameInput;
        private Label lblUsernameIcon;
        private TextBox txtUsername;

        private Label lblFullNameLabel;
        private TextBox txtFullName;

        private Label lblEmailLabel;
        private Panel panelEmailInput;
        private Label lblEmailIcon;
        private TextBox txtEmail;

        private Label lblRoleLabel;
        private ComboBox cboRole;
        private Label lblRoleHelperText;

        private Label lblDepartmentLabel;
        private TextBox txtDepartment;

        // -- Change Password (right column) --
        private FlowLayoutPanel flowLayoutChangePassword;
        private FlowLayoutPanel flowLayoutChangePasswordHeader;
        private Label lblChangePasswordIcon;
        private Label lblChangePasswordTitle;
        private Label lblChangePasswordSubtitle;
        private Panel panelChangePasswordDivider;

        private Label lblCurrentPasswordLabel;
        private Panel panelCurrentPasswordInput;
        private Label lblCurrentPasswordIcon;
        private TextBox txtCurrentPassword;

        private Label lblNewPasswordLabel;
        private Panel panelNewPasswordInput;
        private Label lblNewPasswordIcon;
        private TextBox txtNewPassword;

        private Label lblConfirmPasswordLabel;
        private Panel panelConfirmPasswordInput;
        private Label lblConfirmPasswordIcon;
        private TextBox txtConfirmPassword;

        private Panel panelPasswordRequirements;
        private Label lblPasswordRequirementsIcon;
        private Label lblPasswordRequirementsTitle;
        private Label lblPasswordRequirementsBullets;

        // -- Hardware Status card --
        private Panel panelHardwareStatusCard;
        private FlowLayoutPanel flowLayoutHardwareStatusHeader;
        private Label lblHardwareStatusIcon;
        private Label lblHardwareStatusTitle;
        private TableLayoutPanel tableLayoutHardwareStatusItems;

        private FlowLayoutPanel flowLayoutStatusPlc;
        private Label lblStatusDotPlc;
        private Label lblStatusNamePlc;
        private Label lblStatusValuePlc;

        private FlowLayoutPanel flowLayoutStatusCamera;
        private Label lblStatusDotCamera;
        private Label lblStatusNameCamera;
        private Label lblStatusValueCamera;

        private FlowLayoutPanel flowLayoutStatusIoModule;
        private Label lblStatusDotIoModule;
        private Label lblStatusNameIoModule;
        private Label lblStatusValueIoModule;

        private FlowLayoutPanel flowLayoutStatusLighting;
        private Label lblStatusDotLighting;
        private Label lblStatusNameLighting;
        private Label lblStatusValueLighting;

        private void InitializeComponent()
        {
            this.tableLayoutRoot = new TableLayoutPanel();

            this.tableLayoutHeader = new TableLayoutPanel();
            this.panelHeaderIcon = new Panel();
            this.lblHeaderIconGlyph = new Label();
            this.flowLayoutTitleBlock = new FlowLayoutPanel();
            this.lblPageTitle = new Label();
            this.lblPageSubtitle = new Label();
            this.panelHeaderSpacer = new Panel();
            this.flowLayoutHeaderRight = new FlowLayoutPanel();
            this.btnReset = new Button();
            this.btnSave = new Button();

            this.panelBody = new Panel();
            this.flowLayoutCards = new FlowLayoutPanel();

            this.panelUserSettingsCard = new Panel();
            this.tableLayoutUserSettingsHeader = new TableLayoutPanel();
            this.flowLayoutUserSettingsTitleBlock = new FlowLayoutPanel();
            this.lblUserSettingsIcon = new Label();
            this.lblUserSettingsTitle = new Label();
            this.panelUserSettingsHeaderSpacer = new Panel();
            this.btnSaveUserSettings = new Button();

            this.tableLayoutUserSettingsColumns = new TableLayoutPanel();

            this.flowLayoutProfileInfo = new FlowLayoutPanel();
            this.flowLayoutProfileInfoHeader = new FlowLayoutPanel();
            this.lblProfileInfoIcon = new Label();
            this.lblProfileInfoTitle = new Label();
            this.panelProfileInfoDivider = new Panel();

            this.lblUsernameLabel = new Label();
            this.lblUsernameRequiredMark = new Label();
            this.panelUsernameInput = new Panel();
            this.lblUsernameIcon = new Label();
            this.txtUsername = new TextBox();

            this.lblFullNameLabel = new Label();
            this.txtFullName = new TextBox();

            this.lblEmailLabel = new Label();
            this.panelEmailInput = new Panel();
            this.lblEmailIcon = new Label();
            this.txtEmail = new TextBox();

            this.lblRoleLabel = new Label();
            this.cboRole = new ComboBox();
            this.lblRoleHelperText = new Label();

            this.lblDepartmentLabel = new Label();
            this.txtDepartment = new TextBox();

            this.flowLayoutChangePassword = new FlowLayoutPanel();
            this.flowLayoutChangePasswordHeader = new FlowLayoutPanel();
            this.lblChangePasswordIcon = new Label();
            this.lblChangePasswordTitle = new Label();
            this.lblChangePasswordSubtitle = new Label();
            this.panelChangePasswordDivider = new Panel();

            this.lblCurrentPasswordLabel = new Label();
            this.panelCurrentPasswordInput = new Panel();
            this.lblCurrentPasswordIcon = new Label();
            this.txtCurrentPassword = new TextBox();

            this.lblNewPasswordLabel = new Label();
            this.panelNewPasswordInput = new Panel();
            this.lblNewPasswordIcon = new Label();
            this.txtNewPassword = new TextBox();

            this.lblConfirmPasswordLabel = new Label();
            this.panelConfirmPasswordInput = new Panel();
            this.lblConfirmPasswordIcon = new Label();
            this.txtConfirmPassword = new TextBox();

            this.panelPasswordRequirements = new Panel();
            this.lblPasswordRequirementsIcon = new Label();
            this.lblPasswordRequirementsTitle = new Label();
            this.lblPasswordRequirementsBullets = new Label();

            this.panelHardwareStatusCard = new Panel();
            this.flowLayoutHardwareStatusHeader = new FlowLayoutPanel();
            this.lblHardwareStatusIcon = new Label();
            this.lblHardwareStatusTitle = new Label();
            this.tableLayoutHardwareStatusItems = new TableLayoutPanel();

            this.flowLayoutStatusPlc = new FlowLayoutPanel();
            this.lblStatusDotPlc = new Label();
            this.lblStatusNamePlc = new Label();
            this.lblStatusValuePlc = new Label();

            this.flowLayoutStatusCamera = new FlowLayoutPanel();
            this.lblStatusDotCamera = new Label();
            this.lblStatusNameCamera = new Label();
            this.lblStatusValueCamera = new Label();

            this.flowLayoutStatusIoModule = new FlowLayoutPanel();
            this.lblStatusDotIoModule = new Label();
            this.lblStatusNameIoModule = new Label();
            this.lblStatusValueIoModule = new Label();

            this.flowLayoutStatusLighting = new FlowLayoutPanel();
            this.lblStatusDotLighting = new Label();
            this.lblStatusNameLighting = new Label();
            this.lblStatusValueLighting = new Label();

            this.tableLayoutRoot.SuspendLayout();
            this.tableLayoutHeader.SuspendLayout();
            this.panelHeaderIcon.SuspendLayout();
            this.flowLayoutTitleBlock.SuspendLayout();
            this.flowLayoutHeaderRight.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.flowLayoutCards.SuspendLayout();
            this.panelUserSettingsCard.SuspendLayout();
            this.tableLayoutUserSettingsHeader.SuspendLayout();
            this.flowLayoutUserSettingsTitleBlock.SuspendLayout();
            this.tableLayoutUserSettingsColumns.SuspendLayout();
            this.flowLayoutProfileInfo.SuspendLayout();
            this.flowLayoutProfileInfoHeader.SuspendLayout();
            this.panelUsernameInput.SuspendLayout();
            this.panelEmailInput.SuspendLayout();
            this.flowLayoutChangePassword.SuspendLayout();
            this.flowLayoutChangePasswordHeader.SuspendLayout();
            this.panelCurrentPasswordInput.SuspendLayout();
            this.panelNewPasswordInput.SuspendLayout();
            this.panelConfirmPasswordInput.SuspendLayout();
            this.panelPasswordRequirements.SuspendLayout();
            this.panelHardwareStatusCard.SuspendLayout();
            this.flowLayoutHardwareStatusHeader.SuspendLayout();
            this.tableLayoutHardwareStatusItems.SuspendLayout();
            this.flowLayoutStatusPlc.SuspendLayout();
            this.flowLayoutStatusCamera.SuspendLayout();
            this.flowLayoutStatusIoModule.SuspendLayout();
            this.flowLayoutStatusLighting.SuspendLayout();
            this.SuspendLayout();

            // ---------------------------------------------------------- tableLayoutRoot
            this.tableLayoutRoot.Dock = DockStyle.Fill;
            this.tableLayoutRoot.ColumnCount = 1;
            this.tableLayoutRoot.RowCount = 2;
            this.tableLayoutRoot.BackColor = Color.FromArgb(245, 247, 250);
            this.tableLayoutRoot.Padding = new Padding(24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutRoot.Controls.Add(this.tableLayoutHeader, 0, 0);
            this.tableLayoutRoot.Controls.Add(this.panelBody, 0, 1);

            // ---------------------------------------------------------- Header
            this.tableLayoutHeader.Dock = DockStyle.Top;
            this.tableLayoutHeader.AutoSize = true;
            this.tableLayoutHeader.ColumnCount = 4;
            this.tableLayoutHeader.RowCount = 1;
            this.tableLayoutHeader.Margin = new Padding(0, 0, 0, 20);
            this.tableLayoutHeader.BackColor = Color.FromArgb(245, 247, 250);
            this.tableLayoutHeader.Name = "tableLayoutHeader";
            this.tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutHeader.Controls.Add(this.panelHeaderIcon, 0, 0);
            this.tableLayoutHeader.Controls.Add(this.flowLayoutTitleBlock, 1, 0);
            this.tableLayoutHeader.Controls.Add(this.panelHeaderSpacer, 2, 0);
            this.tableLayoutHeader.Controls.Add(this.flowLayoutHeaderRight, 3, 0);

            this.panelHeaderIcon.BackColor = Color.FromArgb(224, 234, 255);
            this.panelHeaderIcon.Size = new Size(44, 44);
            this.panelHeaderIcon.Margin = new Padding(0, 0, 12, 0);
            this.panelHeaderIcon.Name = "panelHeaderIcon";
            this.panelHeaderIcon.Controls.Add(this.lblHeaderIconGlyph);

            this.lblHeaderIconGlyph.Text = "\u2699";
            this.lblHeaderIconGlyph.Font = new Font("Segoe UI", 14F);
            this.lblHeaderIconGlyph.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblHeaderIconGlyph.AutoSize = false;
            this.lblHeaderIconGlyph.Dock = DockStyle.Fill;
            this.lblHeaderIconGlyph.TextAlign = ContentAlignment.MiddleCenter;
            this.lblHeaderIconGlyph.Name = "lblHeaderIconGlyph";

            this.flowLayoutTitleBlock.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutTitleBlock.AutoSize = true;
            this.flowLayoutTitleBlock.WrapContents = false;
            this.flowLayoutTitleBlock.BackColor = Color.FromArgb(245, 247, 250);
            this.flowLayoutTitleBlock.Name = "flowLayoutTitleBlock";
            this.flowLayoutTitleBlock.Controls.Add(this.lblPageTitle);
            this.flowLayoutTitleBlock.Controls.Add(this.lblPageSubtitle);

            this.lblPageTitle.Text = "System Settings";
            this.lblPageTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblPageTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Name = "lblPageTitle";

            this.lblPageSubtitle.Text = "Configure hardware triggering and inspection parameters";
            this.lblPageSubtitle.Font = new Font("Segoe UI", 9F);
            this.lblPageSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.Margin = new Padding(0, 2, 0, 0);
            this.lblPageSubtitle.Name = "lblPageSubtitle";

            this.panelHeaderSpacer.BackColor = Color.FromArgb(245, 247, 250);
            this.panelHeaderSpacer.Height = 1;
            this.panelHeaderSpacer.Name = "panelHeaderSpacer";

            this.flowLayoutHeaderRight.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutHeaderRight.AutoSize = true;
            this.flowLayoutHeaderRight.WrapContents = false;
            this.flowLayoutHeaderRight.Anchor = AnchorStyles.Right;
            this.flowLayoutHeaderRight.BackColor = Color.FromArgb(245, 247, 250);
            this.flowLayoutHeaderRight.Name = "flowLayoutHeaderRight";
            this.flowLayoutHeaderRight.Controls.Add(this.btnReset);
            this.flowLayoutHeaderRight.Controls.Add(this.btnSave);

            this.btnReset.Text = "\u21BA  Reset";
            this.btnReset.FlatStyle = FlatStyle.Flat;
            this.btnReset.BackColor = Color.White;
            this.btnReset.ForeColor = Color.FromArgb(30, 41, 59);
            this.btnReset.AutoSize = true;
            this.btnReset.Padding = new Padding(10, 6, 10, 6);
            this.btnReset.Margin = new Padding(0, 8, 12, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.TabIndex = 0;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);

            this.btnSave.Text = "\uD83D\uDCBE  Save";
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.BackColor = Color.FromArgb(37, 99, 235);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.AutoSize = true;
            this.btnSave.Padding = new Padding(10, 6, 10, 6);
            this.btnSave.Margin = new Padding(0, 8, 0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.TabIndex = 1;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // ---------------------------------------------------------- Body / cards
            this.panelBody.Dock = DockStyle.Fill;
            this.panelBody.AutoScroll = true;
            this.panelBody.BackColor = Color.FromArgb(245, 247, 250);
            this.panelBody.Name = "panelBody";
            this.panelBody.Controls.Add(this.flowLayoutCards);

            this.flowLayoutCards.Dock = DockStyle.Top;
            this.flowLayoutCards.AutoSize = true;
            this.flowLayoutCards.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutCards.WrapContents = false;
            this.flowLayoutCards.BackColor = Color.FromArgb(245, 247, 250);
            this.flowLayoutCards.Name = "flowLayoutCards";
            this.flowLayoutCards.Controls.Add(this.panelUserSettingsCard);
            this.flowLayoutCards.Controls.Add(this.panelHardwareStatusCard);

            // ============================================================ User Settings card
            this.panelUserSettingsCard.BackColor = Color.White;
            this.panelUserSettingsCard.BorderStyle = BorderStyle.FixedSingle;
            this.panelUserSettingsCard.Padding = new Padding(24);
            this.panelUserSettingsCard.Width = 1360;
            this.panelUserSettingsCard.Height = 700;
            this.panelUserSettingsCard.Margin = new Padding(0, 0, 0, 20);
            this.panelUserSettingsCard.Name = "panelUserSettingsCard";
            this.panelUserSettingsCard.Controls.Add(this.tableLayoutUserSettingsColumns);
            this.panelUserSettingsCard.Controls.Add(this.tableLayoutUserSettingsHeader);

            this.tableLayoutUserSettingsHeader.Dock = DockStyle.Top;
            this.tableLayoutUserSettingsHeader.AutoSize = true;
            this.tableLayoutUserSettingsHeader.ColumnCount = 3;
            this.tableLayoutUserSettingsHeader.RowCount = 1;
            this.tableLayoutUserSettingsHeader.Margin = new Padding(0, 0, 0, 20);
            this.tableLayoutUserSettingsHeader.Name = "tableLayoutUserSettingsHeader";
            this.tableLayoutUserSettingsHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutUserSettingsHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutUserSettingsHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutUserSettingsHeader.Controls.Add(this.flowLayoutUserSettingsTitleBlock, 0, 0);
            this.tableLayoutUserSettingsHeader.Controls.Add(this.panelUserSettingsHeaderSpacer, 1, 0);
            this.tableLayoutUserSettingsHeader.Controls.Add(this.btnSaveUserSettings, 2, 0);

            this.flowLayoutUserSettingsTitleBlock.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutUserSettingsTitleBlock.AutoSize = true;
            this.flowLayoutUserSettingsTitleBlock.WrapContents = false;
            this.flowLayoutUserSettingsTitleBlock.Name = "flowLayoutUserSettingsTitleBlock";
            this.flowLayoutUserSettingsTitleBlock.Controls.Add(this.lblUserSettingsIcon);
            this.flowLayoutUserSettingsTitleBlock.Controls.Add(this.lblUserSettingsTitle);

            this.lblUserSettingsIcon.Text = "\uD83D\uDC64";
            this.lblUserSettingsIcon.Font = new Font("Segoe UI", 13F);
            this.lblUserSettingsIcon.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblUserSettingsIcon.AutoSize = true;
            this.lblUserSettingsIcon.Margin = new Padding(0, 4, 8, 0);
            this.lblUserSettingsIcon.Name = "lblUserSettingsIcon";

            this.lblUserSettingsTitle.Text = "User Settings";
            this.lblUserSettingsTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblUserSettingsTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblUserSettingsTitle.AutoSize = true;
            this.lblUserSettingsTitle.Name = "lblUserSettingsTitle";

            this.panelUserSettingsHeaderSpacer.Height = 1;
            this.panelUserSettingsHeaderSpacer.Name = "panelUserSettingsHeaderSpacer";

            this.btnSaveUserSettings.Text = "\uD83D\uDCBE  Save User Settings";
            this.btnSaveUserSettings.FlatStyle = FlatStyle.Flat;
            this.btnSaveUserSettings.BackColor = Color.FromArgb(37, 99, 235);
            this.btnSaveUserSettings.ForeColor = Color.White;
            this.btnSaveUserSettings.FlatAppearance.BorderSize = 0;
            this.btnSaveUserSettings.AutoSize = true;
            this.btnSaveUserSettings.Padding = new Padding(10, 6, 10, 6);
            this.btnSaveUserSettings.Name = "btnSaveUserSettings";
            this.btnSaveUserSettings.Click += new System.EventHandler(this.BtnSaveUserSettings_Click);

            this.tableLayoutUserSettingsColumns.Dock = DockStyle.Fill;
            this.tableLayoutUserSettingsColumns.ColumnCount = 2;
            this.tableLayoutUserSettingsColumns.RowCount = 1;
            this.tableLayoutUserSettingsColumns.Name = "tableLayoutUserSettingsColumns";
            this.tableLayoutUserSettingsColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutUserSettingsColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutUserSettingsColumns.Controls.Add(this.flowLayoutProfileInfo, 0, 0);
            this.tableLayoutUserSettingsColumns.Controls.Add(this.flowLayoutChangePassword, 1, 0);

            // ---------------------------------------------------------- Profile Information
            this.flowLayoutProfileInfo.Dock = DockStyle.Fill;
            this.flowLayoutProfileInfo.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutProfileInfo.WrapContents = false;
            this.flowLayoutProfileInfo.Padding = new Padding(0, 0, 20, 0);
            this.flowLayoutProfileInfo.Name = "flowLayoutProfileInfo";
            this.flowLayoutProfileInfo.Controls.Add(this.flowLayoutProfileInfoHeader);
            this.flowLayoutProfileInfo.Controls.Add(this.panelProfileInfoDivider);
            this.flowLayoutProfileInfo.Controls.Add(this.lblUsernameLabel);
            this.flowLayoutProfileInfo.Controls.Add(this.panelUsernameInput);
            this.flowLayoutProfileInfo.Controls.Add(this.lblFullNameLabel);
            this.flowLayoutProfileInfo.Controls.Add(this.txtFullName);
            this.flowLayoutProfileInfo.Controls.Add(this.lblEmailLabel);
            this.flowLayoutProfileInfo.Controls.Add(this.panelEmailInput);
            this.flowLayoutProfileInfo.Controls.Add(this.lblRoleLabel);
            this.flowLayoutProfileInfo.Controls.Add(this.cboRole);
            this.flowLayoutProfileInfo.Controls.Add(this.lblRoleHelperText);
            this.flowLayoutProfileInfo.Controls.Add(this.lblDepartmentLabel);
            this.flowLayoutProfileInfo.Controls.Add(this.txtDepartment);

            this.flowLayoutProfileInfoHeader.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutProfileInfoHeader.AutoSize = true;
            this.flowLayoutProfileInfoHeader.WrapContents = false;
            this.flowLayoutProfileInfoHeader.Name = "flowLayoutProfileInfoHeader";
            this.flowLayoutProfileInfoHeader.Controls.Add(this.lblProfileInfoIcon);
            this.flowLayoutProfileInfoHeader.Controls.Add(this.lblProfileInfoTitle);

            this.lblProfileInfoIcon.Text = "\uD83D\uDEE1";
            this.lblProfileInfoIcon.Font = new Font("Segoe UI", 10F);
            this.lblProfileInfoIcon.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblProfileInfoIcon.AutoSize = true;
            this.lblProfileInfoIcon.Margin = new Padding(0, 2, 6, 0);
            this.lblProfileInfoIcon.Name = "lblProfileInfoIcon";

            this.lblProfileInfoTitle.Text = "Profile Information";
            this.lblProfileInfoTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblProfileInfoTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblProfileInfoTitle.AutoSize = true;
            this.lblProfileInfoTitle.Name = "lblProfileInfoTitle";

            this.panelProfileInfoDivider.BackColor = Color.FromArgb(230, 232, 236);
            this.panelProfileInfoDivider.Height = 1;
            this.panelProfileInfoDivider.Width = 600;
            this.panelProfileInfoDivider.Margin = new Padding(0, 8, 0, 16);
            this.panelProfileInfoDivider.Name = "panelProfileInfoDivider";

            this.lblUsernameLabel.Text = "Username";
            this.lblUsernameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblUsernameLabel.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblUsernameLabel.AutoSize = true;
            this.lblUsernameLabel.Margin = new Padding(0, 0, 2, 4);
            this.lblUsernameLabel.Name = "lblUsernameLabel";
            

            this.lblUsernameLabel.Text = "Username *";

            this.panelUsernameInput.BackColor = Color.White;
            this.panelUsernameInput.BorderStyle = BorderStyle.FixedSingle;
            this.panelUsernameInput.Height = 32;
            this.panelUsernameInput.Width = 600;
            this.panelUsernameInput.Margin = new Padding(0, 0, 0, 16);
            this.panelUsernameInput.Name = "panelUsernameInput";
            this.panelUsernameInput.Controls.Add(this.txtUsername);
            this.panelUsernameInput.Controls.Add(this.lblUsernameIcon);

            this.lblUsernameIcon.Text = "\uD83D\uDC64";
            this.lblUsernameIcon.Font = new Font("Segoe UI", 9F);
            this.lblUsernameIcon.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblUsernameIcon.Dock = DockStyle.Left;
            this.lblUsernameIcon.Width = 28;
            this.lblUsernameIcon.TextAlign = ContentAlignment.MiddleCenter;
            this.lblUsernameIcon.Name = "lblUsernameIcon";

            this.txtUsername.Text = "user";
            this.txtUsername.BorderStyle = BorderStyle.None;
            this.txtUsername.Dock = DockStyle.Fill;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.TabIndex = 10;

            this.lblFullNameLabel.Text = "Full Name";
            this.lblFullNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblFullNameLabel.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblFullNameLabel.AutoSize = true;
            this.lblFullNameLabel.Margin = new Padding(0, 0, 0, 4);
            this.lblFullNameLabel.Name = "lblFullNameLabel";

            this.txtFullName.Text = "Enter full name";
            this.txtFullName.ForeColor = Color.FromArgb(100, 116, 139);
            this.txtFullName.Width = 600;
            this.txtFullName.Margin = new Padding(0, 0, 0, 16);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.TabIndex = 11;
            this.txtFullName.Enter += new System.EventHandler(this.TxtFullName_Enter);
            this.txtFullName.Leave += new System.EventHandler(this.TxtFullName_Leave);

            this.lblEmailLabel.Text = "Email Address";
            this.lblEmailLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblEmailLabel.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblEmailLabel.AutoSize = true;
            this.lblEmailLabel.Margin = new Padding(0, 0, 0, 4);
            this.lblEmailLabel.Name = "lblEmailLabel";

            this.panelEmailInput.BackColor = Color.White;
            this.panelEmailInput.BorderStyle = BorderStyle.FixedSingle;
            this.panelEmailInput.Height = 32;
            this.panelEmailInput.Width = 600;
            this.panelEmailInput.Margin = new Padding(0, 0, 0, 16);
            this.panelEmailInput.Name = "panelEmailInput";
            this.panelEmailInput.Controls.Add(this.txtEmail);
            this.panelEmailInput.Controls.Add(this.lblEmailIcon);

            this.lblEmailIcon.Text = "\u2709";
            this.lblEmailIcon.Font = new Font("Segoe UI", 9F);
            this.lblEmailIcon.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblEmailIcon.Dock = DockStyle.Left;
            this.lblEmailIcon.Width = 28;
            this.lblEmailIcon.TextAlign = ContentAlignment.MiddleCenter;
            this.lblEmailIcon.Name = "lblEmailIcon";

            this.txtEmail.Text = "user@company.com";
            this.txtEmail.ForeColor = Color.FromArgb(100, 116, 139);
            this.txtEmail.BorderStyle = BorderStyle.None;
            this.txtEmail.Dock = DockStyle.Fill;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.TabIndex = 12;
            this.txtEmail.Enter += new System.EventHandler(this.TxtEmail_Enter);
            this.txtEmail.Leave += new System.EventHandler(this.TxtEmail_Leave);

            this.lblRoleLabel.Text = "Role";
            this.lblRoleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblRoleLabel.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblRoleLabel.AutoSize = true;
            this.lblRoleLabel.Margin = new Padding(0, 0, 0, 4);
            this.lblRoleLabel.Name = "lblRoleLabel";

            this.cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRole.Width = 600;
            this.cboRole.Margin = new Padding(0, 0, 0, 4);
            this.cboRole.Items.AddRange(new object[] { "Operator", "Engineer", "Administrator" });
            this.cboRole.SelectedIndex = 1;
            this.cboRole.Name = "cboRole";
            this.cboRole.TabIndex = 13;
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.CboRole_SelectedIndexChanged);

            this.lblRoleHelperText.Text = "Operators have read-only dashboard access";
            this.lblRoleHelperText.Font = new Font("Segoe UI", 8F);
            this.lblRoleHelperText.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblRoleHelperText.AutoSize = true;
            this.lblRoleHelperText.Margin = new Padding(0, 0, 0, 16);
            this.lblRoleHelperText.Name = "lblRoleHelperText";

            this.lblDepartmentLabel.Text = "Department";
            this.lblDepartmentLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDepartmentLabel.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblDepartmentLabel.AutoSize = true;
            this.lblDepartmentLabel.Margin = new Padding(0, 0, 0, 4);
            this.lblDepartmentLabel.Name = "lblDepartmentLabel";

            this.txtDepartment.Text = "Quality Control, Production, etc.";
            this.txtDepartment.ForeColor = Color.FromArgb(100, 116, 139);
            this.txtDepartment.Width = 600;
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.TabIndex = 14;
            this.txtDepartment.Enter += new System.EventHandler(this.TxtDepartment_Enter);
            this.txtDepartment.Leave += new System.EventHandler(this.TxtDepartment_Leave);

            // ---------------------------------------------------------- Change Password
            this.flowLayoutChangePassword.Dock = DockStyle.Fill;
            this.flowLayoutChangePassword.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutChangePassword.WrapContents = false;
            this.flowLayoutChangePassword.Padding = new Padding(20, 0, 0, 0);
            this.flowLayoutChangePassword.Name = "flowLayoutChangePassword";
            this.flowLayoutChangePassword.Controls.Add(this.flowLayoutChangePasswordHeader);
            this.flowLayoutChangePassword.Controls.Add(this.lblChangePasswordSubtitle);
            this.flowLayoutChangePassword.Controls.Add(this.panelChangePasswordDivider);
            this.flowLayoutChangePassword.Controls.Add(this.lblCurrentPasswordLabel);
            this.flowLayoutChangePassword.Controls.Add(this.panelCurrentPasswordInput);
            this.flowLayoutChangePassword.Controls.Add(this.lblNewPasswordLabel);
            this.flowLayoutChangePassword.Controls.Add(this.panelNewPasswordInput);
            this.flowLayoutChangePassword.Controls.Add(this.lblConfirmPasswordLabel);
            this.flowLayoutChangePassword.Controls.Add(this.panelConfirmPasswordInput);
            this.flowLayoutChangePassword.Controls.Add(this.panelPasswordRequirements);

            this.flowLayoutChangePasswordHeader.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutChangePasswordHeader.AutoSize = true;
            this.flowLayoutChangePasswordHeader.WrapContents = false;
            this.flowLayoutChangePasswordHeader.Name = "flowLayoutChangePasswordHeader";
            this.flowLayoutChangePasswordHeader.Controls.Add(this.lblChangePasswordIcon);
            this.flowLayoutChangePasswordHeader.Controls.Add(this.lblChangePasswordTitle);

            this.lblChangePasswordIcon.Text = "\uD83D\uDD12";
            this.lblChangePasswordIcon.Font = new Font("Segoe UI", 10F);
            this.lblChangePasswordIcon.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblChangePasswordIcon.AutoSize = true;
            this.lblChangePasswordIcon.Margin = new Padding(0, 2, 6, 0);
            this.lblChangePasswordIcon.Name = "lblChangePasswordIcon";

            this.lblChangePasswordTitle.Text = "Change Password";
            this.lblChangePasswordTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblChangePasswordTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblChangePasswordTitle.AutoSize = true;
            this.lblChangePasswordTitle.Name = "lblChangePasswordTitle";

            this.lblChangePasswordSubtitle.Text = "Leave blank if you don't want to change your password";
            this.lblChangePasswordSubtitle.Font = new Font("Segoe UI", 8F);
            this.lblChangePasswordSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblChangePasswordSubtitle.AutoSize = true;
            this.lblChangePasswordSubtitle.Margin = new Padding(0, 0, 0, 8);
            this.lblChangePasswordSubtitle.Name = "lblChangePasswordSubtitle";

            this.panelChangePasswordDivider.BackColor = Color.FromArgb(230, 232, 236);
            this.panelChangePasswordDivider.Height = 1;
            this.panelChangePasswordDivider.Width = 600;
            this.panelChangePasswordDivider.Margin = new Padding(0, 0, 0, 16);
            this.panelChangePasswordDivider.Name = "panelChangePasswordDivider";

            this.lblCurrentPasswordLabel.Text = "Current Password";
            this.lblCurrentPasswordLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCurrentPasswordLabel.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblCurrentPasswordLabel.AutoSize = true;
            this.lblCurrentPasswordLabel.Margin = new Padding(0, 0, 0, 4);
            this.lblCurrentPasswordLabel.Name = "lblCurrentPasswordLabel";

            this.panelCurrentPasswordInput.BackColor = Color.White;
            this.panelCurrentPasswordInput.BorderStyle = BorderStyle.FixedSingle;
            this.panelCurrentPasswordInput.Height = 32;
            this.panelCurrentPasswordInput.Width = 600;
            this.panelCurrentPasswordInput.Margin = new Padding(0, 0, 0, 16);
            this.panelCurrentPasswordInput.Name = "panelCurrentPasswordInput";
            this.panelCurrentPasswordInput.Controls.Add(this.txtCurrentPassword);
            this.panelCurrentPasswordInput.Controls.Add(this.lblCurrentPasswordIcon);

            this.lblCurrentPasswordIcon.Text = "\uD83D\uDD12";
            this.lblCurrentPasswordIcon.Font = new Font("Segoe UI", 9F);
            this.lblCurrentPasswordIcon.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblCurrentPasswordIcon.Dock = DockStyle.Left;
            this.lblCurrentPasswordIcon.Width = 28;
            this.lblCurrentPasswordIcon.TextAlign = ContentAlignment.MiddleCenter;
            this.lblCurrentPasswordIcon.Name = "lblCurrentPasswordIcon";

            this.txtCurrentPassword.Text = "Enter current password";
            this.txtCurrentPassword.ForeColor = Color.FromArgb(100, 116, 139);
            this.txtCurrentPassword.BorderStyle = BorderStyle.None;
            this.txtCurrentPassword.Dock = DockStyle.Fill;
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.TabIndex = 15;
            this.txtCurrentPassword.Enter += new System.EventHandler(this.TxtCurrentPassword_Enter);
            this.txtCurrentPassword.Leave += new System.EventHandler(this.TxtCurrentPassword_Leave);

            this.lblNewPasswordLabel.Text = "New Password";
            this.lblNewPasswordLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblNewPasswordLabel.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblNewPasswordLabel.AutoSize = true;
            this.lblNewPasswordLabel.Margin = new Padding(0, 0, 0, 4);
            this.lblNewPasswordLabel.Name = "lblNewPasswordLabel";

            this.panelNewPasswordInput.BackColor = Color.White;
            this.panelNewPasswordInput.BorderStyle = BorderStyle.FixedSingle;
            this.panelNewPasswordInput.Height = 32;
            this.panelNewPasswordInput.Width = 600;
            this.panelNewPasswordInput.Margin = new Padding(0, 0, 0, 16);
            this.panelNewPasswordInput.Name = "panelNewPasswordInput";
            this.panelNewPasswordInput.Controls.Add(this.txtNewPassword);
            this.panelNewPasswordInput.Controls.Add(this.lblNewPasswordIcon);

            this.lblNewPasswordIcon.Text = "\uD83D\uDD12";
            this.lblNewPasswordIcon.Font = new Font("Segoe UI", 9F);
            this.lblNewPasswordIcon.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblNewPasswordIcon.Dock = DockStyle.Left;
            this.lblNewPasswordIcon.Width = 28;
            this.lblNewPasswordIcon.TextAlign = ContentAlignment.MiddleCenter;
            this.lblNewPasswordIcon.Name = "lblNewPasswordIcon";

            this.txtNewPassword.Text = "Enter new password (min. 6 characters)";
            this.txtNewPassword.ForeColor = Color.FromArgb(100, 116, 139);
            this.txtNewPassword.BorderStyle = BorderStyle.None;
            this.txtNewPassword.Dock = DockStyle.Fill;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.TabIndex = 16;
            this.txtNewPassword.Enter += new System.EventHandler(this.TxtNewPassword_Enter);
            this.txtNewPassword.Leave += new System.EventHandler(this.TxtNewPassword_Leave);

            this.lblConfirmPasswordLabel.Text = "Confirm New Password";
            this.lblConfirmPasswordLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblConfirmPasswordLabel.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblConfirmPasswordLabel.AutoSize = true;
            this.lblConfirmPasswordLabel.Margin = new Padding(0, 0, 0, 4);
            this.lblConfirmPasswordLabel.Name = "lblConfirmPasswordLabel";

            this.panelConfirmPasswordInput.BackColor = Color.White;
            this.panelConfirmPasswordInput.BorderStyle = BorderStyle.FixedSingle;
            this.panelConfirmPasswordInput.Height = 32;
            this.panelConfirmPasswordInput.Width = 600;
            this.panelConfirmPasswordInput.Margin = new Padding(0, 0, 0, 16);
            this.panelConfirmPasswordInput.Name = "panelConfirmPasswordInput";
            this.panelConfirmPasswordInput.Controls.Add(this.txtConfirmPassword);
            this.panelConfirmPasswordInput.Controls.Add(this.lblConfirmPasswordIcon);

            this.lblConfirmPasswordIcon.Text = "\uD83D\uDD12";
            this.lblConfirmPasswordIcon.Font = new Font("Segoe UI", 9F);
            this.lblConfirmPasswordIcon.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblConfirmPasswordIcon.Dock = DockStyle.Left;
            this.lblConfirmPasswordIcon.Width = 28;
            this.lblConfirmPasswordIcon.TextAlign = ContentAlignment.MiddleCenter;
            this.lblConfirmPasswordIcon.Name = "lblConfirmPasswordIcon";

            this.txtConfirmPassword.Text = "Confirm new password";
            this.txtConfirmPassword.ForeColor = Color.FromArgb(100, 116, 139);
            this.txtConfirmPassword.BorderStyle = BorderStyle.None;
            this.txtConfirmPassword.Dock = DockStyle.Fill;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.TabIndex = 17;
            this.txtConfirmPassword.Enter += new System.EventHandler(this.TxtConfirmPassword_Enter);
            this.txtConfirmPassword.Leave += new System.EventHandler(this.TxtConfirmPassword_Leave);

            this.panelPasswordRequirements.BackColor = Color.FromArgb(254, 249, 219);
            this.panelPasswordRequirements.Padding = new Padding(16);
            this.panelPasswordRequirements.Width = 600;
            this.panelPasswordRequirements.Height = 110;
            this.panelPasswordRequirements.Name = "panelPasswordRequirements";
            this.panelPasswordRequirements.Controls.Add(this.lblPasswordRequirementsBullets);
            this.panelPasswordRequirements.Controls.Add(this.lblPasswordRequirementsTitle);
            this.panelPasswordRequirements.Controls.Add(this.lblPasswordRequirementsIcon);

            this.lblPasswordRequirementsIcon.Text = "\u26A0";
            this.lblPasswordRequirementsIcon.Font = new Font("Segoe UI", 9F);
            this.lblPasswordRequirementsIcon.ForeColor = Color.FromArgb(180, 130, 20);
            this.lblPasswordRequirementsIcon.AutoSize = true;
            this.lblPasswordRequirementsIcon.Location = new Point(16, 14);
            this.lblPasswordRequirementsIcon.Name = "lblPasswordRequirementsIcon";

            this.lblPasswordRequirementsTitle.Text = "Password Requirements:";
            this.lblPasswordRequirementsTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblPasswordRequirementsTitle.ForeColor = Color.FromArgb(120, 84, 8);
            this.lblPasswordRequirementsTitle.AutoSize = true;
            this.lblPasswordRequirementsTitle.Location = new Point(36, 14);
            this.lblPasswordRequirementsTitle.Name = "lblPasswordRequirementsTitle";

            this.lblPasswordRequirementsBullets.Text =
                "\u2022  Minimum 6 characters\r\n" +
                "\u2022  Must match confirmation\r\n" +
                "\u2022  Current password required for verification";
            this.lblPasswordRequirementsBullets.Font = new Font("Segoe UI", 8.5F);
            this.lblPasswordRequirementsBullets.ForeColor = Color.FromArgb(120, 84, 8);
            this.lblPasswordRequirementsBullets.AutoSize = true;
            this.lblPasswordRequirementsBullets.Location = new Point(36, 38);
            this.lblPasswordRequirementsBullets.Name = "lblPasswordRequirementsBullets";

            // ============================================================ Hardware Status card
            this.panelHardwareStatusCard.BackColor = Color.White;
            this.panelHardwareStatusCard.BorderStyle = BorderStyle.FixedSingle;
            this.panelHardwareStatusCard.Padding = new Padding(24);
            this.panelHardwareStatusCard.Width = 1360;
            this.panelHardwareStatusCard.Height = 140;
            this.panelHardwareStatusCard.Name = "panelHardwareStatusCard";
            this.panelHardwareStatusCard.Controls.Add(this.tableLayoutHardwareStatusItems);
            this.panelHardwareStatusCard.Controls.Add(this.flowLayoutHardwareStatusHeader);

            this.flowLayoutHardwareStatusHeader.Dock = DockStyle.Top;
            this.flowLayoutHardwareStatusHeader.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutHardwareStatusHeader.AutoSize = true;
            this.flowLayoutHardwareStatusHeader.WrapContents = false;
            this.flowLayoutHardwareStatusHeader.Margin = new Padding(0, 0, 0, 20);
            this.flowLayoutHardwareStatusHeader.Name = "flowLayoutHardwareStatusHeader";
            this.flowLayoutHardwareStatusHeader.Controls.Add(this.lblHardwareStatusIcon);
            this.flowLayoutHardwareStatusHeader.Controls.Add(this.lblHardwareStatusTitle);

            this.lblHardwareStatusIcon.Text = "\u2933";
            this.lblHardwareStatusIcon.Font = new Font("Segoe UI", 11F);
            this.lblHardwareStatusIcon.ForeColor = Color.FromArgb(22, 163, 74);
            this.lblHardwareStatusIcon.AutoSize = true;
            this.lblHardwareStatusIcon.Margin = new Padding(0, 3, 8, 0);
            this.lblHardwareStatusIcon.Name = "lblHardwareStatusIcon";

            this.lblHardwareStatusTitle.Text = "Hardware Status";
            this.lblHardwareStatusTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblHardwareStatusTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblHardwareStatusTitle.AutoSize = true;
            this.lblHardwareStatusTitle.Name = "lblHardwareStatusTitle";

            this.tableLayoutHardwareStatusItems.Dock = DockStyle.Top;
            this.tableLayoutHardwareStatusItems.AutoSize = true;
            this.tableLayoutHardwareStatusItems.ColumnCount = 4;
            this.tableLayoutHardwareStatusItems.RowCount = 1;
            this.tableLayoutHardwareStatusItems.Name = "tableLayoutHardwareStatusItems";
            this.tableLayoutHardwareStatusItems.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutHardwareStatusItems.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutHardwareStatusItems.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutHardwareStatusItems.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutHardwareStatusItems.Controls.Add(this.flowLayoutStatusPlc, 0, 0);
            this.tableLayoutHardwareStatusItems.Controls.Add(this.flowLayoutStatusCamera, 1, 0);
            this.tableLayoutHardwareStatusItems.Controls.Add(this.flowLayoutStatusIoModule, 2, 0);
            this.tableLayoutHardwareStatusItems.Controls.Add(this.flowLayoutStatusLighting, 3, 0);

            // -- Honest default: gray dot + "Not Connected", NOT the mockup's green "Connected" --
            // this is an unwired scaffold, so claiming a live connection here would be misleading.
            // SetHardwareStatus(...) below is the intended way to reflect real status later.

            BuildStatusItem(this.flowLayoutStatusPlc, out this.lblStatusDotPlc, out this.lblStatusNamePlc, out this.lblStatusValuePlc, "PLC Controller");
            BuildStatusItem(this.flowLayoutStatusCamera, out this.lblStatusDotCamera, out this.lblStatusNameCamera, out this.lblStatusValueCamera, "Camera");
            BuildStatusItem(this.flowLayoutStatusIoModule, out this.lblStatusDotIoModule, out this.lblStatusNameIoModule, out this.lblStatusValueIoModule, "I/O Module");
            BuildStatusItem(this.flowLayoutStatusLighting, out this.lblStatusDotLighting, out this.lblStatusNameLighting, out this.lblStatusValueLighting, "Lighting");

            // ---------------------------------------------------------- SettingsControl itself
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Dock = DockStyle.Fill;
            this.Controls.Add(this.tableLayoutRoot);
            this.Name = "SettingsControl";
            this.Size = new Size(1400, 1000);

            this.tableLayoutHardwareStatusItems.ResumeLayout(false);
            this.tableLayoutHardwareStatusItems.PerformLayout();
            this.flowLayoutHardwareStatusHeader.ResumeLayout(false);
            this.flowLayoutHardwareStatusHeader.PerformLayout();
            this.panelHardwareStatusCard.ResumeLayout(false);
            this.panelPasswordRequirements.ResumeLayout(false);
            this.panelPasswordRequirements.PerformLayout();
            this.panelConfirmPasswordInput.ResumeLayout(false);
            this.panelConfirmPasswordInput.PerformLayout();
            this.panelNewPasswordInput.ResumeLayout(false);
            this.panelNewPasswordInput.PerformLayout();
            this.panelCurrentPasswordInput.ResumeLayout(false);
            this.panelCurrentPasswordInput.PerformLayout();
            this.flowLayoutChangePasswordHeader.ResumeLayout(false);
            this.flowLayoutChangePasswordHeader.PerformLayout();
            this.flowLayoutChangePassword.ResumeLayout(false);
            this.flowLayoutChangePassword.PerformLayout();
            this.panelEmailInput.ResumeLayout(false);
            this.panelEmailInput.PerformLayout();
            this.panelUsernameInput.ResumeLayout(false);
            this.panelUsernameInput.PerformLayout();
            this.flowLayoutProfileInfoHeader.ResumeLayout(false);
            this.flowLayoutProfileInfoHeader.PerformLayout();
            this.flowLayoutProfileInfo.ResumeLayout(false);
            this.flowLayoutProfileInfo.PerformLayout();
            this.tableLayoutUserSettingsColumns.ResumeLayout(false);
            this.flowLayoutUserSettingsTitleBlock.ResumeLayout(false);
            this.flowLayoutUserSettingsTitleBlock.PerformLayout();
            this.tableLayoutUserSettingsHeader.ResumeLayout(false);
            this.tableLayoutUserSettingsHeader.PerformLayout();
            this.panelUserSettingsCard.ResumeLayout(false);
            this.flowLayoutCards.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.flowLayoutHeaderRight.ResumeLayout(false);
            this.flowLayoutHeaderRight.PerformLayout();
            this.flowLayoutTitleBlock.ResumeLayout(false);
            this.flowLayoutTitleBlock.PerformLayout();
            this.panelHeaderIcon.ResumeLayout(false);
            this.tableLayoutHeader.ResumeLayout(false);
            this.tableLayoutHeader.PerformLayout();
            this.tableLayoutRoot.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Builds one "dot + name + value" hardware status item. A small helper rather than fully
        /// flat inline code, since this exact 3-control shape repeats identically four times --
        /// noted in the class-level fidelity caveat as a deviation from what VS's own designer
        /// would literally emit.
        /// </summary>
        private void BuildStatusItem(
            FlowLayoutPanel container, out Label dot, out Label name, out Label value, string deviceName)
        {
            dot = new Label
            {
                Text = "\u25CF",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(148, 163, 184),
                AutoSize = true,
                Margin = new Padding(0, 4, 6, 0)
            };

            var textBlock = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                WrapContents = false
            };

            name = new Label
            {
                Text = deviceName,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true
            };

            value = new Label
            {
                Text = "Not Connected",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                AutoSize = true
            };

            textBlock.Controls.Add(name);
            textBlock.Controls.Add(value);

            container.FlowDirection = FlowDirection.LeftToRight;
            container.AutoSize = true;
            container.WrapContents = false;
            container.Controls.Add(dot);
            container.Controls.Add(textBlock);
        }

        #endregion
    }
}