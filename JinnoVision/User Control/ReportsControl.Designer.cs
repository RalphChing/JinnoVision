using System.Drawing;
using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;

namespace JinnoVision.User_Control
{
    partial class ReportsControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        // -- Root layout --
        private TableLayoutPanel tableLayoutRoot;

        // -- Header --
        private TableLayoutPanel tableLayoutHeader;
        private FlowLayoutPanel flowLayoutTitleBlock;
        private Label lblPageTitle;
        private Label lblPageSubtitle;
        private Panel panelHeaderSpacer;
        private FlowLayoutPanel flowLayoutHeaderRight;
        private ComboBox cboPeriod;
        private Button btnExportReport;

        // -- KPI cards row --
        private TableLayoutPanel tableLayoutKpiRow;

        private Panel panelKpiTotalInspections;
        private Label lblTotalInspectionsIcon;
        private Label lblTotalInspectionsTitle;
        private Label lblTotalInspectionsValue;
        private Label lblTotalInspectionsDelta;

        private Panel panelKpiYieldRate;
        private Label lblYieldRateIcon;
        private Label lblYieldRateTitle;
        private Label lblYieldRateValue;
        private Label lblYieldRateDelta;

        private Panel panelKpiTotalDefects;
        private Label lblTotalDefectsIcon;
        private Label lblTotalDefectsTitle;
        private Label lblTotalDefectsValue;
        private Label lblTotalDefectsDelta;

        private Panel panelKpiAvgCycleTime;
        private Label lblAvgCycleTimeIcon;
        private Label lblAvgCycleTimeTitle;
        private Label lblAvgCycleTimeValue;
        private Label lblAvgCycleTimeDelta;

        // -- Charts row 1 --
        private TableLayoutPanel tableLayoutChartsRowOne;

        private Panel panelYieldTrendCard;
        private Label lblYieldTrendTitle;
        //private Chart chartYieldTrend;
        //private ChartArea chartAreaYieldTrend;
        //private Series seriesYieldTrend;

        private Panel panelParetoCard;
        private Label lblParetoTitle;
        //private Chart chartParetoAnalysis;
        //private ChartArea chartAreaPareto;
        //private Series seriesPareto;

        // -- Charts row 2 --
        private TableLayoutPanel tableLayoutChartsRowTwo;

        private Panel panelDistributionCard;
        private Label lblDistributionTitle;
        //private Chart chartDefectDistribution;
        //private ChartArea chartAreaDistribution;
        //private Series seriesDistribution;
        //private Legend legendDistribution;

        private Panel panelHistoryCard;
        private TableLayoutPanel tableLayoutHistoryHeader;
        private Label lblHistoryTitle;
        private TextBox txtSearchById;
        private TableLayoutPanel tableLayoutHistoryColumnHeaders;
        private Label lblColHeaderResultId;
        private Label lblColHeaderDefects;
        private Label lblColHeaderCycleTime;
        private Label lblColHeaderTimestamp;
        private FlowLayoutPanel flowLayoutHistoryRows;
        private Label lblHistoryEmptyState;

        /// <summary>
        /// Required method for Designer support - do not modify the contents of this method
        /// with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutRoot = new TableLayoutPanel();

            this.tableLayoutHeader = new TableLayoutPanel();
            this.flowLayoutTitleBlock = new FlowLayoutPanel();
            this.lblPageTitle = new Label();
            this.lblPageSubtitle = new Label();
            this.panelHeaderSpacer = new Panel();
            this.flowLayoutHeaderRight = new FlowLayoutPanel();
            this.cboPeriod = new ComboBox();
            this.btnExportReport = new Button();

            this.tableLayoutKpiRow = new TableLayoutPanel();

            this.panelKpiTotalInspections = new Panel();
            this.lblTotalInspectionsIcon = new Label();
            this.lblTotalInspectionsTitle = new Label();
            this.lblTotalInspectionsValue = new Label();
            this.lblTotalInspectionsDelta = new Label();

            this.panelKpiYieldRate = new Panel();
            this.lblYieldRateIcon = new Label();
            this.lblYieldRateTitle = new Label();
            this.lblYieldRateValue = new Label();
            this.lblYieldRateDelta = new Label();

            this.panelKpiTotalDefects = new Panel();
            this.lblTotalDefectsIcon = new Label();
            this.lblTotalDefectsTitle = new Label();
            this.lblTotalDefectsValue = new Label();
            this.lblTotalDefectsDelta = new Label();

            this.panelKpiAvgCycleTime = new Panel();
            this.lblAvgCycleTimeIcon = new Label();
            this.lblAvgCycleTimeTitle = new Label();
            this.lblAvgCycleTimeValue = new Label();
            this.lblAvgCycleTimeDelta = new Label();

            this.tableLayoutChartsRowOne = new TableLayoutPanel();

