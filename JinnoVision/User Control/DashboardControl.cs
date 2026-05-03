using AForge.Video;
using AForge.Video.DirectShow;
using JinnoVision.App.Core;
using JinnoVision.App.Models;
using JinnoVision.App.Services;
using JinnoVision.Models;
using JinnoVision.Services;
using JinnoVision.Services.Camera;
using JinnoVision.Services.Setup;
using JinnoVision.Services.Vision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JinnoVision.User_Control
{
    public partial class DashboardControl : UserControl
    {
        private ICameraService _cameraService;
        private readonly CodeReaderModule _codeReaderModule;
        private OnnxClassifier _classifier;
        private InspectionEngine _inspectionEngine;
        private RecipeStorageService _recipeStorageService = new RecipeStorageService();

        private RecipeModel _currentRecipe;
        private List<InspectionResult> _lastInspectionResults = new List<InspectionResult>();

        private Bitmap _latestFrame;
        private Bitmap _capturedFrame;

        public DashboardControl()
        {
            InitializeComponent();

            _codeReaderModule = new CodeReaderModule();
            InitializeInspection();
            LoadLatestRecipeForInspection();
            picCapture.Paint += PicCapture_Paint;
            //picCamera.Paint += PicCamera_Paint;

            btnRunCodeRead.Click += BtnRunCodeRead_Click;
        
            btnConnect.Click += BtnConnect_Click;
            btnDisconnect.Click += BtnDisconnect_Click;
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            this.Disposed += DashboardControl_Disposed;
            btnCaptureInspect.Click += BtnCaptureInspect_Click;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _cameraService?.Dispose();
                _cameraService = new HikMvsCameraService();
                _cameraService.FrameReceived += CameraService_FrameReceived;

                bool ok = _cameraService.InitializeAndOpenFirstCamera();
                lblStatus.Text = ok ? "Camera connected" : "Camera not found / open failed";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Connect error";
                MessageBox.Show(ex.Message, "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _cameraService?.Stop();
                _cameraService?.Close();

                picCamera.Image = null;
                lblStatus.Text = "Disconnected";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Disconnect Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (_cameraService == null)
                return;

            bool ok = _cameraService.Start(picCamera.Handle);
            lblStatus.Text = ok ? "Live View" : "Start failed";
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            _cameraService?.Stop();
            lblStatus.Text = "Stopped";
        }

        private void BtnRunCodeRead_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.bmp;*.png;*.jpg;*.jpeg;*.tif;*.tiff";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                CodeReadResult result = _codeReaderModule.Run(ofd.FileName);

                if (result.Success)
                {
                    txtVisionResult.Text =
                        $"Type: {result.CodeType}{Environment.NewLine}" +
                        $"Text: {result.CodeText}";
                }
                else
                {
                    txtVisionResult.Text = $"Failed: {result.ErrorMessage}";
                }
            }
        }
        private void BtnCaptureInspect_Click(object sender, EventArgs e)
        {
            if (_cameraService == null)
            {
                MessageBox.Show("Camera not connected.");
                return;
            }

            Bitmap frame = _cameraService.CaptureFrame();

            if (frame == null)
            {
                MessageBox.Show("Failed to capture frame.");
                return;
            }

            var old = picCapture.Image;
            picCapture.Image = (Bitmap)frame.Clone();
            old?.Dispose();

            RunLiveInspection(frame);

            picCapture.Invalidate();

            frame.Dispose();

            lblStatus.Text = "Captured + inspected";
        }
        private void CameraService_FrameReceived(object sender, CameraFrameEventArgs e)
        {
            if (picCamera.InvokeRequired)
            {
                picCamera.BeginInvoke(new Action(() =>
                {
                    UpdatePreview(e.Frame);
                }));
            }
            else
            {
                UpdatePreview(e.Frame);
            }
        }

        private void UpdatePreview(Bitmap frame)
        {
            var oldImage = picCamera.Image;
            picCamera.Image = frame;
            oldImage?.Dispose();

            if (_inspectionEngine != null && _currentRecipe != null)
            {
                using (Bitmap inspectionFrame = new Bitmap(frame))
                {
                    RunLiveInspection(inspectionFrame);
                }
            }

            picCamera.Invalidate();
        }
        private void PicCapture_Paint(object sender, PaintEventArgs e)
        {
            if (_lastInspectionResults == null || _lastInspectionResults.Count == 0)
                return;

            if (_currentRecipe == null)
                return;

            var step = _currentRecipe.Steps.FirstOrDefault();

            if (step == null)
                return;

            foreach (var result in _lastInspectionResults)
            {
                var roi = step.Rois.FirstOrDefault(r => r.RoiId == result.RoiName);

                if (roi == null)
                    continue;

                Rectangle imageRect = new Rectangle(
                    roi.X,
                    roi.Y,
                    roi.Width,
                    roi.Height);

                Rectangle pbRect = TranslateToPictureBoxRect(picCamera, imageRect);

                Color color = GetResultColor(result.FinalResult);

                using (var pen = new Pen(color, 4))
                {
                    e.Graphics.DrawRectangle(pen, pbRect);
                }

                DrawLabel(
                    e.Graphics,
                    $"{result.FinalResult} {result.Confidence:P0}",
                    pbRect,
                    color);
            }
        }
        private void RunLiveInspection(Bitmap frame)
        {
            if (_inspectionEngine == null)
            {
                txtVisionResult.Text = "Inspection engine is null.";
                return;
            }

            if (_currentRecipe == null)
            {
                txtVisionResult.Text = "No recipe loaded.";
                return;
            }

            var step = _currentRecipe.Steps.FirstOrDefault();

            if (step == null || step.Rois.Count == 0)
            {
                txtVisionResult.Text = "Recipe has no ROIs.";
                return;
            }

            var rois = step.Rois.Select(r => new RecipeRoi
            {
                Name = r.RoiId,
                ComponentName = r.ComponentName,
                X = r.X,
                Y = r.Y,
                Width = r.Width,
                Height = r.Height,
                Threshold = r.ConfidenceThreshold / 100f
            }).ToList();

            _lastInspectionResults = _inspectionEngine.InspectFrame(frame, rois);

            txtVisionResult.Text = string.Join(
                Environment.NewLine,
                _lastInspectionResults.Select(r =>
                    $"{r.RoiName}: {r.FinalResult} | Expected: {r.ExpectedComponent} | Predicted: {r.PredictedComponent}/{r.PredictedStatus} | {r.Confidence:P1}"
                )
            );
        }

        private void InitializeInspection()
        {
            try
            {
                _classifier = new OnnxClassifier();
                _inspectionEngine = new InspectionEngine(_classifier);
            }
            catch (Exception ex)
            {
                txtVisionResult.Text =
                    "AI model not loaded yet. Retrain model first.\r\n" + ex.Message;
            }
        }

        private void LoadLatestRecipeForInspection()
        {
            _currentRecipe = _recipeStorageService.LoadMostRecentRecipe();

            if (_currentRecipe != null)
            {
                txtVisionResult.Text =
                    $"Loaded recipe: {_currentRecipe.RecipeName}";
            }
        }
        private Color GetResultColor(string result)
        {
            switch (result)
            {
                case "PASS":
                    return Color.LimeGreen;
                case "FAIL":
                    return Color.Red;
                case "UNCERTAIN":
                    return Color.Gold;
                case "WRONG COMPONENT":
                    return Color.OrangeRed;
                default:
                    return Color.White;
            }
        }
        private void DrawLabel(Graphics g, string text, Rectangle rect, Color backColor)
        {
            using (Font font = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                SizeF textSize = g.MeasureString(text, font);

                int x = rect.X;
                int y = rect.Y - (int)textSize.Height - 6;

                if (y < 0)
                    y = rect.Y + 4;

                Rectangle labelRect = new Rectangle(
                    x,
                    y,
                    (int)textSize.Width + 10,
                    (int)textSize.Height + 6);

                using (Brush bg = new SolidBrush(backColor))
                using (Brush fg = new SolidBrush(Color.White))
                {
                    g.FillRectangle(bg, labelRect);
                    g.DrawString(text, font, fg, labelRect.X + 5, labelRect.Y + 3);
                }
            }
        }
        private Rectangle TranslateToPictureBoxRect(PictureBox pictureBox, Rectangle imgRect)
        {
            if (pictureBox.Image == null)
                return Rectangle.Empty;

            var img = pictureBox.Image;

            float imageAspect = (float)img.Width / img.Height;
            float boxAspect = (float)pictureBox.Width / pictureBox.Height;

            int drawWidth, drawHeight;
            int offsetX = 0, offsetY = 0;

            if (imageAspect > boxAspect)
            {
                drawWidth = pictureBox.Width;
                drawHeight = (int)(pictureBox.Width / imageAspect);
                offsetY = (pictureBox.Height - drawHeight) / 2;
            }
            else
            {
                drawHeight = pictureBox.Height;
                drawWidth = (int)(pictureBox.Height * imageAspect);
                offsetX = (pictureBox.Width - drawWidth) / 2;
            }

            float scaleX = (float)drawWidth / img.Width;
            float scaleY = (float)drawHeight / img.Height;

            int x = (int)(imgRect.X * scaleX + offsetX);
            int y = (int)(imgRect.Y * scaleY + offsetY);
            int w = (int)(imgRect.Width * scaleX);
            int h = (int)(imgRect.Height * scaleY);

            return new Rectangle(x, y, w, h);
        }
        private void DashboardControl_Disposed(object sender, EventArgs e)
        {
            if (_cameraService != null)
            {
                _cameraService.FrameReceived -= CameraService_FrameReceived;
                _cameraService.Dispose();
                _cameraService = null;
            }
        }
    }
}
