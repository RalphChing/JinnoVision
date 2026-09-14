using JinnoVision.App.Core;
using JinnoVision.App.Models;
using JinnoVision.App.Services;
using JinnoVision.Models;
using JinnoVision.Services;
using JinnoVision.Services.Camera;
using JinnoVision.Services.Cobot;
using JinnoVision.Services.Plc;
using JinnoVision.Services.Setup;
using JinnoVision.Services.Vision;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        private IPlcService _plcService;
        private ICobotService _cobotService;
        private bool _robotInspectionRunning;
        private bool _autoConnectStarted;

        private RecipeModel _selectedRecipe;
        private int _currentStepIndex = -1;

        private ComboBox cboRecipes;
        private Button btnLoadRecipe;
        private Label lblLoadedRecipe;
        private Label lblCurrentStep;
        private FlowLayoutPanel panelInspectionResults;

        private string _currentRunFolder;
        private int _captureSequence;

        public DashboardControl()
        {
            InitializeComponent();

            this.HandleCreated += DashboardControl_HandleCreated;
            _codeReaderModule = new CodeReaderModule();

            InitializeInspection();
            //LoadLatestRecipeForInspection();
            //AutoConnectCamera();
            bool usePlc = bool.Parse(ConfigurationManager.AppSettings["UsePlc"] ?? "false");

            if (usePlc)
            {
                InitializePlc();
                AutoConnectPlc();
            }
            else
            {
                InitializeCobot();
                AutoConnectCobotFromConfig();
            }

            LoadRecipeDropdown();

            btnLoadRecipe.Click += BtnLoadRecipe_Click;
            picCapture.Paint += PicCapture_Paint;
            btnConnect.Click += BtnConnect_Click;
            btnDisconnect.Click += BtnDisconnect_Click;
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            this.Disposed += DashboardControl_Disposed;
            
            btnCaptureInspect.Click += BtnCaptureInspect_Click;
        }
        private void InitializePlc()
        {
            _plcService = new ModbusTcpPlcService();

            _plcService.StartInspectionRequested += PlcService_StartInspectionRequested;
            _plcService.NextStepRequested += PlcService_NextStepRequested;
        }
        private void AutoConnectCamera()
        {
            if (_cameraService != null)
                return;

            _cameraService = new HikMvsCameraService();
            _cameraService.FrameReceived += CameraService_FrameReceived;

            bool ok = _cameraService.InitializeAndOpenFirstCamera();

            if (!ok)
            {
                lblStatus.Text = "Camera not found / open failed";
                return;
            }

            _cameraService.Start(picCamera.Handle);

            lblStatus.Text = "Camera connected + live";
        }
        private void PlcService_StartInspectionRequested()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(PlcService_StartInspectionRequested));
                return;
            }

            SetPlcStatus("PLC Start Trigger Received", Color.Orange);
            StartInspectionSequence();
        }

        private void PlcService_NextStepRequested()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(PlcService_NextStepRequested));
                return;
            }

            SetPlcStatus("PLC Next Step Trigger Received", Color.Orange);

            MoveToNextInspectionStep();
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
        private void BtnLoadRecipe_Click(object sender, EventArgs e)
        {
            if (cboRecipes.SelectedItem == null)
            {
                MessageBox.Show("Please select a recipe.");
                return;
            }

            _selectedRecipe = cboRecipes.SelectedItem as RecipeModel;
            _currentRecipe = _selectedRecipe;
            _currentStepIndex = -1;

            lblLoadedRecipe.Text = $"Loaded Recipe: {_selectedRecipe.RecipeName}";
            lblCurrentStep.Text = "Current Step: Waiting for cobot start";

            panelInspectionResults.Controls.Clear();
        }
        private void DashboardControl_HandleCreated(object sender, EventArgs e)
        {
            if (_autoConnectStarted)
                return;

            _autoConnectStarted = true;
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
        private void CobotService_ProgramStartRequested()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(CobotService_ProgramStartRequested));
                return;
            }

            string provider = ConfigurationManager.AppSettings["VisionProvider"];

            //StartInspectionSequence();
        }

        private void CobotService_NextStepRequested()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(CobotService_NextStepRequested));
                return;
            }

            MoveToNextInspectionStep();
        }
        private void StartInspectionSequence()
        {
            _currentRecipe = cboRecipes.SelectedItem as RecipeModel;
            if (_currentRecipe == null)
            {
                MessageBox.Show("No recipe loaded.");
                return;
            }

            if (_currentRecipe.Steps == null || _currentRecipe.Steps.Count == 0)
            {
                MessageBox.Show("Loaded recipe has no steps.");
                return;
            }
            //picCapture.Image = _cameraService.CaptureFrame();
            StartNewInspectionRunFolder();

            _currentStepIndex = 0;
            RunCurrentStepInspection();
        }

        private void MoveToNextInspectionStep()
        {
            if (_selectedRecipe == null)
                return;

            if (_currentStepIndex < 0)
            {
                StartInspectionSequence();
                return;
            }

            _currentStepIndex++;

            if (_currentStepIndex >= _selectedRecipe.Steps.Count)
            {
                lblCurrentStep.Text = "Inspection complete.";
                return;
            }

            RunCurrentStepInspection();
        }

        private void RunCurrentStepInspection()
        {
            if (_selectedRecipe == null)
                return;

            if (_currentStepIndex < 0)
                return;

            var step = _selectedRecipe.Steps[_currentStepIndex];

            lblCurrentStep.Text =
                $"Current Step: {step.StepName}";

            panelInspectionResults.Controls.Clear();

            bool overallPass = true;

            Bitmap frame = _cameraService.CaptureFrame();

            if (frame == null)
            {
                lblStatus.Text = "Capture failed.";
                return;
            }

            var old = picCapture.Image;
            picCapture.Image = (Bitmap)frame.Clone();
            old?.Dispose();

            RunLiveInspection(frame);
            picCapture.Invalidate();

            frame.Dispose();
            foreach (var roi in step.Rois)
            {
                bool pass = InspectRoi(step, roi);

                if (!pass)
                    overallPass = false;
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

            if (_selectedRecipe == null)
                return;

            if (_currentStepIndex < 0 || _currentStepIndex >= _selectedRecipe.Steps.Count)
                return;

            var step = _selectedRecipe.Steps[_currentStepIndex];

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

                Rectangle pbRect = TranslateToPictureBoxRect(picCapture, imageRect);

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
        private Color GetResultColor(string result)
        {
            switch (result?.ToUpperInvariant())
            {
                case "PASS":
                    return Color.FromArgb(0, 180, 0);

                case "FAIL":
                    return Color.FromArgb(220, 0, 0);

                default:
                    return Color.FromArgb(255, 140, 0);
            }
        }
        private void RunLiveInspection(Bitmap frame)
        {
            if (_selectedRecipe == null)
                return;

            if (_currentStepIndex < 0 || _currentStepIndex >= _selectedRecipe.Steps.Count)
                return;

            var step = _selectedRecipe.Steps[_currentStepIndex];

            if (step == null || step.Rois == null || step.Rois.Count == 0)
                return;

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
                MessageBox.Show(
                    "AI model not loaded yet. Retrain model first.\r\n" + ex.Message,
                    "Initialization Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void LoadRecipeDropdown()
        {
            cboRecipes.Items.Clear();

            var recipes = _recipeStorageService.LoadAllRecipes();

            foreach (var recipe in recipes)
            {
                cboRecipes.Items.Add(recipe);
            }

            cboRecipes.DisplayMember = "RecipeName";

            if (cboRecipes.Items.Count > 0)
                cboRecipes.SelectedIndex = 0;
        }
        private bool InspectRoi(InspectionStepModel step, RoiInfoModel roi)
        {
            try
            {
                if (_cameraService == null)
                    return false;

                Bitmap frame = _cameraService.CaptureFrame();
                if (frame == null)
                    return false;

                using (frame)
                {
                    var recipeRoi = new RecipeRoi
                    {
                        Name = roi.RoiId,
                        ComponentName = roi.ComponentName,
                        X = roi.X,
                        Y = roi.Y,
                        Width = roi.Width,
                        Height = roi.Height,
                        Threshold = roi.ConfidenceThreshold / 100f
                    };

                    var results = _inspectionEngine.InspectFrame(
                        frame,
                        new List<RecipeRoi> { recipeRoi });

                    var result = results.FirstOrDefault();

                    if (result == null)
                        return false;

                    bool pass = result.FinalResult == "PASS";

                    AddInspectionResultRow(
                        step.StepName,
                        roi,
                        result);

                    return pass;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                AddInspectionErrorRow(
                    step.StepName,
                    roi,
                    ex.Message);

                return false;
            }
        }
        private void AddInspectionResultRow(
    string stepName,
    RoiInfoModel roi,
    InspectionResult result)
        {
            Color backColor;

            switch (result.FinalResult)
            {
                case "PASS":
                    backColor = Color.FromArgb(220, 255, 220);
                    break;

                case "FAIL":
                    backColor = Color.FromArgb(255, 220, 220);
                    break;

                default:
                    backColor = Color.FromArgb(255, 245, 200);
                    break;
            }

            var lbl = new Label
            {
                AutoSize = false,
                Width = panelInspectionResults.Width - 30,
                Height = 40,
                Margin = new Padding(0, 0, 0, 6),
                Padding = new Padding(10, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = backColor,
                ForeColor = Color.Black,
                Text =
                    $"{stepName} | {roi.RoiName} | {roi.ComponentName} | " +
                    $"{result.FinalResult} ({result.Confidence:P0})"
            };

            panelInspectionResults.Controls.Add(lbl);
        }
        private void AddInspectionErrorRow(
    string stepName,
    RoiInfoModel roi,
    string error)
        {
            var lbl = new Label
            {
                AutoSize = false,
                Width = panelInspectionResults.Width - 30,
                Height = 40,
                Margin = new Padding(0, 0, 0, 6),
                Padding = new Padding(10, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(255, 220, 220),
                ForeColor = Color.Black,
                Text =
                    $"{stepName} | {roi.RoiName} | ERROR | {error}"
            };

            panelInspectionResults.Controls.Add(lbl);
        }
        private void StartNewInspectionRunFolder()
        {
            if (_selectedRecipe == null)
                return;

            string baseFolder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "InspectionRuns");

            string safeRecipeName = MakeSafeFileName(_selectedRecipe.RecipeName);

            string runName =
                $"{DateTime.Now:yyyyMMdd_HHmmss}_{safeRecipeName}_{_selectedRecipe.RecipeId}";

            _currentRunFolder = Path.Combine(baseFolder, runName);

            Directory.CreateDirectory(_currentRunFolder);

            _captureSequence = 0;

            File.WriteAllText(
                Path.Combine(_currentRunFolder, "run_log.txt"),
                $"Run Started: {DateTime.Now:yyyy-MM-dd HH:mm:ss}{Environment.NewLine}" +
                $"Recipe ID: {_selectedRecipe.RecipeId}{Environment.NewLine}" +
                $"Recipe Name: {_selectedRecipe.RecipeName}{Environment.NewLine}");
        }
        private string MakeSafeFileName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "Unnamed";

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                value = value.Replace(c, '_');
            }

            return value.Trim();
        }
        private string SaveInspectionCapture(Bitmap frame, InspectionStepModel step)
        {
            if (frame == null)
                return null;

            if (string.IsNullOrWhiteSpace(_currentRunFolder))
                StartNewInspectionRunFolder();

            _captureSequence++;

            string stepName = MakeSafeFileName(step.StepName);

            string fileName =
                $"{_captureSequence:000}_Step{step.StepNo}_{stepName}_{DateTime.Now:HHmmssfff}.png";

            string path = Path.Combine(_currentRunFolder, fileName);

            frame.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            File.AppendAllText(
                Path.Combine(_currentRunFolder, "run_log.txt"),
                $"Captured: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | " +
                $"Step {step.StepNo} - {step.StepName} | {fileName}{Environment.NewLine}");

            return path;
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
        private void AutoConnectPlc()
        {
            string ip = "192.168.1.80";
            int port = 513;

            try
            {
                _plcService.Connect(ip, port);
                _plcService.StartListening();

                lblPlcStatus.Text = "PLC Connected";
                lblPlcStatus.BackColor = Color.LimeGreen;
            }
            catch (Exception ex)
            {
                lblPlcStatus.Text = "PLC Connection Failed";
                lblPlcStatus.BackColor = Color.Red;

                MessageBox.Show(ex.Message);
            }
        }
        private void SetPlcStatus(string text, Color color)
        {
            if (lblPlcStatus == null)
                return;

            lblPlcStatus.Text = text;
            lblPlcStatus.BackColor = color;
            lblPlcStatus.ForeColor = Color.White;
        }
        private void InitializeCobot()
        {
            _cobotService = new JakaCobotService();

            _cobotService.ProgramStartRequested +=
                CobotService_ProgramStartRequested;

            _cobotService.NextStepRequested +=
                CobotService_NextStepRequested;
        }
        private void AutoConnectCobotFromConfig()
        {
            try
            {
                string ip =
                    ConfigurationManager.AppSettings["CobotIp"];

                int port =
                    int.Parse(
                        ConfigurationManager.AppSettings["CobotPort"]);

                _cobotService.Connect(ip, port);
                _cobotService.StartListening();
                lblCobotStatus.Text = "Cobot Connected";
                lblCobotStatus.BackColor = Color.LimeGreen;
            }
            catch (Exception ex)
            {
                lblCobotStatus.Text = "Cobot Connection Failed";
                lblCobotStatus.BackColor = Color.Red;

                MessageBox.Show(ex.Message);
            }
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