            this.panelYieldTrendCard = new Panel();
            this.lblYieldTrendTitle = new Label();
            //this.chartYieldTrend = new Chart();
            //this.chartAreaYieldTrend = new ChartArea();
            //this.seriesYieldTrend = new Series();

            this.panelParetoCard = new Panel();
            this.lblParetoTitle = new Label();
            //this.chartParetoAnalysis = new Chart();
            //this.chartAreaPareto = new ChartArea();
            //this.seriesPareto = new Series();

            this.tableLayoutChartsRowTwo = new TableLayoutPanel();

            this.panelDistributionCard = new Panel();
            this.lblDistributionTitle = new Label();
            //this.chartDefectDistribution = new Chart();
            //this.chartAreaDistribution = new ChartArea();
            //this.seriesDistribution = new Series();
            //this.legendDistribution = new Legend();

            this.panelHistoryCard = new Panel();
            this.tableLayoutHistoryHeader = new TableLayoutPanel();
            this.lblHistoryTitle = new Label();
            this.txtSearchById = new TextBox();
            this.tableLayoutHistoryColumnHeaders = new TableLayoutPanel();
            this.lblColHeaderResultId = new Label();
            this.lblColHeaderDefects = new Label();
            this.lblColHeaderCycleTime = new Label();
            this.lblColHeaderTimestamp = new Label();
            this.flowLayoutHistoryRows = new FlowLayoutPanel();
            this.lblHistoryEmptyState = new Label();

            //((System.ComponentModel.ISupportInitialize)(this.chartYieldTrend)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.chartParetoAnalysis)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.chartDefectDistribution)).BeginInit();

            this.tableLayoutRoot.SuspendLayout();
            this.tableLayoutHeader.SuspendLayout();
            this.flowLayoutTitleBlock.SuspendLayout();
            this.flowLayoutHeaderRight.SuspendLayout();
            this.tableLayoutKpiRow.SuspendLayout();
            this.panelKpiTotalInspections.SuspendLayout();
            this.panelKpiYieldRate.SuspendLayout();
            this.panelKpiTotalDefects.SuspendLayout();
            this.panelKpiAvgCycleTime.SuspendLayout();
            this.tableLayoutChartsRowOne.SuspendLayout();
            this.panelYieldTrendCard.SuspendLayout();
            this.panelParetoCard.SuspendLayout();
            this.tableLayoutChartsRowTwo.SuspendLayout();
            this.panelDistributionCard.SuspendLayout();
            this.panelHistoryCard.SuspendLayout();
            this.tableLayoutHistoryHeader.SuspendLayout();
            this.tableLayoutHistoryColumnHeaders.SuspendLayout();
            this.flowLayoutHistoryRows.SuspendLayout();
            this.SuspendLayout();

