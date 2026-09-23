using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    partial class ReviewControl
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
        private ComboBox cboAllResults;
        private TextBox txtSearchById;

        // -- Body: sidebar + detail --
        private TableLayoutPanel tableLayoutBody;

        private Panel panelSidebar;
        private FlowLayoutPanel flowLayoutSidebarCards;
        private Label lblSidebarEmptyState;

        private Panel panelDetail;
        private TableLayoutPanel tableLayoutDetailHeader;
        private FlowLayoutPanel flowLayoutDetailTitleBlock;
        private Label lblDetailId;
        private Label lblDetailSubtitle;
        private Panel panelDetailHeaderSpacer;
        private TableLayoutPanel tableLayoutDetailStats;
        private FlowLayoutPanel flowLayoutTimestampStat;
        private Label lblTimestampCaption;
        private Label lblTimestampValue;
        private FlowLayoutPanel flowLayoutCycleTimeStat;
        private Label lblCycleTimeCaption;
        private Label lblCycleTimeValue;
        private Label lblDetailResultPill;

        private TableLayoutPanel tableLayoutImages;

        private Panel panelReferenceImageCard;
        private Label lblReferenceImageTitle;
        private Panel panelReferenceImageViewport;
        private PictureBox picReferenceImage;
        private Label lblReferenceImagePlaceholder;
        private Button btnZoomInReference;
        private Button btnZoomOutReference;
        private Label lblReferenceZoomLevel;

        private Panel panelInspectedImageCard;
        private TableLayoutPanel tableLayoutInspectedImageHeader;
        private Label lblInspectedImageTitle;
        private Button btnExportInspectedImage;
        private Panel panelInspectedImageViewport;
        private PictureBox picInspectedImage;
        private Label lblInspectedImagePlaceholder;

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
            this.cboAllResults = new ComboBox();
            this.txtSearchById = new TextBox();

            this.tableLayoutBody = new TableLayoutPanel();

            this.panelSidebar = new Panel();
            this.flowLayoutSidebarCards = new FlowLayoutPanel();
            this.lblSidebarEmptyState = new Label();

            this.panelDetail = new Panel();
            this.tableLayoutDetailHeader = new TableLayoutPanel();
            this.flowLayoutDetailTitleBlock = new FlowLayoutPanel();
            this.lblDetailId = new Label();
            this.lblDetailSubtitle = new Label();
            this.panelDetailHeaderSpacer = new Panel();
            this.tableLayoutDetailStats = new TableLayoutPanel();
            this.flowLayoutTimestampStat = new FlowLayoutPanel();
            this.lblTimestampCaption = new Label();
            this.lblTimestampValue = new Label();
            this.flowLayoutCycleTimeStat = new FlowLayoutPanel();
            this.lblCycleTimeCaption = new Label();
            this.lblCycleTimeValue = new Label();
            this.lblDetailResultPill = new Label();

            this.tableLayoutImages = new TableLayoutPanel();

            this.panelReferenceImageCard = new Panel();
            this.lblReferenceImageTitle = new Label();
            this.panelReferenceImageViewport = new Panel();
            this.picReferenceImage = new PictureBox();
            this.lblReferenceImagePlaceholder = new Label();
            this.btnZoomInReference = new Button();
            this.btnZoomOutReference = new Button();
            this.lblReferenceZoomLevel = new Label();

            this.panelInspectedImageCard = new Panel();
            this.tableLayoutInspectedImageHeader = new TableLayoutPanel();
            this.lblInspectedImageTitle = new Label();
            this.btnExportInspectedImage = new Button();
            this.panelInspectedImageViewport = new Panel();
            this.picInspectedImage = new PictureBox();
            this.lblInspectedImagePlaceholder = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.picReferenceImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInspectedImage)).BeginInit();

            this.tableLayoutRoot.SuspendLayout();
            this.tableLayoutHeader.SuspendLayout();
            this.panelHeaderIcon.SuspendLayout();
            this.flowLayoutTitleBlock.SuspendLayout();
            this.flowLayoutHeaderRight.SuspendLayout();
            this.tableLayoutBody.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.flowLayoutSidebarCards.SuspendLayout();
            this.panelDetail.SuspendLayout();
            this.tableLayoutDetailHeader.SuspendLayout();
            this.flowLayoutDetailTitleBlock.SuspendLayout();
            this.tableLayoutDetailStats.SuspendLayout();
            this.flowLayoutTimestampStat.SuspendLayout();
            this.flowLayoutCycleTimeStat.SuspendLayout();
            this.tableLayoutImages.SuspendLayout();
            this.panelReferenceImageCard.SuspendLayout();
            this.panelReferenceImageViewport.SuspendLayout();
            this.panelInspectedImageCard.SuspendLayout();
            this.tableLayoutInspectedImageHeader.SuspendLayout();
            this.panelInspectedImageViewport.SuspendLayout();
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
            this.tableLayoutRoot.Controls.Add(this.tableLayoutBody, 0, 1);

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

            this.lblHeaderIconGlyph.Text = "\u25A6";
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

            this.lblPageTitle.Text = "Defect Review & Analysis";
            this.lblPageTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblPageTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Name = "lblPageTitle";

            this.lblPageSubtitle.Text = "Inspect and analyze detected defects";
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
            this.flowLayoutHeaderRight.Controls.Add(this.cboAllResults);
            this.flowLayoutHeaderRight.Controls.Add(this.txtSearchById);

            this.cboAllResults.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboAllResults.Width = 140;
            this.cboAllResults.Margin = new Padding(0, 8, 12, 0);
            this.cboAllResults.Items.AddRange(new object[] { "All Results", "Pass Only", "Fail Only" });
            this.cboAllResults.SelectedIndex = 0;
            this.cboAllResults.Name = "cboAllResults";
            this.cboAllResults.TabIndex = 0;

            this.txtSearchById.Width = 220;
            this.txtSearchById.Text = "Search by ID...";
            this.txtSearchById.ForeColor = Color.FromArgb(100, 116, 139);
            this.txtSearchById.Margin = new Padding(0, 8, 0, 0);
            this.txtSearchById.Name = "txtSearchById";
            this.txtSearchById.TabIndex = 1;
            this.txtSearchById.Enter += new System.EventHandler(this.TxtSearchById_Enter);
            this.txtSearchById.Leave += new System.EventHandler(this.TxtSearchById_Leave);

            // ---------------------------------------------------------- Body (sidebar + detail)
            this.tableLayoutBody.Dock = DockStyle.Fill;
            this.tableLayoutBody.ColumnCount = 2;
            this.tableLayoutBody.RowCount = 1;
            this.tableLayoutBody.BackColor = Color.FromArgb(245, 247, 250);
            this.tableLayoutBody.Name = "tableLayoutBody";
            this.tableLayoutBody.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F));
            this.tableLayoutBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutBody.Controls.Add(this.panelSidebar, 0, 0);
            this.tableLayoutBody.Controls.Add(this.panelDetail, 1, 0);

            // ---------------------------------------------------------- Sidebar
            this.panelSidebar.Dock = DockStyle.Fill;
            this.panelSidebar.BackColor = Color.FromArgb(245, 247, 250);
            this.panelSidebar.Padding = new Padding(0, 0, 16, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Controls.Add(this.flowLayoutSidebarCards);

            this.flowLayoutSidebarCards.Dock = DockStyle.Fill;
            this.flowLayoutSidebarCards.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutSidebarCards.WrapContents = false;
            this.flowLayoutSidebarCards.AutoScroll = true;
            this.flowLayoutSidebarCards.BackColor = Color.FromArgb(245, 247, 250);
            this.flowLayoutSidebarCards.Name = "flowLayoutSidebarCards";
            this.flowLayoutSidebarCards.Controls.Add(this.lblSidebarEmptyState);

            // Empty-state placeholder -- ClearInspectionList()/AddInspectionCard(...) manage this
            // the same way ReportControl's history panel does.
            this.lblSidebarEmptyState.Text = "No inspections to review yet.";
            this.lblSidebarEmptyState.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblSidebarEmptyState.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblSidebarEmptyState.AutoSize = true;
            this.lblSidebarEmptyState.Margin = new Padding(4, 16, 0, 0);
            this.lblSidebarEmptyState.Name = "lblSidebarEmptyState";

            // ---------------------------------------------------------- Detail panel
            this.panelDetail.Dock = DockStyle.Fill;
            this.panelDetail.BackColor = Color.FromArgb(245, 247, 250);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Controls.Add(this.tableLayoutImages);
            this.panelDetail.Controls.Add(this.tableLayoutDetailHeader);

            this.tableLayoutDetailHeader.Dock = DockStyle.Top;
            this.tableLayoutDetailHeader.AutoSize = true;
            this.tableLayoutDetailHeader.ColumnCount = 3;
            this.tableLayoutDetailHeader.RowCount = 1;
            this.tableLayoutDetailHeader.BackColor = Color.White;
            this.tableLayoutDetailHeader.Padding = new Padding(20);
            this.tableLayoutDetailHeader.Margin = new Padding(0, 0, 0, 16);
            this.tableLayoutDetailHeader.Name = "tableLayoutDetailHeader";
            this.tableLayoutDetailHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutDetailHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutDetailHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutDetailHeader.Controls.Add(this.flowLayoutDetailTitleBlock, 0, 0);
            this.tableLayoutDetailHeader.Controls.Add(this.panelDetailHeaderSpacer, 1, 0);
            this.tableLayoutDetailHeader.Controls.Add(this.tableLayoutDetailStats, 2, 0);

            this.flowLayoutDetailTitleBlock.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutDetailTitleBlock.AutoSize = true;
            this.flowLayoutDetailTitleBlock.WrapContents = false;
            this.flowLayoutDetailTitleBlock.BackColor = Color.White;
            this.flowLayoutDetailTitleBlock.Name = "flowLayoutDetailTitleBlock";
            this.flowLayoutDetailTitleBlock.Controls.Add(this.lblDetailId);
            this.flowLayoutDetailTitleBlock.Controls.Add(this.lblDetailSubtitle);

            this.lblDetailId.Text = "No Inspection Selected";
            this.lblDetailId.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.lblDetailId.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblDetailId.AutoSize = true;
            this.lblDetailId.Name = "lblDetailId";

            this.lblDetailSubtitle.Text = "Select an inspection from the list";
            this.lblDetailSubtitle.Font = new Font("Segoe UI", 9.5F);
            this.lblDetailSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblDetailSubtitle.AutoSize = true;
            this.lblDetailSubtitle.Margin = new Padding(0, 2, 0, 0);
            this.lblDetailSubtitle.Name = "lblDetailSubtitle";

            this.panelDetailHeaderSpacer.BackColor = Color.White;
            this.panelDetailHeaderSpacer.Height = 1;
            this.panelDetailHeaderSpacer.Name = "panelDetailHeaderSpacer";

             //this.tableLayoutDetailStats.FlowDirection = FlowDirection.LeftToRight;
            this.tableLayoutDetailStats.AutoSize = true;
            this.tableLayoutDetailStats.ColumnCount = 3;
            this.tableLayoutDetailStats.RowCount = 1;
            this.tableLayoutDetailStats.BackColor = Color.White;
            this.tableLayoutDetailStats.Name = "tableLayoutDetailStats";
            this.tableLayoutDetailStats.Controls.Add(this.flowLayoutTimestampStat, 0, 0);
            this.tableLayoutDetailStats.Controls.Add(this.flowLayoutCycleTimeStat, 1, 0);
            this.tableLayoutDetailStats.Controls.Add(this.lblDetailResultPill, 2, 0);

            this.flowLayoutTimestampStat.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutTimestampStat.AutoSize = true;
            this.flowLayoutTimestampStat.WrapContents = false;
            this.flowLayoutTimestampStat.BackColor = Color.White;
            this.flowLayoutTimestampStat.Margin = new Padding(0, 0, 24, 0);
            this.flowLayoutTimestampStat.Name = "flowLayoutTimestampStat";
            this.flowLayoutTimestampStat.Controls.Add(this.lblTimestampCaption);
            this.flowLayoutTimestampStat.Controls.Add(this.lblTimestampValue);

            this.lblTimestampCaption.Text = "Timestamp";
            this.lblTimestampCaption.Font = new Font("Segoe UI", 8.5F);
            this.lblTimestampCaption.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTimestampCaption.AutoSize = true;
            this.lblTimestampCaption.Name = "lblTimestampCaption";

            this.lblTimestampValue.Text = "--";
            this.lblTimestampValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblTimestampValue.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblTimestampValue.AutoSize = true;
            this.lblTimestampValue.Name = "lblTimestampValue";

            this.flowLayoutCycleTimeStat.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutCycleTimeStat.AutoSize = true;
            this.flowLayoutCycleTimeStat.WrapContents = false;
            this.flowLayoutCycleTimeStat.BackColor = Color.White;
            this.flowLayoutCycleTimeStat.Margin = new Padding(0, 0, 24, 0);
            this.flowLayoutCycleTimeStat.Name = "flowLayoutCycleTimeStat";
            this.flowLayoutCycleTimeStat.Controls.Add(this.lblCycleTimeCaption);
            this.flowLayoutCycleTimeStat.Controls.Add(this.lblCycleTimeValue);

            this.lblCycleTimeCaption.Text = "Cycle Time";
            this.lblCycleTimeCaption.Font = new Font("Segoe UI", 8.5F);
            this.lblCycleTimeCaption.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblCycleTimeCaption.AutoSize = true;
            this.lblCycleTimeCaption.Name = "lblCycleTimeCaption";

            this.lblCycleTimeValue.Text = "--";
            this.lblCycleTimeValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblCycleTimeValue.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblCycleTimeValue.AutoSize = true;
            this.lblCycleTimeValue.Name = "lblCycleTimeValue";

            this.lblDetailResultPill.Text = "N/A";
            this.lblDetailResultPill.BackColor = Color.FromArgb(148, 163, 184);
            this.lblDetailResultPill.ForeColor = Color.White;
            this.lblDetailResultPill.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDetailResultPill.AutoSize = true;
            this.lblDetailResultPill.Padding = new Padding(14, 6, 14, 6);
            this.lblDetailResultPill.Anchor = AnchorStyles.None;
            this.lblDetailResultPill.TextAlign = ContentAlignment.MiddleCenter;
            this.lblDetailResultPill.Name = "lblDetailResultPill";

            // ---------------------------------------------------------- Reference / Inspected images
            this.tableLayoutImages.Dock = DockStyle.Fill;
            this.tableLayoutImages.ColumnCount = 2;
            this.tableLayoutImages.RowCount = 1;
            this.tableLayoutImages.BackColor = Color.FromArgb(245, 247, 250);
            this.tableLayoutImages.Name = "tableLayoutImages";
            this.tableLayoutImages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutImages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutImages.Controls.Add(this.panelReferenceImageCard, 0, 0);
            this.tableLayoutImages.Controls.Add(this.panelInspectedImageCard, 1, 0);

            // -- Reference Image (Golden Sample) --
            this.panelReferenceImageCard.Dock = DockStyle.Fill;
            this.panelReferenceImageCard.BackColor = Color.White;
            this.panelReferenceImageCard.Padding = new Padding(20);
            this.panelReferenceImageCard.Margin = new Padding(0, 0, 16, 0);
            this.panelReferenceImageCard.BorderStyle = BorderStyle.FixedSingle;
            this.panelReferenceImageCard.Name = "panelReferenceImageCard";
            this.panelReferenceImageCard.Controls.Add(this.panelReferenceImageViewport);
            this.panelReferenceImageCard.Controls.Add(this.lblReferenceImageTitle);

            this.lblReferenceImageTitle.Text = "Reference Image (Golden Sample)";
            this.lblReferenceImageTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblReferenceImageTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblReferenceImageTitle.AutoSize = true;
            this.lblReferenceImageTitle.Dock = DockStyle.Top;
            this.lblReferenceImageTitle.Padding = new Padding(0, 0, 0, 12);
            this.lblReferenceImageTitle.Name = "lblReferenceImageTitle";

            this.panelReferenceImageViewport.Dock = DockStyle.Fill;
            this.panelReferenceImageViewport.BackColor = Color.FromArgb(226, 232, 240);
            this.panelReferenceImageViewport.Name = "panelReferenceImageViewport";
            this.panelReferenceImageViewport.Controls.Add(this.lblReferenceImagePlaceholder);
            this.panelReferenceImageViewport.Controls.Add(this.picReferenceImage);
            this.panelReferenceImageViewport.Controls.Add(this.lblReferenceZoomLevel);
            this.panelReferenceImageViewport.Controls.Add(this.btnZoomOutReference);
            this.panelReferenceImageViewport.Controls.Add(this.btnZoomInReference);

            this.picReferenceImage.Dock = DockStyle.Fill;
            this.picReferenceImage.BackColor = Color.FromArgb(226, 232, 240);
            this.picReferenceImage.SizeMode = PictureBoxSizeMode.Zoom;
            this.picReferenceImage.Name = "picReferenceImage";
            // No Image assigned -- placeholder label below shows through until a real golden
            // sample image is loaded.

            this.lblReferenceImagePlaceholder.Text = "Reference PCB Image";
            this.lblReferenceImagePlaceholder.Font = new Font("Segoe UI", 10F);
            this.lblReferenceImagePlaceholder.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblReferenceImagePlaceholder.AutoSize = true;
            this.lblReferenceImagePlaceholder.Anchor = AnchorStyles.None;
            this.lblReferenceImagePlaceholder.Name = "lblReferenceImagePlaceholder";
            this.lblReferenceImagePlaceholder.BringToFront();

            this.btnZoomInReference.Text = "+";
            this.btnZoomInReference.FlatStyle = FlatStyle.Flat;
            this.btnZoomInReference.BackColor = Color.White;
            this.btnZoomInReference.Size = new Size(32, 32);
            this.btnZoomInReference.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnZoomInReference.Name = "btnZoomInReference";
            this.btnZoomInReference.Click += new System.EventHandler(this.BtnZoomInReference_Click);
            this.btnZoomInReference.BringToFront();

            this.btnZoomOutReference.Text = "\u2212";
            this.btnZoomOutReference.FlatStyle = FlatStyle.Flat;
            this.btnZoomOutReference.BackColor = Color.White;
            this.btnZoomOutReference.Size = new Size(32, 32);
            this.btnZoomOutReference.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnZoomOutReference.Name = "btnZoomOutReference";
            this.btnZoomOutReference.Click += new System.EventHandler(this.BtnZoomOutReference_Click);
            this.btnZoomOutReference.BringToFront();

            this.lblReferenceZoomLevel.Text = "100%";
            this.lblReferenceZoomLevel.BackColor = Color.White;
            this.lblReferenceZoomLevel.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblReferenceZoomLevel.Font = new Font("Segoe UI", 8.5F);
            this.lblReferenceZoomLevel.AutoSize = true;
            this.lblReferenceZoomLevel.Padding = new Padding(8, 4, 8, 4);
            this.lblReferenceZoomLevel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lblReferenceZoomLevel.Name = "lblReferenceZoomLevel";
            this.lblReferenceZoomLevel.BringToFront();

            // -- Inspected Image --
            this.panelInspectedImageCard.Dock = DockStyle.Fill;
            this.panelInspectedImageCard.BackColor = Color.White;
            this.panelInspectedImageCard.Padding = new Padding(20);
            this.panelInspectedImageCard.BorderStyle = BorderStyle.FixedSingle;
            this.panelInspectedImageCard.Name = "panelInspectedImageCard";
            this.panelInspectedImageCard.Controls.Add(this.panelInspectedImageViewport);
            this.panelInspectedImageCard.Controls.Add(this.tableLayoutInspectedImageHeader);

            this.tableLayoutInspectedImageHeader.Dock = DockStyle.Top;
            this.tableLayoutInspectedImageHeader.AutoSize = true;
            this.tableLayoutInspectedImageHeader.ColumnCount = 2;
            this.tableLayoutInspectedImageHeader.RowCount = 1;
            this.tableLayoutInspectedImageHeader.Padding = new Padding(0, 0, 0, 12);
            this.tableLayoutInspectedImageHeader.Name = "tableLayoutInspectedImageHeader";
            this.tableLayoutInspectedImageHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutInspectedImageHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutInspectedImageHeader.Controls.Add(this.lblInspectedImageTitle, 0, 0);
            this.tableLayoutInspectedImageHeader.Controls.Add(this.btnExportInspectedImage, 1, 0);

            this.lblInspectedImageTitle.Text = "Inspected Image";
            this.lblInspectedImageTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblInspectedImageTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblInspectedImageTitle.AutoSize = true;
            this.lblInspectedImageTitle.Anchor = AnchorStyles.Left;
            this.lblInspectedImageTitle.Name = "lblInspectedImageTitle";

            this.btnExportInspectedImage.Text = "\u2B07  Export";
            this.btnExportInspectedImage.FlatStyle = FlatStyle.Flat;
            this.btnExportInspectedImage.BackColor = Color.White;
            this.btnExportInspectedImage.ForeColor = Color.FromArgb(30, 41, 59);
            this.btnExportInspectedImage.AutoSize = true;
            this.btnExportInspectedImage.Padding = new Padding(8, 4, 8, 4);
            this.btnExportInspectedImage.Anchor = AnchorStyles.Right;
            this.btnExportInspectedImage.Name = "btnExportInspectedImage";
            this.btnExportInspectedImage.Click += new System.EventHandler(this.BtnExportInspectedImage_Click);

            this.panelInspectedImageViewport.Dock = DockStyle.Fill;
            this.panelInspectedImageViewport.BackColor = Color.FromArgb(226, 232, 240);
            this.panelInspectedImageViewport.Name = "panelInspectedImageViewport";
            this.panelInspectedImageViewport.Controls.Add(this.lblInspectedImagePlaceholder);
            this.panelInspectedImageViewport.Controls.Add(this.picInspectedImage);

            this.picInspectedImage.Dock = DockStyle.Fill;
            this.picInspectedImage.BackColor = Color.FromArgb(226, 232, 240);
            this.picInspectedImage.SizeMode = PictureBoxSizeMode.Zoom;
            this.picInspectedImage.Name = "picInspectedImage";
            // Paint handler draws defect marker overlays (rectangle + label pill) once real
            // defect data is available -- empty list by default, so it renders nothing extra yet.
            this.picInspectedImage.Paint += new PaintEventHandler(this.PicInspectedImage_Paint);

            this.lblInspectedImagePlaceholder.Text = "Inspected PCB Image";
            this.lblInspectedImagePlaceholder.Font = new Font("Segoe UI", 10F);
            this.lblInspectedImagePlaceholder.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblInspectedImagePlaceholder.AutoSize = true;
            this.lblInspectedImagePlaceholder.Anchor = AnchorStyles.None;
            this.lblInspectedImagePlaceholder.Name = "lblInspectedImagePlaceholder";
            this.lblInspectedImagePlaceholder.BringToFront();

            // ---------------------------------------------------------- ReviewControl itself
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Dock = DockStyle.Fill;
            this.Controls.Add(this.tableLayoutRoot);
            this.Name = "ReviewControl";
            this.Size = new Size(1400, 1000);

            ((System.ComponentModel.ISupportInitialize)(this.picReferenceImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInspectedImage)).EndInit();

            this.panelInspectedImageViewport.ResumeLayout(false);
            this.panelInspectedImageViewport.PerformLayout();
            this.tableLayoutInspectedImageHeader.ResumeLayout(false);
            this.tableLayoutInspectedImageHeader.PerformLayout();
            this.panelInspectedImageCard.ResumeLayout(false);
            this.panelReferenceImageViewport.ResumeLayout(false);
            this.panelReferenceImageViewport.PerformLayout();
            this.panelReferenceImageCard.ResumeLayout(false);
            this.tableLayoutImages.ResumeLayout(false);
            this.flowLayoutCycleTimeStat.ResumeLayout(false);
            this.flowLayoutCycleTimeStat.PerformLayout();
            this.flowLayoutTimestampStat.ResumeLayout(false);
            this.flowLayoutTimestampStat.PerformLayout();
            this.tableLayoutDetailStats.ResumeLayout(false);
            this.tableLayoutDetailStats.PerformLayout();
            this.flowLayoutDetailTitleBlock.ResumeLayout(false);
            this.flowLayoutDetailTitleBlock.PerformLayout();
            this.tableLayoutDetailHeader.ResumeLayout(false);
            this.tableLayoutDetailHeader.PerformLayout();
            this.panelDetail.ResumeLayout(false);
            this.flowLayoutSidebarCards.ResumeLayout(false);
            this.flowLayoutSidebarCards.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.tableLayoutBody.ResumeLayout(false);
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

        #endregion
    }
}