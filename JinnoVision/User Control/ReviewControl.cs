using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    public partial class ReviewControl : UserControl
    {
        private static readonly Color ColorTitleText = Color.FromArgb(30, 41, 59);
        private static readonly Color ColorSubtleText = Color.FromArgb(100, 116, 139);
        private static readonly Color ColorPositive = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorNegative = Color.FromArgb(220, 38, 38);
        private static readonly Color ColorNeutralPill = Color.FromArgb(148, 163, 184);
        private static readonly Color ColorCardSelected = Color.FromArgb(219, 234, 254);
        private static readonly Color ColorCardSelectedBorder = Color.FromArgb(37, 99, 235);

        public class DefectMarker
        {
            public Rectangle Bounds { get; set; }
            public string Label { get; set; }
        }

        private readonly List<DefectMarker> _defectMarkers = new List<DefectMarker>();
        private Panel _selectedSidebarCard;
        private int _referenceZoomPercent = 100;

        public ReviewControl()
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

        private void BtnZoomInReference_Click(object sender, EventArgs e)
        {
            _referenceZoomPercent = Math.Min(400, _referenceZoomPercent + 25);
            lblReferenceZoomLevel.Text = $"{_referenceZoomPercent}%";
        }

        private void BtnZoomOutReference_Click(object sender, EventArgs e)
        {
            _referenceZoomPercent = Math.Max(25, _referenceZoomPercent - 25);
            lblReferenceZoomLevel.Text = $"{_referenceZoomPercent}%";
        }

        private void BtnExportInspectedImage_Click(object sender, EventArgs e)
        {
            
        }

        private void PicInspectedImage_Paint(object sender, PaintEventArgs e)
        {
            foreach (var marker in _defectMarkers)
            {
                using (var pen = new Pen(ColorNegative, 2))
                {
                    e.Graphics.DrawRectangle(pen, marker.Bounds);
                }

                using (Font font = new Font("Segoe UI", 8, FontStyle.Bold))
                {
                    SizeF textSize = e.Graphics.MeasureString(marker.Label, font);
                    var labelRect = new Rectangle(
                        marker.Bounds.X,
                        marker.Bounds.Y - (int)textSize.Height - 6,
                        (int)textSize.Width + 10,
                        (int)textSize.Height + 6);

                    if (labelRect.Y < 0)
                        labelRect.Y = marker.Bounds.Y + 4;

                    using (Brush bg = new SolidBrush(ColorNegative))
                    using (Brush fg = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillRectangle(bg, labelRect);
                        e.Graphics.DrawString(marker.Label, font, fg, labelRect.X + 5, labelRect.Y + 3);
                    }
                }
            }
        }

        public void SetInspectedImageDefects(List<DefectMarker> markers)
        {
            _defectMarkers.Clear();
            if (markers != null)
                _defectMarkers.AddRange(markers);

            picInspectedImage.Invalidate();
        }

        public void ShowInspectionDetail(
            string inspectionId, string boardDescription, bool passed,
            DateTime timestamp, double cycleTimeSeconds, List<DefectMarker> defects)
        {
            lblDetailId.Text = inspectionId;
            lblDetailSubtitle.Text = boardDescription;
            lblTimestampValue.Text = timestamp.ToString("M/d/yyyy, h:mm:ss tt");
            lblCycleTimeValue.Text = $"{cycleTimeSeconds:0.0}s";

            lblDetailResultPill.Text = passed ? "PASS" : "FAIL";
            lblDetailResultPill.BackColor = passed ? ColorPositive : ColorNegative;

            SetInspectedImageDefects(defects);
        }

        public void ClearInspectionList()
        {
            flowLayoutSidebarCards.Controls.Clear();
            _selectedSidebarCard = null;
        }

        public void AddInspectionCard(
            string inspectionId, string boardId, bool passed, int defectCount,
            DateTime timestamp, string description, double cycleTimeSeconds,
            List<DefectMarker> defects)
        {
            var card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(16),
                Width = flowLayoutSidebarCards.Width - 24,
                Height = 130,
                Margin = new Padding(0, 0, 0, 12),
                Cursor = Cursors.Hand
            };

            var content = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = false,
                Dock = DockStyle.Fill,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            var row1 = new TableLayoutPanel
            {
                AutoSize = true,
                ColumnCount = 2,
                RowCount = 1,
                Width = card.Width - 32
            };
            row1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            row1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));

            row1.Controls.Add(new Label
            {
                Text = inspectionId,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorTitleText,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            }, 0, 0);

            row1.Controls.Add(new Label
            {
                Text = passed ? "\u2713  PASS" : "\u2715  FAIL",
                BackColor = passed ? ColorPositive : ColorNegative,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(8, 3, 8, 3),
                Anchor = AnchorStyles.Right
            }, 1, 0);

            content.Controls.Add(row1);

            content.Controls.Add(new Label
            {
                Text = boardId,
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorSubtleText,
                AutoSize = true,
                Margin = new Padding(0, 4, 0, 8)
            });

            var row3 = new TableLayoutPanel
            {
                AutoSize = true,
                ColumnCount = 2,
                RowCount = 1,
                Width = card.Width - 32
            };
            row3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            row3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));

            row3.Controls.Add(new Label
            {
                Text = "\u23F1  " + timestamp.ToString("h:mm:ss tt"),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = ColorSubtleText,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            }, 0, 0);

            row3.Controls.Add(new Label
            {
                Text = $"{defectCount} defect(s)",
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = defectCount > 0 ? ColorNegative : ColorTitleText,
                AutoSize = true,
                Anchor = AnchorStyles.Right
            }, 1, 0);

            content.Controls.Add(row3);

            content.Controls.Add(new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = ColorSubtleText,
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 0)
            });

            card.Controls.Add(content);

            EventHandler selectHandler = (s, e) =>
            {
                if (_selectedSidebarCard != null)
                    _selectedSidebarCard.BackColor = Color.White;

                card.BackColor = ColorCardSelected;
                _selectedSidebarCard = card;

                ShowInspectionDetail(inspectionId, description, passed, timestamp, cycleTimeSeconds, defects);
            };

            card.Click += selectHandler;
            foreach (Control child in content.Controls)
                child.Click += selectHandler;

            flowLayoutSidebarCards.Controls.Add(card);
        }
    }
}