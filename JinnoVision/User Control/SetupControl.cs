using JinnoVision.Models;
using JinnoVision.Services.Setup;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using System.Linq;

namespace JinnoVision.User_Control
{
    public partial class SetupControl : UserControl
    {
        private readonly RecipeStorageService _recipeStorageService;
        private FlowLayoutPanel panelRecipeList;
        private Panel panelHeader;
        private Panel panelTabs;
        private Panel panelContent;

        private FlowLayoutPanel panelSteps;
        private FlowLayoutPanel panelRois;

        private Button btnNewRecipe;
        private Button btnSaveRecipe;
        private Button btnAddStep;
        private Button btnAddRoi;
        private Button btnSaveRoi;
        private Button btnRoiConfig;
        private Button btnDetectionParams;
        private Button btnCameraLighting;

        private TextBox txtRecipeName;
        private Label lblRecipeId;

        private RecipeModel _currentRecipe;
        private InspectionStepModel _selectedStep;

        private PictureBox picRoiImage;
        private bool _isDrawingRoi;
        private Point _roiStartPoint;
        private Rectangle _previewRect;
        private Rectangle _pendingRoiImageRect;
        private Panel panelRoiEditor;
        private PictureBox picRoiPreview;
        private TextBox txtRoiName;
        private ComboBox cmbRoiClass;
        private TrackBar trkConfidence;
        private Label lblConfidenceValue;
        private Label lblRoiSize;
        private Label lblRoiPosition;
        private Button btnDeletePendingRoi; 
        private bool _isDraggingPendingRoi;
        private Point _dragStartPoint;
        private Rectangle _dragStartRect;

        private List<RecipeModel> _recipes = new List<RecipeModel>();
        private RoiInfoModel _selectedRoi;

        private enum RoiResizeHandle
        {
            None,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        private RoiResizeHandle _activeResizeHandle = RoiResizeHandle.None;
        private bool _isResizingPendingRoi;
        private const int RoiHandleSize = 8;
        private Rectangle _resizeStartPbRect;

        public SetupControl()
        {
            InitializeComponent();

            _recipeStorageService = new RecipeStorageService();

            BuildLayout();
            WireEvents();

            LoadRecipesToLeftPanel();
            LoadMostRecentRecipe();
        }

        private void WireEvents()
        {
            btnNewRecipe.Click += BtnNewRecipe_Click;
            //btnAddStep.Click += BtnAddStep_Click;
            btnAddRoi.Click += BtnAddRoi_Click;
            btnSaveRoi.Click += BtnSaveRoi_Click;
            btnSaveRecipe.Click += BtnSaveRecipe_Click; 
            btnDeletePendingRoi.Click += BtnDeletePendingRoi_Click;
        }
        #region Build Panels
        private void BuildLeftPanel(Panel parent)
        {
            parent.Controls.Clear();

            btnNewRecipe = new Button
            {
                Text = "+ New Recipe",
                Dock = DockStyle.Top,
                Height = 45,
                FlatStyle = FlatStyle.Flat
            };

            panelRecipeList = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(0, 15, 0, 0)
            };

            parent.Controls.Add(panelRecipeList);
            parent.Controls.Add(btnNewRecipe);
        }
        private void BuildMainPanel(Panel parent)
        {
            parent.Controls.Clear();

            BuildContent(parent);
            BuildHeader(parent);
            BuildTabs(parent);
        }
        private void BuildTabs(Panel parent)
        {
            panelTabs = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(0, 10, 0, 10)
            };

            btnRoiConfig = new Button
            {
                Text = "ROI Configuration",
                Width = 160,
                Height = 35,
                Left = 0,
                Top = 10
            };

            btnDetectionParams = new Button
            {
                Text = "Detection Parameters",
                Width = 180,
                Height = 35,
                Left = 170,
                Top = 10
            };

            btnCameraLighting = new Button
            {
                Text = "Camera && Lighting",
                Width = 170,
                Height = 35,
                Left = 360,
                Top = 10
            };

            panelTabs.Controls.Add(btnRoiConfig);
            panelTabs.Controls.Add(btnDetectionParams);
            panelTabs.Controls.Add(btnCameraLighting);

