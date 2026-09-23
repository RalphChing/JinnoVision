using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JinnoVision.App.Services.Vision.GcadAnomalyDetection
{
    /// <summary>
    /// Stages training and evaluation data for GCAD model.
    /// Organizes recipe data into train/val/eval splits per HALCON conventions.
    /// </summary>
    public class GcadDataStager
    {
        private readonly string _baseDataPath;
        private readonly string _recipePath;

        public GcadDataStager(string recipeId)
        {
            _baseDataPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "gcad_training_data",
                recipeId);

            _recipePath = _baseDataPath;
        }

        /// <summary>
        /// Initialize directory structure for a recipe's GCAD training.
        /// Creates subdirs: train_ok/, val_ok/, eval_ok/, eval_nok/ (with optional per-pixel masks).
        /// </summary>
        public void InitializeDirectories()
        {
            var dirs = new[]
            {
                Path.Combine(_recipePath, "train_ok"),   // known-good boards for training
                Path.Combine(_recipePath, "val_ok"),     // known-good boards for validation
                Path.Combine(_recipePath, "eval_ok"),    // held-out good boards (labeled)
                Path.Combine(_recipePath, "eval_nok"),   // held-out defective boards (labeled, optional masks)
                Path.Combine(_recipePath, "masks")       // per-pixel ground truth masks (optional)
            };

            foreach (var dir in dirs)
            {
                Directory.CreateDirectory(dir);
            }

            Debug.WriteLine($"[GcadDataStager] Initialized directories for recipe at {_recipePath}");
        }

        /// <summary>
        /// Organize ROI training images into train/val/eval splits.
        /// Called after recipe setup completes and training images have been captured.
        /// </summary>
        /// <param name="recipeStoragePath">Path to the recipe's stored training images (where ROI crops are saved).</param>
        /// <param name="trainFraction">Fraction of good images to use for training (e.g., 0.7).</param>
        /// <param name="valFraction">Fraction of good images for validation (e.g., 0.15).</param>
        public async Task<(int trainImages, int valImages, int evalImages)> StageTrainingImagesAsync(
            string recipeStoragePath,
            double trainFraction = 0.7,
            double valFraction = 0.15)
        {
            return await Task.Run(() =>
            {
                if (!Directory.Exists(recipeStoragePath))
                    throw new DirectoryNotFoundException($"Recipe storage path not found: {recipeStoragePath}");

                // Find all "PASS" images (known-good)
                var goodImages = Directory.GetFiles(recipeStoragePath, "*.png", SearchOption.AllDirectories)
                    .Where(f => f.Contains("Pass") || f.Contains("OK"))
                    .ToList();

                if (goodImages.Count == 0)
                {
                    Debug.WriteLine($"[GcadDataStager] No PASS images found in {recipeStoragePath}");
                    return (0, 0, 0);
                }

                // Shuffle and split
                var rng = new Random(42);
                var shuffled = goodImages.OrderBy(x => rng.Next()).ToList();

                int trainCount = Math.Max(1, (int)(shuffled.Count * trainFraction));
                int valCount = Math.Max(1, (int)(shuffled.Count * valFraction));
                int evalCount = shuffled.Count - trainCount - valCount;

                var trainSet = shuffled.Take(trainCount).ToList();
                var valSet = shuffled.Skip(trainCount).Take(valCount).ToList();
                var evalSet = shuffled.Skip(trainCount + valCount).ToList();

                // Copy to staging dirs
                CopyImages(trainSet, Path.Combine(_recipePath, "train_ok"), "train");
                CopyImages(valSet, Path.Combine(_recipePath, "val_ok"), "val");
                CopyImages(evalSet, Path.Combine(_recipePath, "eval_ok"), "eval");

                Debug.WriteLine(
                    $"[GcadDataStager] Staged {trainCount} train, {valCount} val, {evalCount} eval images");

                return (trainCount, valCount, evalCount);
            });
        }

        /// <summary>
        /// Add labeled evaluation set: defective boards with optional per-pixel mask annotations.
        /// </summary>
        /// <param name="defectiveImages">List of (imagePath, maskPath_or_null) tuples.</param>
        public async Task AddLabeledEvalSetAsync(List<(string ImagePath, string MaskPath)> defectiveImages)
        {
            await Task.Run(() =>
            {
                foreach (var (imgPath, maskPath) in defectiveImages)
                {
                    if (!File.Exists(imgPath))
                        continue;

                    var destImg = Path.Combine(_recipePath, "eval_nok", Path.GetFileName(imgPath));
                    File.Copy(imgPath, destImg, overwrite: true);

                    if (!string.IsNullOrWhiteSpace(maskPath) && File.Exists(maskPath))
                    {
                        var maskFileName = Path.GetFileNameWithoutExtension(imgPath) + "_mask.png";
                        var destMask = Path.Combine(_recipePath, "masks", maskFileName);
                        File.Copy(maskPath, destMask, overwrite: true);
                    }
                }

                Debug.WriteLine($"[GcadDataStager] Added {defectiveImages.Count} labeled defective samples");
            });
        }

        /// <summary>
        /// Get the path to a dataset split directory.
        /// </summary>
        public string GetSplitPath(string splitName)
        {
            return Path.Combine(_recipePath, splitName);
        }

        /// <summary>
        /// Count images in each split.
        /// </summary>
        public (int TrainCount, int ValCount, int EvalOkCount, int EvalNokCount) GetImageCounts()
        {
            var trainCount = Directory.GetFiles(Path.Combine(_recipePath, "train_ok"), "*.png").Length;
            var valCount = Directory.GetFiles(Path.Combine(_recipePath, "val_ok"), "*.png").Length;
            var evalOkCount = Directory.GetFiles(Path.Combine(_recipePath, "eval_ok"), "*.png").Length;
            var evalNokCount = Directory.GetFiles(Path.Combine(_recipePath, "eval_nok"), "*.png").Length;

            return (trainCount, valCount, evalOkCount, evalNokCount);
        }

        private void CopyImages(IEnumerable<string> sources, string destDir, string prefix)
        {
            Directory.CreateDirectory(destDir);
            int index = 0;
            foreach (var src in sources)
            {
                var destFileName = $"{prefix}_{index:00000}{Path.GetExtension(src)}";
                var dest = Path.Combine(destDir, destFileName);
                File.Copy(src, dest, overwrite: true);
                index++;
            }
        }
    }
}
