using System;
using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    public partial class ReportsControl : UserControl
    {
        private static readonly Color ColorTitleText = Color.FromArgb(30, 41, 59);
        private static readonly Color ColorSubtleText = Color.FromArgb(100, 116, 139);
        private static readonly Color ColorPositive = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorNegative = Color.FromArgb(220, 38, 38);

        public ReportsControl()
        {
            InitializeComponent();
        }

        private void TxtSearchById_Enter(object sender, EventArgs e)
        {
            if (txtSearchById.Text == "Search by ID...")
            {
                txtSearchById.Text = "";
                txtSearchById.ForeColor = ColorTitleText;
            }
        }

        private void TxtSearchById_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchById.Text))
            {
                txtSearchById.Text = "Search by ID...";
                txtSearchById.ForeColor = ColorSubtleText;
            }
        }

        public void AddInspectionHistoryRow(
            bool passed, string inspectionId, string boardId,
            int defectCount, double cycleTimeSeconds, DateTime timestamp)
        {
            var row = new TableLayoutPanel
            {
                AutoSize = true,
                ColumnCount = 4,
                RowCount = 1,
                Width = flowLayoutHistoryRows.Width - 20,
                Margin = new Padding(0, 0, 0, 10)
            };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));

            var idBlock = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false
            };

            var pill = new Label
            {
                Text = passed ? "\u2713  PASS" : "\u2715  FAIL",
                BackColor = passed ? ColorPositive : ColorNegative,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(8, 3, 8, 3),
                Margin = new Padding(0, 0, 10, 0)
            };
            idBlock.Controls.Add(pill);

            var idText = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                WrapContents = false
            };
            idText.Controls.Add(new Label { Text = inspectionId, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = ColorTitleText, AutoSize = true });
            idText.Controls.Add(new Label { Text = boardId, Font = new Font("Segoe UI", 8), ForeColor = ColorSubtleText, AutoSize = true });
            idBlock.Controls.Add(idText);

            row.Controls.Add(idBlock, 0, 0);
            row.Controls.Add(new Label { Text = defectCount.ToString(), ForeColor = defectCount > 0 ? ColorNegative : ColorTitleText, AutoSize = true, Anchor = AnchorStyles.Left }, 1, 0);
            row.Controls.Add(new Label { Text = $"{cycleTimeSeconds:0.0}s", ForeColor = ColorTitleText, AutoSize = true, Anchor = AnchorStyles.Left }, 2, 0);
            row.Controls.Add(new Label { Text = timestamp.ToString("h:mm:ss tt"), ForeColor = ColorSubtleText, AutoSize = true, Anchor = AnchorStyles.Left }, 3, 0);

            flowLayoutHistoryRows.Controls.Add(row);
        }

        public void ClearInspectionHistory()
        {
            flowLayoutHistoryRows.Controls.Clear();
        }
    }
}