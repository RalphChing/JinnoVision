using JinnoVision.App.Services.Vision.GcadAnomalyDetection;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using JinnoVision.App.Helpers;


namespace JinnoVision.App.Services
{
    public partial class TrainingService
    {
        private readonly IGcadTrainer _gcadTrainer;

        public TrainingService(string recipeId = null)
        {
            if (!string.IsNullOrWhiteSpace(recipeId))
            {
                _gcadTrainer = new HalconGcadTrainer(recipeId);
            }
        }

        /// <summary>
        /// Retrain ONNX classifier (existing flow).
        /// </summary>
        public async Task<string> RetrainAsync()
        {
            return await Task.Run(() =>
            {
                var scriptPath = Path.Combine(AppPaths.Trainer, "train_classifier.py");

                if (!File.Exists(scriptPath))
                    throw new FileNotFoundException("Trainer script not found", scriptPath);

                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{scriptPath}\"",
                    WorkingDirectory = AppPaths.Trainer,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                        throw new Exception(error);

                    return output;
                }
            });
        }

        /// <summary>
        /// Train GCAD anomaly detection model (new Step 1 flow).
        /// </summary>
        public async Task<GcadEvaluationMetrics> TrainGcadModelAsync(
            string recipeId,
            string recipeStoragePath,
            GcadTrainingConfig config = null,
            Action<string> progressCallback = null)
        {
            if (_gcadTrainer == null)
                throw new InvalidOperationException("GCAD trainer not initialized. Provide recipeId in constructor.");

            config = config ?? new GcadTrainingConfig { RecipeId = recipeId };

            try
            {
                progressCallback?.Invoke("Initializing data staging...");
                await _gcadTrainer.InitializeAsync(recipeId, recipeStoragePath);

                progressCallback?.Invoke("Preparing training data...");
                await _gcadTrainer.PrepareDataAsync(config);

                progressCallback?.Invoke("Training GCAD model...");
                var modelPath = await _gcadTrainer.TrainAsync(config, progressCallback);

                progressCallback?.Invoke("Evaluating trained model...");
                var metrics = await _gcadTrainer.EvaluateAsync(modelPath, config);

                progressCallback?.Invoke("Saving evaluation report...");
                var reportPath = Path.Combine(
                    Path.GetDirectoryName(modelPath),
                    $"{Path.GetFileNameWithoutExtension(modelPath)}_evaluation.json");
                await _gcadTrainer.SaveEvaluationReportAsync(metrics, reportPath);

                progressCallback?.Invoke($"GCAD training complete. Board-level recall: {metrics.Recall:P2}");
                return metrics;
            }
            catch (Exception ex)
            {
                progressCallback?.Invoke($"GCAD training failed: {ex.Message}");
                throw;
            }
            finally
            {
                _gcadTrainer?.Dispose();
            }
        }
    }
}