            parent.Controls.Add(panelTabs);
            panelTabs.BringToFront();
        }
        private void BuildHeader(Panel parent)
        {
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            var lblName = new Label
            {
                Text = "Recipe Name",
                Location = new Point(20, 15),
                AutoSize = true
            };

            txtRecipeName = new TextBox
            {
                Location = new Point(20, 40),
                Width = 500,
                Height = 30
            };

            lblRecipeId = new Label
            {
                Text = "Recipe ID",
                Location = new Point(550, 43),
                AutoSize = true
            };

            btnSaveRecipe = new Button
            {
                Text = "Save Recipe",
                Width = 130,
                Height = 38,
                Location = new Point(700, 35),
                BackColor = Color.FromArgb(73, 105, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            panelHeader.Resize += (s, e) =>
            {
                btnSaveRecipe.Left = panelHeader.Width - btnSaveRecipe.Width - 20;
            };
            panelHeader.Controls.Add(lblName);
            panelHeader.Controls.Add(txtRecipeName);
            panelHeader.Controls.Add(lblRecipeId);
            panelHeader.Controls.Add(btnSaveRecipe);

            parent.Controls.Add(panelHeader);
            panelHeader.BringToFront();
        }

        private void BuildContent(Panel parent)
        {
            panelContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20),
                AutoScroll = true
            };

            Label lblMainTitle = new Label
            {
                Text = "Region of Interest (ROI) Setup",
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            panelSteps = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.White, //Color.FromArgb(240, 246, 252), temporary color
                Padding = new Padding(10),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true
            };

            Panel roiHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.White
            };

