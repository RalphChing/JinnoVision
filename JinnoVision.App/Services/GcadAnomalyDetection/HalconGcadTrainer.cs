using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace JinnoVision.App.Services.Vision.GcadAnomalyDetection
{
    /// <summary>
    /// STILL NOT IMPLEMENTED -- no operator binding exists at all, only as HDevelop procedures:
    ///   - normalize_dl_gc_anomaly_scores (required after training a gc_anomaly_detection model,
    ///     before thresholds are meaningful)
    ///   - compute_dl_anomaly_thresholds (threshold calibration for a target recall)
    ///   Both need HDevEngine (hdevenginedotnet.dll, in the same dotnet35 folder as halcondotnet.dll)
    ///   with class/method names confirmed via Object Browser, and their own parameter names
    ///   confirmed via the HDevelop procedure browser.
    /// </summary>
    public class HalconGcadTrainer : IGcadTrainer
    {
        private const string BaseGcadModelFileName = "pretrained_dl_anomaly_global_context.hdl";
        private const int OkClassId = 0;
        private const int NokClassId = 1;

        private readonly string _recipeId;
        private string _recipeStoragePath;
        private string _stagingPath;
        private string _preprocessingParamsPath;
        private GcadDataStager _dataStager;
        private bool _disposed;

        public HalconGcadTrainer(string recipeId)
        {
            _recipeId = recipeId ?? throw new ArgumentNullException(nameof(recipeId));
        }

        public async Task InitializeAsync(string recipeId, string recipeStoragePath)
        {
            ThrowIfDisposed();

            if (!Directory.Exists(recipeStoragePath))
                throw new DirectoryNotFoundException($"Recipe storage path not found: {recipeStoragePath}");

            _recipeStoragePath = recipeStoragePath;
            _stagingPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "gcad_training_data",
                recipeId);

            _dataStager = new GcadDataStager(recipeId);
            _dataStager.InitializeDirectories();

            var (trainCount, valCount, evalCount) = await _dataStager.StageTrainingImagesAsync(
                _recipeStoragePath,
                trainFraction: 0.7,
                valFraction: 0.15);

            Debug.WriteLine(
                $"[HalconGcadTrainer] Initialized {_recipeId}: " +
                $"{trainCount} train, {valCount} val, {evalCount} eval images");
        }

        public async Task PrepareDataAsync(GcadTrainingConfig config, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (_dataStager == null)
                throw new InvalidOperationException("Call InitializeAsync first");

            await Task.Run(() =>
            {
                var (trainCount, valCount, evalOkCount, evalNokCount) = _dataStager.GetImageCounts();

                if (trainCount == 0)
                    throw new GcadTrainingException("No training images found. Stage data first.");

                var trainOkPath = _dataStager.GetSplitPath("train_ok");
                var allImages = Directory.GetFiles(trainOkPath, "*.png")
                    .Concat(Directory.GetFiles(trainOkPath, "*.jpg"))
                    .Concat(Directory.GetFiles(trainOkPath, "*.bmp"))
                    .ToList();

                if (allImages.Count == 0)
                    throw new GcadTrainingException($"No images found in {trainOkPath}");

                var rng = new Random(config.RandomSeed);
                var shuffled = allImages.OrderBy(_ => rng.Next()).ToList();
                int valSplitCount = Math.Max(1, (int)(shuffled.Count * config.ValidationFraction));

                var valFiles = shuffled.Take(valSplitCount).ToList();
                var trainFiles = shuffled.Skip(valSplitCount).ToList();

                Debug.WriteLine($"[HalconGcadTrainer] Split: {trainFiles.Count} train, {valFiles.Count} val");

                _preprocessingParamsPath = Path.Combine(_stagingPath, "preprocessing_params.json");
                Directory.CreateDirectory(_stagingPath);
                SavePreprocessingParams(config, trainFiles, valFiles, _preprocessingParamsPath);

            }, cancellationToken);
        }

        public async Task<string> TrainAsync(
            GcadTrainingConfig config,
            Action<string> progressCallback = null,
            CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            return await Task.Run<string>((Func<string>)(() =>
            {
                progressCallback?.Invoke("Loading base GCAD model...");

                HTuple dlModelHandle;
                HOperatorSet.ReadDlModel(new HTuple(BaseGcadModelFileName), out dlModelHandle);
                Debug.WriteLine("[HalconGcadTrainer] Loaded base GCAD model");

                HTuple imageWidth, imageHeight, imageRangeMin, imageRangeMax;
                HOperatorSet.GetDlModelParam(dlModelHandle, new HTuple("image_width"), out imageWidth);
                HOperatorSet.GetDlModelParam(dlModelHandle, new HTuple("image_height"), out imageHeight);
                HOperatorSet.GetDlModelParam(dlModelHandle, new HTuple("image_range_min"), out imageRangeMin);
                HOperatorSet.GetDlModelParam(dlModelHandle, new HTuple("image_range_max"), out imageRangeMax);
                Debug.WriteLine(
                    $"[HalconGcadTrainer] Model expects {imageWidth.I}x{imageHeight.I}, " +
                    $"range [{imageRangeMin.D}, {imageRangeMax.D}]");

                HOperatorSet.SetDlModelParam(dlModelHandle, new HTuple("gc_anomaly_networks"),
                    new HTuple(config.GcAnomalyNetworks));
                HOperatorSet.SetDlModelParam(dlModelHandle, new HTuple("batch_size"),
                    new HTuple(config.BatchSize));

                HOperatorSet.SetDlModelParam(dlModelHandle, new HTuple("anomaly_score_tolerance"),
                    new HTuple(0.0));

                if (_preprocessingParamsPath == null || !File.Exists(_preprocessingParamsPath))
                    throw new GcadTrainingException("Call PrepareDataAsync before TrainAsync");

                var (trainFiles, valFiles) = LoadPreprocessingParams(_preprocessingParamsPath);

                var rng = new Random(config.RandomSeed);

                for (int epoch = 0; epoch < config.NumEpochs; epoch++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var shuffledTrain = trainFiles.OrderBy(_ => rng.Next()).ToList();
                    double lastLoss = double.NaN;
                    int batchCount = 0;

                    for (int i = 0; i + config.BatchSize <= shuffledTrain.Count; i += config.BatchSize)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var batchFiles = shuffledTrain.Skip(i).Take(config.BatchSize).ToList();

                        HTuple sampleBatch = new HTuple();
                        foreach (var file in batchFiles)
                        {
                            HTuple sample = BuildDlSample(
                                file, imageWidth.I, imageHeight.I, imageRangeMin.D, imageRangeMax.D,
                                includeLabel: true);
                            sampleBatch = sampleBatch.TupleConcat(sample);
                        }

                        HTuple trainResult;
                        HOperatorSet.TrainDlModelBatch(dlModelHandle, sampleBatch, out trainResult);

                        HTuple totalLoss;
                        HOperatorSet.GetDictTuple(trainResult, new HTuple("total_loss"), out totalLoss);
                        lastLoss = totalLoss.D;
                        batchCount++;
                    }

                    Debug.WriteLine($"[HalconGcadTrainer] Epoch {epoch + 1}/{config.NumEpochs}: " +
                        $"{batchCount} batches, last total_loss={lastLoss}");
                    progressCallback?.Invoke($"Epoch {epoch + 1}/{config.NumEpochs} complete (loss: {lastLoss:F4})");
                }

                throw new NotImplementedException(
                    "Training loop is implemented and should run correctly, but stops here: " +
                    "normalize_dl_gc_anomaly_scores has no operator binding and must be called via " +
                    "HDevEngine before the model is usable. See the class-level comment for what's needed.");

            }), cancellationToken);
        }

        public async Task<GcadEvaluationMetrics> EvaluateAsync(
            string modelPath,
            GcadTrainingConfig config,
            CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (!File.Exists(modelPath))
                throw new FileNotFoundException($"Model not found: {modelPath}");

            return await Task.Run(() =>
            {
                var metrics = new GcadEvaluationMetrics();

                var evalOkPath = _dataStager.GetSplitPath("eval_ok");
                var evalNokPath = _dataStager.GetSplitPath("eval_nok");

                var evalOkImages = Directory.GetFiles(evalOkPath, "*.png").ToList();
                var evalNokImages = Directory.GetFiles(evalNokPath, "*.png").ToList();
                metrics.TotalBoards = evalOkImages.Count + evalNokImages.Count;

                HTuple dlModelHandle;
                HOperatorSet.ReadDlModel(new HTuple(modelPath), out dlModelHandle);

                HTuple imageWidth, imageHeight, imageRangeMin, imageRangeMax;
                HOperatorSet.GetDlModelParam(dlModelHandle, new HTuple("image_width"), out imageWidth);
                HOperatorSet.GetDlModelParam(dlModelHandle, new HTuple("image_height"), out imageHeight);
                HOperatorSet.GetDlModelParam(dlModelHandle, new HTuple("image_range_min"), out imageRangeMin);
                HOperatorSet.GetDlModelParam(dlModelHandle, new HTuple("image_range_max"), out imageRangeMax);

                metrics.TrueNegatives = 0;
                metrics.FalsePositives = 0;
                foreach (var imgPath in evalOkImages)
                {
                    double anomalyScore = RunInference(
                        dlModelHandle, imgPath, imageWidth.I, imageHeight.I, imageRangeMin.D, imageRangeMax.D);
                    if (anomalyScore < config.AnomalyScoreThreshold)
                        metrics.TrueNegatives++;
                    else
                        metrics.FalsePositives++;
                }

                metrics.TruePositives = 0;
                metrics.FalseNegatives = 0;
                foreach (var imgPath in evalNokImages)
                {
                    double anomalyScore = RunInference(
                        dlModelHandle, imgPath, imageWidth.I, imageHeight.I, imageRangeMin.D, imageRangeMax.D);
                    if (anomalyScore >= config.AnomalyScoreThreshold)
                        metrics.TruePositives++;
                    else
                        metrics.FalseNegatives++;
                }

                if (metrics.TotalBoards > 0)
                {
                    metrics.Recall = (metrics.TruePositives + metrics.FalseNegatives) > 0
                        ? (double)metrics.TruePositives / (metrics.TruePositives + metrics.FalseNegatives)
                        : 0.0;
                    metrics.Precision = (metrics.TruePositives + metrics.FalsePositives) > 0
                        ? (double)metrics.TruePositives / (metrics.TruePositives + metrics.FalsePositives)
                        : 0.0;
                    metrics.FalsePositiveRate = (metrics.FalsePositives + metrics.TrueNegatives) > 0
                        ? (double)metrics.FalsePositives / (metrics.FalsePositives + metrics.TrueNegatives)
                        : 0.0;
                    metrics.F1Score = (metrics.Recall + metrics.Precision) > 0
                        ? 2.0 * (metrics.Recall * metrics.Precision) / (metrics.Recall + metrics.Precision)
                        : 0.0;
                }

                // TODO: NOT IMPLEMENTED -- compute_dl_anomaly_thresholds (HDevEngine-only, see above).
                metrics.RecommendedThreshold = config.AnomalyScoreThreshold;

                Debug.WriteLine("[HalconGcadTrainer] Evaluation complete: " + metrics.ToString());
                return metrics;

            }, cancellationToken);
        }

        public async Task SaveEvaluationReportAsync(GcadEvaluationMetrics metrics, string outputPath)
        {
            await Task.Run(() =>
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
                var report = new
                {
                    RecipeId = _recipeId,
                    Timestamp = DateTime.UtcNow,
                    Metrics = metrics,
                    Summary = metrics.ToString()
                };
                var json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputPath, json);
            });
        }

        public string GetPreprocessingParametersPath() => _preprocessingParamsPath;

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(HalconGcadTrainer));
        }

        /// <summary>
        /// Builds a DLSample dict for one image. CONFIRMED keys ('image', 'anomaly_label_id') from
        /// the official Deep Learning / Model chapter. Preprocessing (resize + type conversion +
        /// range scaling) is the SOFT SPOT noted at the class level -- standard HALCON pattern,
        /// not copied from a confirmed working example.
        /// </summary>
        private HTuple BuildDlSample(
            string imagePath, int targetWidth, int targetHeight,
            double rangeMin, double rangeMax, bool includeLabel)
        {
            HObject image;
            HOperatorSet.ReadImage(out image, imagePath);

            HObject imageResized;
            HOperatorSet.ZoomImageSize(image, out imageResized, targetWidth, targetHeight, "constant");

            HObject imageReal;
            HOperatorSet.ConvertImageType(imageResized, out imageReal, "real");

            // Linear rescale from the raw byte range [0, 255] to [rangeMin, rangeMax].
            double mult = (rangeMax - rangeMin) / 255.0;
            double add = rangeMin;
            HObject imageScaled;
            HOperatorSet.ScaleImage(imageReal, out imageScaled, mult, add);

            HTuple sample;
            HOperatorSet.CreateDict(out sample);
            HOperatorSet.SetDictObject(imageScaled, sample, new HTuple("image"));

            if (includeLabel)
            {
                // All staged training/eval "ok" images are known-good -> class id 0.
                HOperatorSet.SetDictTuple(sample, new HTuple("anomaly_label_id"), new HTuple(OkClassId));
            }

            return sample;
        }

        private double RunInference(
            HTuple dlModelHandle, string imagePath,
            int targetWidth, int targetHeight, double rangeMin, double rangeMax)
        {
            HTuple dlSampleHandle = BuildDlSample(
                imagePath, targetWidth, targetHeight, rangeMin, rangeMax, includeLabel: false);

            HTuple dlResultHandle;
            HOperatorSet.ApplyDlModel(dlModelHandle, dlSampleHandle, new HTuple(), out dlResultHandle);

            HTuple anomalyScore;
            HOperatorSet.GetDictTuple(dlResultHandle, new HTuple("anomaly_score"), out anomalyScore);

            return anomalyScore.D;
        }

        private void SavePreprocessingParams(
            GcadTrainingConfig config, List<string> trainFiles, List<string> valFiles, string path)
        {
            var info = new
            {
                ImageWidth = config.ImageWidth,
                ImageHeight = config.ImageHeight,
                RandomSeed = config.RandomSeed,
                CreatedAt = DateTime.UtcNow,
                TrainFiles = trainFiles,
                ValFiles = valFiles
            };
            var json = JsonSerializer.Serialize(info, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        private (List<string> trainFiles, List<string> valFiles) LoadPreprocessingParams(string path)
        {
            var json = File.ReadAllText(path);
            using (var doc = JsonDocument.Parse(json))
            {
                var root = doc.RootElement;
                var trainFiles = root.GetProperty("TrainFiles").EnumerateArray().Select(e => e.GetString()).ToList();
                var valFiles = root.GetProperty("ValFiles").EnumerateArray().Select(e => e.GetString()).ToList();
                return (trainFiles, valFiles);
            }
        }
    }
}