            // ---------------------------------------------------------- tableLayoutRoot
            this.tableLayoutRoot.Dock = DockStyle.Fill;
            this.tableLayoutRoot.ColumnCount = 1;
            this.tableLayoutRoot.RowCount = 4;
            this.tableLayoutRoot.BackColor = Color.FromArgb(245, 247, 250);
            this.tableLayoutRoot.Padding = new Padding(24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.tableLayoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.tableLayoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutRoot.Controls.Add(this.tableLayoutHeader, 0, 0);
            this.tableLayoutRoot.Controls.Add(this.tableLayoutKpiRow, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.tableLayoutChartsRowOne, 0, 2);
            this.tableLayoutRoot.Controls.Add(this.tableLayoutChartsRowTwo, 0, 3);

            // ---------------------------------------------------------- Header
            this.tableLayoutHeader.Dock = DockStyle.Top;
            this.tableLayoutHeader.AutoSize = true;
            this.tableLayoutHeader.ColumnCount = 3;
            this.tableLayoutHeader.RowCount = 1;
            this.tableLayoutHeader.Margin = new Padding(0, 0, 0, 20);
            this.tableLayoutHeader.BackColor = Color.FromArgb(245, 247, 250);
            this.tableLayoutHeader.Name = "tableLayoutHeader";
            this.tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutHeader.Controls.Add(this.flowLayoutTitleBlock, 0, 0);
            this.tableLayoutHeader.Controls.Add(this.panelHeaderSpacer, 1, 0);
            this.tableLayoutHeader.Controls.Add(this.flowLayoutHeaderRight, 2, 0);

            this.flowLayoutTitleBlock.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutTitleBlock.AutoSize = true;
            this.flowLayoutTitleBlock.WrapContents = false;
            this.flowLayoutTitleBlock.BackColor = Color.FromArgb(245, 247, 250);
            this.flowLayoutTitleBlock.Name = "flowLayoutTitleBlock";
            this.flowLayoutTitleBlock.Controls.Add(this.lblPageTitle);
            this.flowLayoutTitleBlock.Controls.Add(this.lblPageSubtitle);

            this.lblPageTitle.Text = "Reports & Analytics";
            this.lblPageTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblPageTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Name = "lblPageTitle";

            this.lblPageSubtitle.Text = "View inspection trends and performance metrics";
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
            this.flowLayoutHeaderRight.Controls.Add(this.cboPeriod);
            this.flowLayoutHeaderRight.Controls.Add(this.btnExportReport);

            this.cboPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPeriod.Width = 100;
            this.cboPeriod.Margin = new Padding(0, 8, 12, 0);
            this.cboPeriod.Items.AddRange(new object[] { "Daily", "Weekly", "Monthly" });
            this.cboPeriod.Name = "cboPeriod";
            this.cboPeriod.TabIndex = 0;

            this.btnExportReport.Text = "\u2B07  Export Report";
            this.btnExportReport.BackColor = Color.FromArgb(37, 99, 235);
            this.btnExportReport.ForeColor = Color.White;
            this.btnExportReport.FlatStyle = FlatStyle.Flat;
            this.btnExportReport.FlatAppearance.BorderSize = 0;
            this.btnExportReport.AutoSize = true;
            this.btnExportReport.Padding = new Padding(10, 6, 10, 6);
            this.btnExportReport.Margin = new Padding(0, 6, 0, 0);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.TabIndex = 1;
            this.btnExportReport.UseVisualStyleBackColor = false;

            // ---------------------------------------------------------- KPI cards row
            this.tableLayoutKpiRow.Dock = DockStyle.Top;
            this.tableLayoutKpiRow.AutoSize = true;
            this.tableLayoutKpiRow.ColumnCount = 4;
            this.tableLayoutKpiRow.RowCount = 1;
            this.tableLayoutKpiRow.Margin = new Padding(0, 0, 0, 20);
            this.tableLayoutKpiRow.BackColor = Color.FromArgb(245, 247, 250);
            this.tableLayoutKpiRow.Name = "tableLayoutKpiRow";
            this.tableLayoutKpiRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutKpiRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutKpiRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutKpiRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutKpiRow.Controls.Add(this.panelKpiTotalInspections, 0, 0);
            this.tableLayoutKpiRow.Controls.Add(this.panelKpiYieldRate, 1, 0);
            this.tableLayoutKpiRow.Controls.Add(this.panelKpiTotalDefects, 2, 0);
            this.tableLayoutKpiRow.Controls.Add(this.panelKpiAvgCycleTime, 3, 0);

            // -- Total Inspections card --
            this.panelKpiTotalInspections.BackColor = Color.White;
            this.panelKpiTotalInspections.Margin = new Padding(0, 0, 16, 0);
            this.panelKpiTotalInspections.Padding = new Padding(20);
            this.panelKpiTotalInspections.Width = 280;
            this.panelKpiTotalInspections.Height = 130;
            this.panelKpiTotalInspections.BorderStyle = BorderStyle.FixedSingle;
            this.panelKpiTotalInspections.Name = "panelKpiTotalInspections";
            this.panelKpiTotalInspections.Controls.Add(this.lblTotalInspectionsIcon);
            this.panelKpiTotalInspections.Controls.Add(this.lblTotalInspectionsTitle);
            this.panelKpiTotalInspections.Controls.Add(this.lblTotalInspectionsValue);
            this.panelKpiTotalInspections.Controls.Add(this.lblTotalInspectionsDelta);

            this.lblTotalInspectionsIcon.Text = "\u2197";
            this.lblTotalInspectionsIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTotalInspectionsIcon.ForeColor = Color.FromArgb(22, 163, 74);
            this.lblTotalInspectionsIcon.AutoSize = true;
            this.lblTotalInspectionsIcon.Location = new Point(230, 12);
            this.lblTotalInspectionsIcon.Name = "lblTotalInspectionsIcon";

            this.lblTotalInspectionsTitle.Text = "Total Inspections";
            this.lblTotalInspectionsTitle.Font = new Font("Segoe UI", 9.5F);
            this.lblTotalInspectionsTitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTotalInspectionsTitle.AutoSize = true;
            this.lblTotalInspectionsTitle.Location = new Point(16, 14);
            this.lblTotalInspectionsTitle.Name = "lblTotalInspectionsTitle";

            this.lblTotalInspectionsValue.Text = "0";
            this.lblTotalInspectionsValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblTotalInspectionsValue.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblTotalInspectionsValue.AutoSize = true;
            this.lblTotalInspectionsValue.Location = new Point(16, 44);
            this.lblTotalInspectionsValue.Name = "lblTotalInspectionsValue";

            this.lblTotalInspectionsDelta.Text = "-- vs yesterday";
            this.lblTotalInspectionsDelta.Font = new Font("Segoe UI", 9F);
            this.lblTotalInspectionsDelta.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTotalInspectionsDelta.AutoSize = true;
            this.lblTotalInspectionsDelta.Location = new Point(16, 92);
            this.lblTotalInspectionsDelta.Name = "lblTotalInspectionsDelta";

            // -- Yield Rate card --
            this.panelKpiYieldRate.BackColor = Color.White;
            this.panelKpiYieldRate.Margin = new Padding(0, 0, 16, 0);
            this.panelKpiYieldRate.Padding = new Padding(20);
            this.panelKpiYieldRate.Width = 280;
            this.panelKpiYieldRate.Height = 130;
            this.panelKpiYieldRate.BorderStyle = BorderStyle.FixedSingle;
            this.panelKpiYieldRate.Name = "panelKpiYieldRate";
            this.panelKpiYieldRate.Controls.Add(this.lblYieldRateIcon);
            this.panelKpiYieldRate.Controls.Add(this.lblYieldRateTitle);
            this.panelKpiYieldRate.Controls.Add(this.lblYieldRateValue);
            this.panelKpiYieldRate.Controls.Add(this.lblYieldRateDelta);

            this.lblYieldRateIcon.Text = "\u2713";
            this.lblYieldRateIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblYieldRateIcon.ForeColor = Color.FromArgb(22, 163, 74);
            this.lblYieldRateIcon.AutoSize = true;
            this.lblYieldRateIcon.Location = new Point(230, 12);
            this.lblYieldRateIcon.Name = "lblYieldRateIcon";

            this.lblYieldRateTitle.Text = "Yield Rate";
            this.lblYieldRateTitle.Font = new Font("Segoe UI", 9.5F);
            this.lblYieldRateTitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblYieldRateTitle.AutoSize = true;
            this.lblYieldRateTitle.Location = new Point(16, 14);
            this.lblYieldRateTitle.Name = "lblYieldRateTitle";

            this.lblYieldRateValue.Text = "--%";
            this.lblYieldRateValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblYieldRateValue.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblYieldRateValue.AutoSize = true;
            this.lblYieldRateValue.Location = new Point(16, 44);
            this.lblYieldRateValue.Name = "lblYieldRateValue";

            this.lblYieldRateDelta.Text = "-- vs yesterday";
            this.lblYieldRateDelta.Font = new Font("Segoe UI", 9F);
            this.lblYieldRateDelta.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblYieldRateDelta.AutoSize = true;
            this.lblYieldRateDelta.Location = new Point(16, 92);
            this.lblYieldRateDelta.Name = "lblYieldRateDelta";

            // -- Total Defects card --
            this.panelKpiTotalDefects.BackColor = Color.White;
            this.panelKpiTotalDefects.Margin = new Padding(0, 0, 16, 0);
            this.panelKpiTotalDefects.Padding = new Padding(20);
            this.panelKpiTotalDefects.Width = 280;
            this.panelKpiTotalDefects.Height = 130;
            this.panelKpiTotalDefects.BorderStyle = BorderStyle.FixedSingle;
            this.panelKpiTotalDefects.Name = "panelKpiTotalDefects";
            this.panelKpiTotalDefects.Controls.Add(this.lblTotalDefectsIcon);
            this.panelKpiTotalDefects.Controls.Add(this.lblTotalDefectsTitle);
            this.panelKpiTotalDefects.Controls.Add(this.lblTotalDefectsValue);
            this.panelKpiTotalDefects.Controls.Add(this.lblTotalDefectsDelta);

            this.lblTotalDefectsIcon.Text = "\u2715";
            this.lblTotalDefectsIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTotalDefectsIcon.ForeColor = Color.FromArgb(220, 38, 38);
            this.lblTotalDefectsIcon.AutoSize = true;
            this.lblTotalDefectsIcon.Location = new Point(230, 12);
            this.lblTotalDefectsIcon.Name = "lblTotalDefectsIcon";

            this.lblTotalDefectsTitle.Text = "Total Defects";
            this.lblTotalDefectsTitle.Font = new Font("Segoe UI", 9.5F);
            this.lblTotalDefectsTitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTotalDefectsTitle.AutoSize = true;
            this.lblTotalDefectsTitle.Location = new Point(16, 14);
            this.lblTotalDefectsTitle.Name = "lblTotalDefectsTitle";

            this.lblTotalDefectsValue.Text = "0";
            this.lblTotalDefectsValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblTotalDefectsValue.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblTotalDefectsValue.AutoSize = true;
            this.lblTotalDefectsValue.Location = new Point(16, 44);
            this.lblTotalDefectsValue.Name = "lblTotalDefectsValue";

            this.lblTotalDefectsDelta.Text = "-- vs yesterday";
            this.lblTotalDefectsDelta.Font = new Font("Segoe UI", 9F);
            this.lblTotalDefectsDelta.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTotalDefectsDelta.AutoSize = true;
            this.lblTotalDefectsDelta.Location = new Point(16, 92);
            this.lblTotalDefectsDelta.Name = "lblTotalDefectsDelta";

            // -- Avg Cycle Time card --
            this.panelKpiAvgCycleTime.BackColor = Color.White;
            this.panelKpiAvgCycleTime.Margin = new Padding(0, 0, 16, 0);
            this.panelKpiAvgCycleTime.Padding = new Padding(20);
            this.panelKpiAvgCycleTime.Width = 280;
            this.panelKpiAvgCycleTime.Height = 130;
            this.panelKpiAvgCycleTime.BorderStyle = BorderStyle.FixedSingle;
            this.panelKpiAvgCycleTime.Name = "panelKpiAvgCycleTime";
            this.panelKpiAvgCycleTime.Controls.Add(this.lblAvgCycleTimeIcon);
            this.panelKpiAvgCycleTime.Controls.Add(this.lblAvgCycleTimeTitle);
            this.panelKpiAvgCycleTime.Controls.Add(this.lblAvgCycleTimeValue);
            this.panelKpiAvgCycleTime.Controls.Add(this.lblAvgCycleTimeDelta);

            this.lblAvgCycleTimeIcon.Text = "\u23F1";
            this.lblAvgCycleTimeIcon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblAvgCycleTimeIcon.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblAvgCycleTimeIcon.AutoSize = true;
            this.lblAvgCycleTimeIcon.Location = new Point(230, 12);
            this.lblAvgCycleTimeIcon.Name = "lblAvgCycleTimeIcon";

            this.lblAvgCycleTimeTitle.Text = "Avg Cycle Time";
            this.lblAvgCycleTimeTitle.Font = new Font("Segoe UI", 9.5F);
            this.lblAvgCycleTimeTitle.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblAvgCycleTimeTitle.AutoSize = true;
            this.lblAvgCycleTimeTitle.Location = new Point(16, 14);
            this.lblAvgCycleTimeTitle.Name = "lblAvgCycleTimeTitle";

            this.lblAvgCycleTimeValue.Text = "--s";
            this.lblAvgCycleTimeValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblAvgCycleTimeValue.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblAvgCycleTimeValue.AutoSize = true;
            this.lblAvgCycleTimeValue.Location = new Point(16, 44);
            this.lblAvgCycleTimeValue.Name = "lblAvgCycleTimeValue";

            this.lblAvgCycleTimeDelta.Text = "-- vs yesterday";
            this.lblAvgCycleTimeDelta.Font = new Font("Segoe UI", 9F);
            this.lblAvgCycleTimeDelta.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblAvgCycleTimeDelta.AutoSize = true;
            this.lblAvgCycleTimeDelta.Location = new Point(16, 92);
            this.lblAvgCycleTimeDelta.Name = "lblAvgCycleTimeDelta";

            // ---------------------------------------------------------- Charts row 1
            this.tableLayoutChartsRowOne.Dock = DockStyle.Fill;
            this.tableLayoutChartsRowOne.ColumnCount = 2;
            this.tableLayoutChartsRowOne.RowCount = 1;
            this.tableLayoutChartsRowOne.Margin = new Padding(0, 0, 0, 20);
            this.tableLayoutChartsRowOne.BackColor = Color.FromArgb(245, 247, 250);
            this.tableLayoutChartsRowOne.Name = "tableLayoutChartsRowOne";
            this.tableLayoutChartsRowOne.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            this.tableLayoutChartsRowOne.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            this.tableLayoutChartsRowOne.Controls.Add(this.panelYieldTrendCard, 0, 0);
            this.tableLayoutChartsRowOne.Controls.Add(this.panelParetoCard, 1, 0);

            this.panelYieldTrendCard.Dock = DockStyle.Fill;
            this.panelYieldTrendCard.BackColor = Color.White;
            this.panelYieldTrendCard.Padding = new Padding(20);
            this.panelYieldTrendCard.Margin = new Padding(0, 0, 16, 0);
            this.panelYieldTrendCard.BorderStyle = BorderStyle.FixedSingle;
            this.panelYieldTrendCard.Name = "panelYieldTrendCard";
            //this.panelYieldTrendCard.Controls.Add(this.chartYieldTrend);
            this.panelYieldTrendCard.Controls.Add(this.lblYieldTrendTitle);

            this.lblYieldTrendTitle.Text = "Yield Trend (24h)";
            this.lblYieldTrendTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblYieldTrendTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblYieldTrendTitle.AutoSize = true;
            this.lblYieldTrendTitle.Dock = DockStyle.Top;
            this.lblYieldTrendTitle.Padding = new Padding(0, 0, 0, 12);
            this.lblYieldTrendTitle.Name = "lblYieldTrendTitle";

            //this.chartAreaYieldTrend.Name = "main";
            //this.chartAreaYieldTrend.BackColor = Color.White;
            //this.chartAreaYieldTrend.AxisX.LineColor = Color.FromArgb(230, 232, 236);
            //this.chartAreaYieldTrend.AxisY.LineColor = Color.FromArgb(230, 232, 236);
            //this.chartAreaYieldTrend.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 232, 236);
            //this.chartAreaYieldTrend.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 232, 236);
            //this.chartAreaYieldTrend.AxisY.Title = "Yield Rate (%)";
            //this.chartAreaYieldTrend.AxisY.Minimum = 90D;
            //this.chartAreaYieldTrend.AxisY.Maximum = 100D;

            //this.seriesYieldTrend.Name = "data";
            //this.seriesYieldTrend.ChartType = SeriesChartType.Line;
            //this.seriesYieldTrend.Color = Color.FromArgb(37, 99, 235);
            //this.seriesYieldTrend.BorderWidth = 2;
            //this.seriesYieldTrend.ChartArea = "main";

            //this.chartYieldTrend.Dock = DockStyle.Fill;
            //this.chartYieldTrend.BackColor = Color.White;
            //this.chartYieldTrend.ChartAreas.Add(this.chartAreaYieldTrend);
            //this.chartYieldTrend.Series.Add(this.seriesYieldTrend);
            //this.chartYieldTrend.Name = "chartYieldTrend";
            // Empty by design -- no data points added. The chart still renders its frame/axes,
            // which is the "container" this scaffold asked for.

            this.panelParetoCard.Dock = DockStyle.Fill;
            this.panelParetoCard.BackColor = Color.White;
            this.panelParetoCard.Padding = new Padding(20);
            this.panelParetoCard.Margin = new Padding(0, 0, 16, 0);
            this.panelParetoCard.BorderStyle = BorderStyle.FixedSingle;
            this.panelParetoCard.Name = "panelParetoCard";
            //this.panelParetoCard.Controls.Add(this.chartParetoAnalysis);
            this.panelParetoCard.Controls.Add(this.lblParetoTitle);

            this.lblParetoTitle.Text = "ROI Defect Pareto Analysis";
            this.lblParetoTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblParetoTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblParetoTitle.AutoSize = true;
            this.lblParetoTitle.Dock = DockStyle.Top;
            this.lblParetoTitle.Padding = new Padding(0, 0, 0, 12);
            this.lblParetoTitle.Name = "lblParetoTitle";

            //this.chartAreaPareto.Name = "main";
            //this.chartAreaPareto.BackColor = Color.White;
            //this.chartAreaPareto.AxisX.LineColor = Color.FromArgb(230, 232, 236);
            //this.chartAreaPareto.AxisY.LineColor = Color.FromArgb(230, 232, 236);
            //this.chartAreaPareto.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 232, 236);
            //this.chartAreaPareto.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 232, 236);
            //this.chartAreaPareto.AxisX.Interval = 1D;

            //this.seriesPareto.Name = "data";
            //this.seriesPareto.ChartType = SeriesChartType.Column;
            //this.seriesPareto.Color = Color.FromArgb(37, 99, 235);
            //this.seriesPareto.ChartArea = "main";

            //this.chartParetoAnalysis.Dock = DockStyle.Fill;
            //this.chartParetoAnalysis.BackColor = Color.White;
            //this.chartParetoAnalysis.ChartAreas.Add(this.chartAreaPareto);
            //this.chartParetoAnalysis.Series.Add(this.seriesPareto);
            //this.chartParetoAnalysis.Name = "chartParetoAnalysis";
            // Empty by design -- see note above.

            // ---------------------------------------------------------- Charts row 2
            this.tableLayoutChartsRowTwo.Dock = DockStyle.Fill;
            this.tableLayoutChartsRowTwo.ColumnCount = 2;
            this.tableLayoutChartsRowTwo.RowCount = 1;
            this.tableLayoutChartsRowTwo.BackColor = Color.FromArgb(245, 247, 250);
            this.tableLayoutChartsRowTwo.Name = "tableLayoutChartsRowTwo";
            this.tableLayoutChartsRowTwo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            this.tableLayoutChartsRowTwo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            this.tableLayoutChartsRowTwo.Controls.Add(this.panelDistributionCard, 0, 0);
            this.tableLayoutChartsRowTwo.Controls.Add(this.panelHistoryCard, 1, 0);

            this.panelDistributionCard.Dock = DockStyle.Fill;
            this.panelDistributionCard.BackColor = Color.White;
            this.panelDistributionCard.Padding = new Padding(20);
            this.panelDistributionCard.Margin = new Padding(0, 0, 16, 0);
            this.panelDistributionCard.BorderStyle = BorderStyle.FixedSingle;
            this.panelDistributionCard.Name = "panelDistributionCard";
            //this.panelDistributionCard.Controls.Add(this.chartDefectDistribution);
            this.panelDistributionCard.Controls.Add(this.lblDistributionTitle);

            this.lblDistributionTitle.Text = "ROI Defect Distribution";
            this.lblDistributionTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblDistributionTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblDistributionTitle.AutoSize = true;
            this.lblDistributionTitle.Dock = DockStyle.Top;
            this.lblDistributionTitle.Padding = new Padding(0, 0, 0, 12);
            this.lblDistributionTitle.Name = "lblDistributionTitle";

            //this.legendDistribution.Name = "legend";
            //this.legendDistribution.Docking = Docking.Bottom;
            //this.legendDistribution.Font = new Font("Segoe UI", 8F);

            //this.chartAreaDistribution.Name = "main";
            //this.chartAreaDistribution.BackColor = Color.White;

            //this.seriesDistribution.Name = "data";
            //this.seriesDistribution.ChartType = SeriesChartType.Pie;
            //this.seriesDistribution.ChartArea = "main";
            //this.seriesDistribution["PieLabelStyle"] = "Outside";

            //this.chartDefectDistribution.Dock = DockStyle.Fill;
            //this.chartDefectDistribution.BackColor = Color.White;
            //this.chartDefectDistribution.ChartAreas.Add(this.chartAreaDistribution);
            //this.chartDefectDistribution.Series.Add(this.seriesDistribution);
            //this.chartDefectDistribution.Legends.Add(this.legendDistribution);
            //this.chartDefectDistribution.Name = "chartDefectDistribution";
            // Empty by design -- see note above.

            // -- Recent Inspection History card --
            this.panelHistoryCard.Dock = DockStyle.Fill;
            this.panelHistoryCard.BackColor = Color.White;
            this.panelHistoryCard.Padding = new Padding(20);
            this.panelHistoryCard.BorderStyle = BorderStyle.FixedSingle;
            this.panelHistoryCard.Name = "panelHistoryCard";
            this.panelHistoryCard.Controls.Add(this.flowLayoutHistoryRows);
            this.panelHistoryCard.Controls.Add(this.tableLayoutHistoryColumnHeaders);
            this.panelHistoryCard.Controls.Add(this.tableLayoutHistoryHeader);

            this.tableLayoutHistoryHeader.Dock = DockStyle.Top;
            this.tableLayoutHistoryHeader.AutoSize = true;
            this.tableLayoutHistoryHeader.ColumnCount = 2;
            this.tableLayoutHistoryHeader.RowCount = 1;
            this.tableLayoutHistoryHeader.Name = "tableLayoutHistoryHeader";
            this.tableLayoutHistoryHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutHistoryHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutHistoryHeader.Controls.Add(this.lblHistoryTitle, 0, 0);
            this.tableLayoutHistoryHeader.Controls.Add(this.txtSearchById, 1, 0);

            this.lblHistoryTitle.Text = "Recent Inspection History";
            this.lblHistoryTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblHistoryTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Anchor = AnchorStyles.Left;
            this.lblHistoryTitle.Name = "lblHistoryTitle";

            this.txtSearchById.Width = 180;
            this.txtSearchById.Text = "Search by ID...";
            this.txtSearchById.ForeColor = Color.FromArgb(100, 116, 139);
            this.txtSearchById.Anchor = AnchorStyles.Right;
            this.txtSearchById.Name = "txtSearchById";
            this.txtSearchById.TabIndex = 2;
            this.txtSearchById.Enter += new System.EventHandler(this.TxtSearchById_Enter);
            this.txtSearchById.Leave += new System.EventHandler(this.TxtSearchById_Leave);

            this.tableLayoutHistoryColumnHeaders.Dock = DockStyle.Top;
            this.tableLayoutHistoryColumnHeaders.AutoSize = true;
            this.tableLayoutHistoryColumnHeaders.ColumnCount = 4;
            this.tableLayoutHistoryColumnHeaders.RowCount = 1;
            this.tableLayoutHistoryColumnHeaders.Margin = new Padding(0, 16, 0, 4);
            this.tableLayoutHistoryColumnHeaders.Name = "tableLayoutHistoryColumnHeaders";
            this.tableLayoutHistoryColumnHeaders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            this.tableLayoutHistoryColumnHeaders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            this.tableLayoutHistoryColumnHeaders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            this.tableLayoutHistoryColumnHeaders.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            this.tableLayoutHistoryColumnHeaders.Controls.Add(this.lblColHeaderResultId, 0, 0);
            this.tableLayoutHistoryColumnHeaders.Controls.Add(this.lblColHeaderDefects, 1, 0);
            this.tableLayoutHistoryColumnHeaders.Controls.Add(this.lblColHeaderCycleTime, 2, 0);
            this.tableLayoutHistoryColumnHeaders.Controls.Add(this.lblColHeaderTimestamp, 3, 0);

            Font headerFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            Color headerColor = Color.FromArgb(100, 116, 139);

            this.lblColHeaderResultId.Text = "RESULT / ID";
            this.lblColHeaderResultId.Font = headerFont;
            this.lblColHeaderResultId.ForeColor = headerColor;
            this.lblColHeaderResultId.AutoSize = true;
            this.lblColHeaderResultId.Name = "lblColHeaderResultId";

            this.lblColHeaderDefects.Text = "DEFECTS";
            this.lblColHeaderDefects.Font = headerFont;
            this.lblColHeaderDefects.ForeColor = headerColor;
            this.lblColHeaderDefects.AutoSize = true;
            this.lblColHeaderDefects.Name = "lblColHeaderDefects";

            this.lblColHeaderCycleTime.Text = "CYCLE TIME";
            this.lblColHeaderCycleTime.Font = headerFont;
            this.lblColHeaderCycleTime.ForeColor = headerColor;
            this.lblColHeaderCycleTime.AutoSize = true;
            this.lblColHeaderCycleTime.Name = "lblColHeaderCycleTime";

            this.lblColHeaderTimestamp.Text = "TIMESTAMP";
            this.lblColHeaderTimestamp.Font = headerFont;
            this.lblColHeaderTimestamp.ForeColor = headerColor;
            this.lblColHeaderTimestamp.AutoSize = true;
            this.lblColHeaderTimestamp.Name = "lblColHeaderTimestamp";

            this.flowLayoutHistoryRows.Dock = DockStyle.Fill;
            this.flowLayoutHistoryRows.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutHistoryRows.WrapContents = false;
            this.flowLayoutHistoryRows.AutoScroll = true;
            this.flowLayoutHistoryRows.BackColor = Color.White;
            this.flowLayoutHistoryRows.Name = "flowLayoutHistoryRows";
            this.flowLayoutHistoryRows.Controls.Add(this.lblHistoryEmptyState);

            // Empty-state placeholder -- remove once real inspection history rows are wired in;
            // ReportControl.AddInspectionHistoryRow shows the intended per-row shape.
            this.lblHistoryEmptyState.Text = "No inspection history yet.";
            this.lblHistoryEmptyState.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblHistoryEmptyState.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblHistoryEmptyState.AutoSize = true;
            this.lblHistoryEmptyState.Margin = new Padding(4, 16, 0, 0);
            this.lblHistoryEmptyState.Name = "lblHistoryEmptyState";

            // ---------------------------------------------------------- ReportControl itself
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Dock = DockStyle.Fill;
            this.Controls.Add(this.tableLayoutRoot);
            this.Name = "ReportControl";
            this.Size = new Size(1400, 1000);

            //((System.ComponentModel.ISupportInitialize)(this.chartYieldTrend)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.chartParetoAnalysis)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.chartDefectDistribution)).EndInit();

            this.flowLayoutHistoryRows.ResumeLayout(false);
            this.flowLayoutHistoryRows.PerformLayout();
            this.tableLayoutHistoryColumnHeaders.ResumeLayout(false);
            this.tableLayoutHistoryColumnHeaders.PerformLayout();
            this.tableLayoutHistoryHeader.ResumeLayout(false);
            this.tableLayoutHistoryHeader.PerformLayout();
            this.panelHistoryCard.ResumeLayout(false);
            this.panelDistributionCard.ResumeLayout(false);
            this.tableLayoutChartsRowTwo.ResumeLayout(false);
            this.panelParetoCard.ResumeLayout(false);
            this.panelYieldTrendCard.ResumeLayout(false);
            this.tableLayoutChartsRowOne.ResumeLayout(false);
            this.panelKpiAvgCycleTime.ResumeLayout(false);
            this.panelKpiAvgCycleTime.PerformLayout();
            this.panelKpiTotalDefects.ResumeLayout(false);
            this.panelKpiTotalDefects.PerformLayout();
            this.panelKpiYieldRate.ResumeLayout(false);
            this.panelKpiYieldRate.PerformLayout();
            this.panelKpiTotalInspections.ResumeLayout(false);
            this.panelKpiTotalInspections.PerformLayout();
            this.tableLayoutKpiRow.ResumeLayout(false);
            this.flowLayoutHeaderRight.ResumeLayout(false);
            this.flowLayoutHeaderRight.PerformLayout();
            this.flowLayoutTitleBlock.ResumeLayout(false);
            this.flowLayoutTitleBlock.PerformLayout();
            this.tableLayoutHeader.ResumeLayout(false);
            this.tableLayoutHeader.PerformLayout();
            this.tableLayoutRoot.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}