            Label lblRoiTitle = new Label
            {
                Text = "ROIs for Selected Step",
                Location = new Point(0, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            btnAddRoi = new Button
            {
                Text = "+ Add Rectangle",
                Width = 120,
                Height = 35,
                Location = new Point(750, 10),
                BackColor = Color.FromArgb(73, 105, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            roiHeader.Resize += (s, e) =>
            {
                btnAddRoi.Left = roiHeader.Width - btnAddRoi.Width - 20;
            };
            roiHeader.Controls.Add(lblRoiTitle);
            roiHeader.Controls.Add(btnAddRoi);
            roiHeader.Controls.Add(btnSaveRoi);

            picRoiImage = new PictureBox
            {
                Dock = DockStyle.Top,
                Height = 500,
                BackColor = Color.Black,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            //test
            Bitmap testImage = new Bitmap(1000, 600);
            using (Graphics g = Graphics.FromImage(testImage))
            {
                g.Clear(Color.DimGray);
            }
            picRoiImage.Image = testImage;
            //test

            picRoiImage.MouseDown += PicRoiImage_MouseDown;
            picRoiImage.MouseMove += PicRoiImage_MouseMove;
            picRoiImage.MouseUp += PicRoiImage_MouseUp;
            picRoiImage.Paint += PicRoiImage_Paint;

            panelRois = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };

            BuildRoiEditor();

            panelContent.Controls.Add(panelRois);     // Fill
            panelContent.Controls.Add(panelRoiEditor);
            panelContent.Controls.Add(roiHeader);     // Top
            panelContent.Controls.Add(picRoiImage);
            panelContent.Controls.Add(panelSteps);    // Top
            panelContent.Controls.Add(lblMainTitle);  // Top

            parent.Controls.Add(panelContent);
        }

        private void BuildLayout()
        {
            Controls.Clear();
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(245, 247, 250);

            var rootPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(15)
            };

            Controls.Add(rootPanel);

            var panelLeft = new Panel
            {
                Dock = DockStyle.Left,
                Width = 280,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            var panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(20, 0, 0, 0)
            };

            rootPanel.Controls.Add(panelMain);
            rootPanel.Controls.Add(panelLeft);

            BuildLeftPanel(panelLeft);
            BuildMainPanel(panelMain);
        }

        private void LoadMostRecentRecipe()
        {
            var recipe = _recipeStorageService.LoadMostRecentRecipe();

            if (recipe == null)
                return;

            LoadRecipe(recipe);
        }
        #endregion

        #region Click Handlers
        private void BtnNewRecipe_Click(object sender, EventArgs e)
        {
            _currentRecipe = new RecipeModel
            {
                RecipeId = GenerateRecipeId(),
                RecipeName = "New Recipe",
                IsActive = false
            };

            var step = new InspectionStepModel
            {
                StepNo = 1,
                StepName = "Main Step"
            };

            _currentRecipe.Steps.Add(step);
            _selectedStep = step;

            txtRecipeName.Text = _currentRecipe.RecipeName;
            lblRecipeId.Text = _currentRecipe.RecipeId;

            panelSteps.Controls.Clear();
            panelRois.Controls.Clear();

            _recipes.Add(_currentRecipe);
            LoadRecipesToLeftPanel();
        }

        private void BtnAddStep_Click(object sender, EventArgs e)
        {
            if (_currentRecipe == null)
                return;

            var step = new InspectionStepModel
            {
                StepNo = _currentRecipe.Steps.Count + 1,
                StepName = $"Step {_currentRecipe.Steps.Count + 1}"
            };

            _currentRecipe.Steps.Add(step);
            _selectedStep = step;

            AddStepCard(step);
            LoadRois();
        }

        private void BtnAddRoi_Click(object sender, EventArgs e)
        {
            if (_currentRecipe == null)
            {
                MessageBox.Show("Please create or select a recipe first.");
                return;
            }

            if (picRoiImage.Image == null)
            {
                MessageBox.Show("No image loaded.");
                return;
            }

            int roiWidth = picRoiImage.Image.Width / 4;
            int roiHeight = picRoiImage.Image.Height / 4;

            int x = (picRoiImage.Image.Width - roiWidth) / 2;
            int y = (picRoiImage.Image.Height - roiHeight) / 2;

            _pendingRoiImageRect = new Rectangle(x, y, roiWidth, roiHeight);
            _previewRect = Rectangle.Empty;
            _isDrawingRoi = false;

            ShowPendingRoiEditor();
            picRoiImage.Invalidate();
        }

        private void BtnSaveRecipe_Click(object sender, EventArgs e)
        {
            if (_currentRecipe == null)
                return;

            _currentRecipe.RecipeName = txtRecipeName.Text;

            _recipeStorageService.SaveRecipe(_currentRecipe);

            LoadRecipesToLeftPanel();

            MessageBox.Show("Recipe saved successfully.");
        }
        private void BtnSaveRoi_Click(object sender, EventArgs e)
        {
            if (_currentRecipe == null)
                return;

            if (_pendingRoiImageRect.Width <= 0 || _pendingRoiImageRect.Height <= 0)
            {
                MessageBox.Show("Please draw or create a default ROI first.");
                return;
            }

            var step = GetCurrentStep();

            var roi = new RoiInfoModel
            {
                RoiId = $"ROI_{step.Rois.Count + 1:000}",
                RoiName = txtRoiName.Text.Trim(),
                X = _pendingRoiImageRect.X,
                Y = _pendingRoiImageRect.Y,
                Width = _pendingRoiImageRect.Width,
                Height = _pendingRoiImageRect.Height,
                TargetClassName = cmbRoiClass.SelectedIndex == 0 ? "OK" : "NG",
                Classification = cmbRoiClass.SelectedIndex == 0 ? "PASS" : "FAIL",
                ConfidenceThreshold = trkConfidence.Value
            };

            step.Rois.Add(roi);
            _selectedRoi = roi;
            AddRoiEditorCard(roi, insertAtTop: true);
            panelRoiEditor.Visible = false;

            _pendingRoiImageRect = Rectangle.Empty;
            _previewRect = Rectangle.Empty;
            picRoiImage.Invalidate();
        }
        private void BtnDeletePendingRoi_Click(object sender, EventArgs e)
        {
            _pendingRoiImageRect = Rectangle.Empty;
            _previewRect = Rectangle.Empty;
            panelRoiEditor.Visible = false;

            picRoiImage.Invalidate();
        }
        private void RecipeCard_Click(object sender, EventArgs e)
        {
            Control clicked = sender as Control;

            Panel card = clicked as Panel ?? clicked.Parent as Panel;

            if (card == null)
                return;

            var recipe = card.Tag as RecipeModel;

            if (recipe == null)
                return;

            LoadRecipe(recipe);
        }
        private void StepCard_Click(object sender, EventArgs e)
        {
            Control clicked = sender as Control;
            Panel card = clicked as Panel ?? clicked.Parent as Panel;

            if (card == null)
                return;

            _selectedStep = card.Tag as InspectionStepModel;

            LoadRois();
        }
        #endregion
        #region Helper Methods
        private string GenerateRecipeId() //temporary ID generator, replace with actual logic as needed
        {
            return $"RCP-{DateTime.Now:yyyyMMddHHmmss}";
        }

        private void AddStepCard(InspectionStepModel step)
        {
            var card = new Panel
            {
                Width = 150,
                Height = 70,
                Margin = new Padding(0, 0, 10, 0),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = step
            };

            var lblStep = new Label
            {
                Text = step.StepName,
                Dock = DockStyle.Top,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            var lblRoiCount = new Label
            {
                Text = $"{step.Rois.Count} ROIs",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray
            };

            card.Controls.Add(lblRoiCount);
            card.Controls.Add(lblStep);

            card.Click += StepCard_Click;
            lblStep.Click += StepCard_Click;
            lblRoiCount.Click += StepCard_Click;

            panelSteps.Controls.Add(card);
        }

        private void LoadRois()
        {
            panelRois.Controls.Clear();
            panelRoiEditor.Visible = false;
            if (_currentRecipe == null || !_currentRecipe.Steps.Any())
                return;

            var step = _currentRecipe.Steps.First();

            foreach (var roi in step.Rois)
            {
                AddRoiEditorCard(roi, insertAtTop: false);
            }
        }

        private void LoadRecipe(RecipeModel recipe)
        {
            _currentRecipe = recipe;

            txtRecipeName.Text = recipe.RecipeName;
            lblRecipeId.Text = recipe.RecipeId;

            panelSteps.Controls.Clear(); 
            panelRois.Controls.Clear();

            if (recipe.Steps.Any())
            {
                _selectedStep = recipe.Steps.First();
                LoadRois();
            }
        }

        private void LoadRecipesToLeftPanel()
        {
            panelRecipeList.Controls.Clear();

            _recipes = _recipeStorageService.LoadAllRecipes();

            foreach (var recipe in _recipes)
            {
                AddRecipeCard(recipe);
            }
        }
        private void AddRecipeCard(RecipeModel recipe)
        {
            var card = new Panel
            {
                Width = panelRecipeList.Width - 25,
                Height = 90,
                Margin = new Padding(0, 0, 0, 10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = recipe
            };

            var lblId = new Label
            {
                Text = recipe.RecipeId,
                Location = new Point(10, 8),
                AutoSize = true
            };

            var lblName = new Label
            {
                Text = recipe.RecipeName,
                Location = new Point(10, 32),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            var lblCount = new Label
            {
                Text = $"Steps: {recipe.Steps.Count}   ROIs: {recipe.Steps.Sum(x => x.Rois.Count)}",
                Location = new Point(10, 60),
                AutoSize = true
            };

            card.Controls.Add(lblId);
            card.Controls.Add(lblName);
            card.Controls.Add(lblCount);

            card.Click += RecipeCard_Click;
            lblId.Click += RecipeCard_Click;
            lblName.Click += RecipeCard_Click;
            lblCount.Click += RecipeCard_Click;

            panelRecipeList.Controls.Add(card);
        }
        private InspectionStepModel GetCurrentStep()
        {
            if (_currentRecipe == null)
                return null;

            if (!_currentRecipe.Steps.Any())
            {
                _currentRecipe.Steps.Add(new InspectionStepModel
                {
                    StepNo = 1,
                    StepName = "Main Step"
                });
            }

            return _currentRecipe.Steps.First();
        }
        #endregion
        #region ROI Drawing Handlers
        private void PicRoiImage_MouseDown(object sender, MouseEventArgs e)
        {
            _activeResizeHandle = GetResizeHandle(e.Location);

            if (_activeResizeHandle != RoiResizeHandle.None)
            {
                _isResizingPendingRoi = true;
                _resizeStartPbRect = TranslateToPictureBoxRect(_pendingRoiImageRect);
                return;
            }

            Rectangle pbRect = TranslateToPictureBoxRect(_pendingRoiImageRect);

            if (pbRect.Contains(e.Location))
            {
                _isDraggingPendingRoi = true;
                _dragStartPoint = e.Location;
                _dragStartRect = pbRect;
                return;
            }

            _isDrawingRoi = true;
            _roiStartPoint = e.Location;
            _previewRect = Rectangle.Empty;
        }

        private void PicRoiImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizingPendingRoi)
            {
                Rectangle pbRect = _resizeStartPbRect;

                switch (_activeResizeHandle)
                {
                    case RoiResizeHandle.TopLeft:
                        pbRect = Rectangle.FromLTRB(e.X, e.Y, _resizeStartPbRect.Right, _resizeStartPbRect.Bottom);
                        break;

                    case RoiResizeHandle.TopRight:
                        pbRect = Rectangle.FromLTRB(_resizeStartPbRect.Left, e.Y, e.X, _resizeStartPbRect.Bottom);
                        break;

                    case RoiResizeHandle.BottomLeft:
                        pbRect = Rectangle.FromLTRB(e.X, _resizeStartPbRect.Top, _resizeStartPbRect.Right, e.Y);
                        break;

                    case RoiResizeHandle.BottomRight:
                        pbRect = Rectangle.FromLTRB(_resizeStartPbRect.Left, _resizeStartPbRect.Top, e.X, e.Y);
                        break;
                }

                pbRect = NormalizeRectangle(pbRect);
                _pendingRoiImageRect = TranslateToImageRect(pbRect);
                picRoiImage.Invalidate();
                return;
            }

            if (_isDraggingPendingRoi)
            {
                int dx = e.X - _dragStartPoint.X;
                int dy = e.Y - _dragStartPoint.Y;

                Rectangle movedRect = new Rectangle(
                    _dragStartRect.X + dx,
                    _dragStartRect.Y + dy,
                    _dragStartRect.Width,
                    _dragStartRect.Height
                );

                _pendingRoiImageRect = TranslateToImageRect(movedRect);

                picRoiImage.Invalidate();
                return;
            }
            if (!_isDrawingRoi)
                return;

            int x = Math.Min(_roiStartPoint.X, e.X);
            int y = Math.Min(_roiStartPoint.Y, e.Y);
            int width = Math.Abs(e.X - _roiStartPoint.X);
            int height = Math.Abs(e.Y - _roiStartPoint.Y);

            _previewRect = new Rectangle(x, y, width, height);

            picRoiImage.Invalidate();
        }

        private void PicRoiImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isResizingPendingRoi)
            {
                _isResizingPendingRoi = false;
                _activeResizeHandle = RoiResizeHandle.None;

                ShowPendingRoiEditor();
                picRoiImage.Invalidate();
                return;
            }

            if (_isDraggingPendingRoi)
            {
                _isDraggingPendingRoi = false;

                ShowPendingRoiEditor();

                picRoiImage.Invalidate();
                return;
            }
            if (!_isDrawingRoi)
                return;

            _isDrawingRoi = false;

            if (_previewRect.Width < 5 || _previewRect.Height < 5)
                return;

            _pendingRoiImageRect = TranslateToImageRect(_previewRect);

            _previewRect = Rectangle.Empty;

            ShowPendingRoiEditor();
            picRoiImage.Invalidate();
        }

        private void PicRoiImage_Paint(object sender, PaintEventArgs e)
        {
            if (picRoiImage.Image == null)
                return;

            if (_currentRecipe != null && _currentRecipe.Steps.Any())
            {
                var step = _currentRecipe.Steps.First();

                foreach (var roi in step.Rois)
                {
                    var rect = TranslateToPictureBoxRect(
                        new Rectangle(roi.X, roi.Y, roi.Width, roi.Height));

                    bool isSelected = (_selectedRoi == roi);

                    using (var pen = new Pen(isSelected ? Color.Yellow : Color.Red, isSelected ? 3 : 2))
                    {
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                }
            }

            if (_pendingRoiImageRect.Width > 0 && _pendingRoiImageRect.Height > 0)
            {
                var pendingRect = TranslateToPictureBoxRect(_pendingRoiImageRect);

                using (var pen = new Pen(Color.Lime, 2))
                {
                    e.Graphics.DrawRectangle(pen, pendingRect);
                }

                DrawRoiHandles(e.Graphics, pendingRect);
            }

            if (_isDrawingRoi && _previewRect.Width > 0 && _previewRect.Height > 0)
            {
                using (var pen = new Pen(Color.Yellow, 2))
                {
                    e.Graphics.DrawRectangle(pen, _previewRect);
                }
            }
        }
        private void DrawRoiHandles(Graphics g, Rectangle rect)
        {
            Brush brush = Brushes.Lime;

            g.FillRectangle(brush, rect.Left - 4, rect.Top - 4, RoiHandleSize, RoiHandleSize);
            g.FillRectangle(brush, rect.Right - 4, rect.Top - 4, RoiHandleSize, RoiHandleSize);
            g.FillRectangle(brush, rect.Left - 4, rect.Bottom - 4, RoiHandleSize, RoiHandleSize);
            g.FillRectangle(brush, rect.Right - 4, rect.Bottom - 4, RoiHandleSize, RoiHandleSize);
        }
        private RoiResizeHandle GetResizeHandle(Point mouse)
        {
            if (_pendingRoiImageRect.Width <= 0 || _pendingRoiImageRect.Height <= 0)
                return RoiResizeHandle.None;

            Rectangle pbRect = TranslateToPictureBoxRect(_pendingRoiImageRect);

            Rectangle tl = new Rectangle(pbRect.Left - 4, pbRect.Top - 4, RoiHandleSize, RoiHandleSize);
            Rectangle tr = new Rectangle(pbRect.Right - 4, pbRect.Top - 4, RoiHandleSize, RoiHandleSize);
            Rectangle bl = new Rectangle(pbRect.Left - 4, pbRect.Bottom - 4, RoiHandleSize, RoiHandleSize);
            Rectangle br = new Rectangle(pbRect.Right - 4, pbRect.Bottom - 4, RoiHandleSize, RoiHandleSize);

            if (tl.Contains(mouse)) return RoiResizeHandle.TopLeft;
            if (tr.Contains(mouse)) return RoiResizeHandle.TopRight;
            if (bl.Contains(mouse)) return RoiResizeHandle.BottomLeft;
            if (br.Contains(mouse)) return RoiResizeHandle.BottomRight;

            return RoiResizeHandle.None;
        }
        private Rectangle NormalizeRectangle(Rectangle rect)
        {
            int x = rect.X;
            int y = rect.Y;
            int width = rect.Width;
            int height = rect.Height;

            if (width < 0)
            {
                x += width;
                width = Math.Abs(width);
            }

            if (height < 0)
            {
                y += height;
                height = Math.Abs(height);
            }

            return new Rectangle(x, y, width, height);
        }
        private Rectangle TranslateToImageRect(Rectangle pbRect)
        {
            if (picRoiImage.Image == null)
                return Rectangle.Empty;

            var img = picRoiImage.Image;

            float imageAspect = (float)img.Width / img.Height;
            float boxAspect = (float)picRoiImage.Width / picRoiImage.Height;

            int drawWidth, drawHeight;
            int offsetX = 0, offsetY = 0;

            if (imageAspect > boxAspect)
            {
                drawWidth = picRoiImage.Width;
                drawHeight = (int)(picRoiImage.Width / imageAspect);
                offsetY = (picRoiImage.Height - drawHeight) / 2;
            }
            else
            {
                drawHeight = picRoiImage.Height;
                drawWidth = (int)(picRoiImage.Height * imageAspect);
                offsetX = (picRoiImage.Width - drawWidth) / 2;
            }

            float scaleX = (float)img.Width / drawWidth;
            float scaleY = (float)img.Height / drawHeight;

            int x = (int)((pbRect.X - offsetX) * scaleX);
            int y = (int)((pbRect.Y - offsetY) * scaleY);
            int w = (int)(pbRect.Width * scaleX);
            int h = (int)(pbRect.Height * scaleY);

            return new Rectangle(x, y, w, h);
        }
        private Rectangle TranslateToPictureBoxRect(Rectangle imgRect)
        {
            var img = picRoiImage.Image;

            float imageAspect = (float)img.Width / img.Height;
            float boxAspect = (float)picRoiImage.Width / picRoiImage.Height;

            int drawWidth, drawHeight;
            int offsetX = 0, offsetY = 0;

            if (imageAspect > boxAspect)
            {
                drawWidth = picRoiImage.Width;
                drawHeight = (int)(picRoiImage.Width / imageAspect);
                offsetY = (picRoiImage.Height - drawHeight) / 2;
            }
            else
            {
                drawHeight = picRoiImage.Height;
                drawWidth = (int)(picRoiImage.Height * imageAspect);
                offsetX = (picRoiImage.Width - drawWidth) / 2;
            }

            float scaleX = (float)drawWidth / img.Width;
            float scaleY = (float)drawHeight / img.Height;

            int x = (int)(imgRect.X * scaleX + offsetX);
            int y = (int)(imgRect.Y * scaleY + offsetY);
            int w = (int)(imgRect.Width * scaleX);
            int h = (int)(imgRect.Height * scaleY);

            return new Rectangle(x, y, w, h);
        }
        #endregion
        #region ROI saving 
        private void AddRoiEditorCard(RoiInfoModel roi, bool insertAtTop)
        {
            var btnUpdate = new Button
            {
                Text = "Update",
                Width = 90,
                Height = 32,
                Location = new Point(175, 125),
                BackColor = Color.FromArgb(73, 105, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            var btnDiscard = new Button
            {
                Text = "Discard",
                Width = 90,
                Height = 32,
                Location = new Point(275, 125),
                FlatStyle = FlatStyle.Flat
            };


            var btnDelete = new Button
            {
                Text = "Delete",
                Width = 80,
                Height = 30,
                Location = new Point(380, 125),
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat
            };

            var card = new Panel
            {
                Width = panelRois.ClientSize.Width - 35,
                Height = 170,
                Margin = new Padding(0, 0, 0, 12),
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = roi
            };

            var picPreview = new PictureBox
            {
                Width = 140,
                Height = 100,
                Location = new Point(15, 15),
                BackColor = Color.Black,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };

            var txtName = new TextBox
            {
                Text = roi.RoiName,
                Location = new Point(175, 25),
                Width = 220
            };

            var cmbClass = new ComboBox
            {
                Location = new Point(420, 25),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbClass.Items.Add("PASS");
            cmbClass.Items.Add("FAIL");
            cmbClass.SelectedItem = string.IsNullOrEmpty(roi.Classification) ? "PASS" : roi.Classification;

            var lblSize = new Label
            {
                Text = $"Size: {roi.Width} x {roi.Height}",
                Location = new Point(175, 65),
                AutoSize = true
            };

            var lblPosition = new Label
            {
                Text = $"Position: ({roi.X}, {roi.Y})",
                Location = new Point(175, 90),
                AutoSize = true
            };

            var trkConfidence = new TrackBar
            {
                Location = new Point(420, 65),
                Width = 220,
                Minimum = 0,
                Maximum = 100,
                Value = roi.ConfidenceThreshold <= 0 ? 85 : roi.ConfidenceThreshold,
                TickFrequency = 10
            };

            var lblConfidence = new Label
            {
                Text = $"{trkConfidence.Value}%",
                Location = new Point(650, 72),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            trkConfidence.ValueChanged += (s, e) =>
            {
                lblConfidence.Text = $"{trkConfidence.Value}%";
                //roi.ConfidenceThreshold = trkConfidence.Value;
            };

            btnUpdate.Click += (s, e) =>
            {
                roi.RoiName = txtName.Text.Trim();
                roi.Classification = cmbClass.SelectedItem?.ToString() ?? "PASS";
                roi.TargetClassName = roi.Classification == "PASS" ? "OK" : "NG";
                roi.ConfidenceThreshold = trkConfidence.Value;

                MessageBox.Show("ROI updated.");
            };

            btnDelete.Click += (s, e) =>
            {
                var confirm = MessageBox.Show(
                    "Delete this ROI?",
                    "Confirm",
                    MessageBoxButtons.YesNo);

                if (confirm != DialogResult.Yes)
                    return;

                var step = GetCurrentStep();

                step.Rois.Remove(roi);

                panelRois.Controls.Remove(card);

                if (_selectedRoi == roi)
                    _selectedRoi = null;

                picRoiImage.Invalidate();
            };
            btnDiscard.Click += (s, e) =>
            {
                txtName.Text = roi.RoiName;

                cmbClass.SelectedItem = string.IsNullOrEmpty(roi.Classification)
                    ? "PASS"
                    : roi.Classification;

                trkConfidence.Value = roi.ConfidenceThreshold <= 0
                    ? 85
                    : roi.ConfidenceThreshold;

                lblConfidence.Text = $"{trkConfidence.Value}%";
            };

            UpdatePreviewImage(picPreview, new Rectangle(roi.X, roi.Y, roi.Width, roi.Height));

            card.Controls.Add(picPreview);
            card.Controls.Add(txtName);
            card.Controls.Add(cmbClass);
            card.Controls.Add(lblSize);
            card.Controls.Add(lblPosition);
            card.Controls.Add(trkConfidence);
            card.Controls.Add(lblConfidence); 
            card.Controls.Add(btnUpdate);
            card.Controls.Add(btnDiscard);
            card.Controls.Add(btnDelete);

            card.Click += (s, e) =>
            {
                SelectRoi(roi);
            };

            foreach (Control c in card.Controls)
            {
                c.Click += (s, e) => SelectRoi(roi);
            }

            if (insertAtTop)
                panelRois.Controls.Add(card);
            else
                panelRois.Controls.Add(card);

            if (insertAtTop)
                panelRois.Controls.SetChildIndex(card, 0);
        }
        private void SelectRoi(RoiInfoModel roi)
        {
            _selectedRoi = roi;

            foreach (Control ctrl in panelRois.Controls)
            {
                if (ctrl is Panel p && p.Tag is RoiInfoModel r)
                {
                    p.BackColor = (r == roi)
                        ? Color.FromArgb(220, 235, 255)   
                        : Color.FromArgb(248, 250, 252);
                }
            }

            picRoiImage.Invalidate();
        }
        private void BuildRoiEditor()
        {
            panelRoiEditor = new Panel
            {
                Dock = DockStyle.Top,
                Height = 260,
                BackColor = Color.White,
                Padding = new Padding(20),
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            picRoiPreview = new PictureBox
            {
                Width = 160,
                Height = 100,
                Location = new Point(20, 20),
                BackColor = Color.Black,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblName = new Label
            {
                Text = "ROI Name / File Prefix",
                Location = new Point(200, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            txtRoiName = new TextBox
            {
                Location = new Point(200, 45),
                Width = 280
            };

            var lblClass = new Label
            {
                Text = "ROI Classification",
                Location = new Point(500, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            cmbRoiClass = new ComboBox
            {
                Location = new Point(500, 45),
                Width = 220,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbRoiClass.Items.Add("PASS - Good/Acceptable");
            cmbRoiClass.Items.Add("FAIL - Defect/Reject");
            cmbRoiClass.SelectedIndex = 0;

            var lblConfidence = new Label
            {
                Text = "Confidence Threshold",
                Location = new Point(200, 90),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            trkConfidence = new TrackBar
            {
                Location = new Point(200, 115),
                Width = 430,
                Minimum = 0,
                Maximum = 100,
                Value = 85,
                TickFrequency = 10
            };

            lblConfidenceValue = new Label
            {
                Text = "85%",
                Location = new Point(640, 120),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            trkConfidence.ValueChanged += (s, e) =>
            {
                lblConfidenceValue.Text = $"{trkConfidence.Value}%";
            };

            lblRoiPosition = new Label
            {
                Text = "Position: -",
                Location = new Point(200, 175),
                AutoSize = true
            };

            lblRoiSize = new Label
            {
                Text = "Size: -",
                Location = new Point(350, 175),
                AutoSize = true
            };

            btnSaveRoi = new Button
            {
                Text = "Save ROI",
                Width = 120,
                Height = 35,
                Location = new Point(500, 200),
                BackColor = Color.FromArgb(73, 105, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnDeletePendingRoi = new Button
            {
                Text = "Delete",
                Width = 100,
                Height = 35,
                Location = new Point(630, 200),
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat
            };

            panelRoiEditor.Controls.Add(picRoiPreview);
            panelRoiEditor.Controls.Add(lblName);
            panelRoiEditor.Controls.Add(txtRoiName);
            panelRoiEditor.Controls.Add(lblClass);
            panelRoiEditor.Controls.Add(cmbRoiClass);
            panelRoiEditor.Controls.Add(lblConfidence);
            panelRoiEditor.Controls.Add(trkConfidence);
            panelRoiEditor.Controls.Add(lblConfidenceValue);
            panelRoiEditor.Controls.Add(lblRoiPosition);
            panelRoiEditor.Controls.Add(lblRoiSize);
            panelRoiEditor.Controls.Add(btnSaveRoi);
            panelRoiEditor.Controls.Add(btnDeletePendingRoi);
        }
        private void UpdatePreviewImage(PictureBox target, Rectangle imageRect)
        {
            if (picRoiImage.Image == null)
                return;

            Rectangle safeRect = Rectangle.Intersect(
                imageRect,
                new Rectangle(0, 0, picRoiImage.Image.Width, picRoiImage.Image.Height)
            );

            if (safeRect.Width <= 0 || safeRect.Height <= 0)
                return;

            using (Bitmap source = new Bitmap(picRoiImage.Image))
            {
                Bitmap crop = source.Clone(safeRect, source.PixelFormat);

                target.Image?.Dispose();
                target.Image = crop;
            }
        }
        private void ShowPendingRoiEditor()
        {
            if (_pendingRoiImageRect.Width <= 0 || _pendingRoiImageRect.Height <= 0)
                return;

            panelRoiEditor.Visible = true;

            txtRoiName.Text = $"ROI {GetCurrentStep().Rois.Count + 1}";
            cmbRoiClass.SelectedIndex = 0;
            trkConfidence.Value = 85;

            lblRoiPosition.Text = $"Position: ({_pendingRoiImageRect.X}, {_pendingRoiImageRect.Y})";
            lblRoiSize.Text = $"Size: {_pendingRoiImageRect.Width} x {_pendingRoiImageRect.Height}";

            UpdateRoiPreview();
        }
        private void ShowSavedRoiEditor(RoiInfoModel roi)
        {
            if (roi == null)
                return;

            panelRoiEditor.Visible = true;

            txtRoiName.Text = roi.RoiName;
            cmbRoiClass.SelectedIndex = roi.Classification == "FAIL" ? 1 : 0;
            trkConfidence.Value = roi.ConfidenceThreshold;

            lblRoiPosition.Text = $"Position: ({roi.X}, {roi.Y})";
            lblRoiSize.Text = $"Size: {roi.Width} x {roi.Height}";

            UpdateRoiPreview(new Rectangle(roi.X, roi.Y, roi.Width, roi.Height));
        }
        private void UpdateRoiPreview()
        {
            if (picRoiImage.Image == null)
                return;

            Rectangle rect = Rectangle.Intersect(
                _pendingRoiImageRect,
                new Rectangle(0, 0, picRoiImage.Image.Width, picRoiImage.Image.Height)
            );

            if (rect.Width <= 0 || rect.Height <= 0)
                return;

            Bitmap source = new Bitmap(picRoiImage.Image);
            Bitmap crop = source.Clone(rect, source.PixelFormat);

            picRoiPreview.Image?.Dispose();
            picRoiPreview.Image = crop;

            source.Dispose();
        }
        private void UpdateRoiPreview(Rectangle imageRect)
        {
            if (picRoiImage.Image == null)
                return;

            Rectangle rect = Rectangle.Intersect(
                imageRect,
                new Rectangle(0, 0, picRoiImage.Image.Width, picRoiImage.Image.Height)
            );

            if (rect.Width <= 0 || rect.Height <= 0)
                return;

            using (Bitmap source = new Bitmap(picRoiImage.Image))
            {
                Bitmap crop = source.Clone(rect, source.PixelFormat);

                picRoiPreview.Image?.Dispose();
                picRoiPreview.Image = crop;
                picRoiPreview.Refresh();
            }
        }
        #endregion
    }
}
