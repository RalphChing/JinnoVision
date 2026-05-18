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

        private FlowLayoutPanel panelRightHeader;

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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.tableHeaderLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblBrandLeft = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnReview = new System.Windows.Forms.Button();
            this.btnSetup = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnFolders = new System.Windows.Forms.Button();
            this.panelUserAreaRight = new System.Windows.Forms.Panel();
            this.btnRolePill = new System.Windows.Forms.Button();
            this.lblAppNameRight = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.tableHeaderLayout.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelUserAreaRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.tableHeaderLayout);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelHeader.Size = new System.Drawing.Size(1200, 72);
            this.panelHeader.TabIndex = 1;
            // 
            // tableHeaderLayout
            // 
            this.tableHeaderLayout.ColumnCount = 4;
            this.tableHeaderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableHeaderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableHeaderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableHeaderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableHeaderLayout.Controls.Add(this.lblBrandLeft, 0, 0);
            this.tableHeaderLayout.Controls.Add(this.panelMenu, 1, 0);
            this.tableHeaderLayout.Controls.Add(this.panelUserAreaRight, 2, 0);
            this.tableHeaderLayout.Controls.Add(this.btnLogout, 3, 0);
            this.tableHeaderLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableHeaderLayout.Location = new System.Drawing.Point(20, 10);
            this.tableHeaderLayout.Name = "tableHeaderLayout";
            this.tableHeaderLayout.RowCount = 1;
            this.tableHeaderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableHeaderLayout.Size = new System.Drawing.Size(1160, 52);
            this.tableHeaderLayout.TabIndex = 0;
            // 
            // panelRightHeader
            // 
            this.panelRightHeader = new System.Windows.Forms.FlowLayoutPanel();
            this.panelRightHeader.AutoSize = true;
            this.panelRightHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelRightHeader.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightHeader.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelRightHeader.WrapContents = false;
            this.panelRightHeader.Padding = new System.Windows.Forms.Padding(0, 12, 20, 0);
            this.panelRightHeader.Margin = new Padding(0);
            // 
            // lblBrandLeft
            // 
            this.lblBrandLeft.AutoSize = true;
            this.lblBrandLeft.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblBrandLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(55)))), ((int)(((byte)(90)))));
            this.lblBrandLeft.Location = new System.Drawing.Point(3, 0);
            this.lblBrandLeft.Name = "lblBrandLeft";
            this.lblBrandLeft.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblBrandLeft.Size = new System.Drawing.Size(140, 52);
            this.lblBrandLeft.TabIndex = 0;
            this.lblBrandLeft.Text = "LUMENAI";
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(3, 18);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(75, 23);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            // 
            // btnReview
            // 
            this.btnReview.Location = new System.Drawing.Point(84, 18);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(75, 23);
            this.btnReview.TabIndex = 1;
            this.btnReview.Text = "Review";
            // 
            // btnSetup
            // 
            this.btnSetup.Location = new System.Drawing.Point(165, 18);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(75, 23);
            this.btnSetup.TabIndex = 2;
            this.btnSetup.Text = "Setup";
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(246, 18);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(75, 23);
            this.btnReports.TabIndex = 3;
            this.btnReports.Text = "Reports";
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(327, 18);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Settings";
            // 
            // btnFolders
            // 
            this.btnFolders.Location = new System.Drawing.Point(408, 18);
            this.btnFolders.Name = "btnFolders";
            this.btnFolders.Size = new System.Drawing.Size(75, 23);
            this.btnFolders.TabIndex = 5;
            this.btnFolders.Text = "Folders";
            // 
            // panelUserAreaRight
            // 
            this.panelUserAreaRight.AutoSize = true;
            this.panelUserAreaRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelUserAreaRight.BackColor = System.Drawing.Color.Transparent;
            this.panelUserAreaRight.Controls.Add(this.btnRolePill);
            this.panelUserAreaRight.Controls.Add(this.lblAppNameRight);
            this.panelUserAreaRight.Location = new System.Drawing.Point(885, 3);
            this.panelUserAreaRight.Name = "panelUserAreaRight";
            this.panelUserAreaRight.Size = new System.Drawing.Size(252, 46);
            this.panelUserAreaRight.TabIndex = 2;
            // 
            // btnRolePill
            // 
            this.btnRolePill.AutoSize = true;
            this.btnRolePill.BackColor = System.Drawing.Color.FromArgb(230, 240, 255);
            this.btnRolePill.FlatAppearance.BorderSize = 0;
            this.btnRolePill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRolePill.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRolePill.ForeColor = System.Drawing.Color.FromArgb(66, 99, 144);
            this.btnRolePill.Margin = new Padding(0, 0, 12, 0);
            this.btnRolePill.Name = "btnRolePill";
            this.btnRolePill.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.btnRolePill.Size = new System.Drawing.Size(196, 43);
            this.btnRolePill.TabIndex = 0;
            this.btnRolePill.Text = "ADMINISTRATOR";
            this.btnRolePill.UseVisualStyleBackColor = false;
            // 
            // lblAppNameRight
            // 
            this.lblAppNameRight.AutoSize = true;
            this.lblAppNameRight.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblAppNameRight.ForeColor = System.Drawing.Color.FromArgb(35, 55, 90);
            this.lblAppNameRight.Margin = new Padding(0, 4, 0, 0);
            this.lblAppNameRight.Name = "lblAppNameRight";
            this.lblAppNameRight.Size = new System.Drawing.Size(99, 38);
            this.lblAppNameRight.TabIndex = 1;
            this.lblAppNameRight.Text = "JINNO";
            // 
            // btnLogout
            // 
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(150)))));
            this.btnLogout.Location = new System.Drawing.Point(1143, 3);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(14, 32);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "⮞";
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 72);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1200, 628);
            this.panelContent.TabIndex = 0;
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelMenu.AutoSize = true;
            this.panelMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMenu.BackColor = System.Drawing.Color.Transparent;
            this.panelMenu.Controls.Add(this.btnDashboard);
            this.panelMenu.Controls.Add(this.btnReview);
            this.panelMenu.Controls.Add(this.btnSetup);
            this.panelMenu.Controls.Add(this.btnReports);
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnFolders);
            this.panelHeader.Controls.Add(this.panelRightHeader);
            this.panelMenu.Location = new System.Drawing.Point(271, 4);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.panelMenu.Size = new System.Drawing.Size(486, 44);
            this.panelMenu.TabIndex = 1;
            this.panelMenu.WrapContents = false;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelHeader.ResumeLayout(false);
            this.tableHeaderLayout.ResumeLayout(false);
            this.tableHeaderLayout.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.panelUserAreaRight.ResumeLayout(false);
            this.panelUserAreaRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
