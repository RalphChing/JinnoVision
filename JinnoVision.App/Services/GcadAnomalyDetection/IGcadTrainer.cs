using System;
using System.Threading;
using System.Threading.Tasks;

namespace JinnoVision.App.Services.Vision.GcadAnomalyDetection
{
    public interface IGcadTrainer : IDisposable
    {
        Task InitializeAsync(string recipeId, string recipeStoragePath);

        Task PrepareDataAsync(GcadTrainingConfig config, CancellationToken cancellationToken = default);

        Task<string> TrainAsync(
            GcadTrainingConfig config,
            Action<string> progressCallback = null,
            CancellationToken cancellationToken = default);

        Task<GcadEvaluationMetrics> EvaluateAsync(
            string modelPath,
            GcadTrainingConfig config,
            CancellationToken cancellationToken = default);

        Task SaveEvaluationReportAsync(GcadEvaluationMetrics metrics, string outputPath);

        string GetPreprocessingParametersPath();
    }
}