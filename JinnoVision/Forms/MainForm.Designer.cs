using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private TableLayoutPanel tableHeaderLayout;
        private Label lblBrandLeft;
        private FlowLayoutPanel panelMenu;
        private Panel panelUserAreaRight;
        private Button btnRolePill;
        private Label lblAppNameRight;
        private Button btnLogout;
        private Panel panelContent;

        private Button btnDashboard;
        private Button btnReview;
        private Button btnSetup;
        private Button btnReports;
        private Button btnSettings;
        private Button btnFolders;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.panelHeader = new System.Windows.Forms.Panel();
            this.tableHeaderLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblBrandLeft = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panelUserAreaRight = new System.Windows.Forms.Panel();
            this.btnRolePill = new System.Windows.Forms.Button();
            this.lblAppNameRight = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();

            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnReview = new System.Windows.Forms.Button();
            this.btnSetup = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnFolders = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // 
            // panelHeader
            // 
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 72;
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelHeader.Name = "panelHeader";

            // 
            // tableHeaderLayout
            // 
            this.tableHeaderLayout.Name = "tableHeaderLayout";
            this.tableHeaderLayout.Dock = DockStyle.Fill;
            this.tableHeaderLayout.RowCount = 1;
            this.tableHeaderLayout.ColumnCount = 4;
            this.tableHeaderLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            // left = auto, center = 100%, right = auto
            this.tableHeaderLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.AutoSize));
            this.tableHeaderLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 80F));
            this.tableHeaderLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.AutoSize));

            this.panelHeader.Controls.Add(this.tableHeaderLayout);

            // 
            // lblBrandLeft  (LUMENAI)
            // 
            this.lblBrandLeft.AutoSize = true;
            this.lblBrandLeft.Text = "LUMENAI";
            this.lblBrandLeft.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblBrandLeft.ForeColor = System.Drawing.Color.FromArgb(35, 55, 90);
            this.lblBrandLeft.Padding = new Padding(0, 15, 0, 0);
            this.lblBrandLeft.Dock = DockStyle.None;
            this.lblBrandLeft.Name = "lblBrandLeft";

            // 
            // panelMenu (center menu – FlowLayoutPanel)
            // 
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.FlowDirection = FlowDirection.LeftToRight;
            this.panelMenu.WrapContents = false;
            this.panelMenu.AutoSize = true;
            this.panelMenu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.panelMenu.BackColor = System.Drawing.Color.Transparent;
            this.panelMenu.Padding = new Padding(0, 15, 0, 0);
            this.panelMenu.Margin = new Padding(0);
            this.panelMenu.Dock = DockStyle.None;
            this.panelMenu.Anchor = AnchorStyles.None; // center inside middle cell

            // 
            // menu buttons (basic, styling done in MainForm.cs)
            // 
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Text = "Dashboard";

            this.btnReview.Name = "btnReview";
            this.btnReview.Text = "Review";

            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Text = "Setup";

            this.btnReports.Name = "btnReports";
            this.btnReports.Text = "Reports";

            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Text = "Settings";

            this.btnFolders.Name = "btnFolders";
            this.btnFolders.Text = "Folders";

            this.panelMenu.Controls.Add(this.btnDashboard);
            this.panelMenu.Controls.Add(this.btnReview);
            this.panelMenu.Controls.Add(this.btnSetup);
            this.panelMenu.Controls.Add(this.btnReports);
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnFolders);

            // 
            // panelUserAreaRight
            // 
            this.panelUserAreaRight.Name = "panelUserAreaRight";
            this.panelUserAreaRight.Dock = DockStyle.None;
            this.panelUserAreaRight.AutoSize = true;
            this.panelUserAreaRight.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.panelUserAreaRight.BackColor = System.Drawing.Color.Transparent;

            // 
            // btnRolePill  (ADMINISTRATOR)
            // 
            this.btnRolePill.AutoSize = true;
            this.btnRolePill.Text = "ADMINISTRATOR";
            this.btnRolePill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRolePill.FlatAppearance.BorderSize = 0;
            this.btnRolePill.BackColor = System.Drawing.Color.FromArgb(230, 240, 255);
            this.btnRolePill.ForeColor = System.Drawing.Color.FromArgb(66, 99, 144);
            this.btnRolePill.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRolePill.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.btnRolePill.Location = new System.Drawing.Point(0, 18);
            this.btnRolePill.Name = "btnRolePill";

            // 
            // lblAppNameRight  (JINNO)
            // 
            this.lblAppNameRight.AutoSize = true;
            this.lblAppNameRight.Text = "JINNO";
            this.lblAppNameRight.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblAppNameRight.ForeColor = System.Drawing.Color.FromArgb(35, 55, 90);
            this.lblAppNameRight.Location = new System.Drawing.Point(150, 18);
            this.lblAppNameRight.Name = "lblAppNameRight";

            // 
            // btnLogout
            // 
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.Text = "⮞"; // placeholder icon
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(120, 130, 150);
            this.btnLogout.Size = new System.Drawing.Size(32, 32);
            this.btnLogout.Location = new System.Drawing.Point(210, 18);
            this.btnLogout.Name = "btnLogout";

            this.panelUserAreaRight.Controls.Add(this.btnRolePill);
            this.panelUserAreaRight.Controls.Add(this.lblAppNameRight);
            this.panelUserAreaRight.Controls.Add(this.btnLogout);

            // 
            // Add controls to tableHeaderLayout (Left, Center, Right)
            // 
            this.tableHeaderLayout.Controls.Add(this.lblBrandLeft, 0, 0);
            this.tableHeaderLayout.Controls.Add(this.panelMenu, 1, 0);
            this.tableHeaderLayout.Controls.Add(this.panelUserAreaRight, 2, 0);
            this.tableHeaderLayout.Controls.Add(this.btnLogout, 3, 0);

            // 
            // panelContent
            // 
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelContent.Name = "panelContent";

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);

            this.ResumeLayout(false);
        }

        #endregion
    }
}
