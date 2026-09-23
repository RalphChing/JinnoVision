using System;
using System.Collections.Generic;

namespace JinnoVision.App.Services.Vision.GcadAnomalyDetection
{
    /// <summary>
    /// Configuration for GCAD model training.
    /// Exposes key tuning parameters; persisted alongside the model artifact.
    /// </summary>
    public class GcadTrainingConfig
    {
        /// <summary>
        /// Recipe ID this config is tied to.
        /// </summary>
        public string RecipeId { get; set; }

        /// <summary>
        /// Training image size (both width and height; HALCON typically wants square images).
        /// Query this from the base model via get_dl_model_param("image_width").
        /// </summary>
        public int ImageWidth { get; set; } = 224;
        public int ImageHeight { get; set; } = 224;

        /// <summary>
        /// Batch size for training. Larger = faster but more GPU memory.
        /// Typical: 16–64 depending on GPU VRAM.
        /// </summary>
        public int BatchSize { get; set; } = 32;

        /// <summary>
        /// Number of training epochs. Start with 50–100 for initial evaluation.
        /// </summary>
        public int NumEpochs { get; set; } = 50;

        /// <summary>
        /// Learning rate for optimizer. Default 0.001 is a safe starting point.
        /// </summary>
        public double LearningRate { get; set; } = 0.001;

        /// <summary>
        /// Momentum for SGD or similar. Typically 0.9.
        /// </summary>
        public double Momentum { get; set; } = 0.9;

        /// <summary>
        /// Which GC-anomaly subnetworks to train: "local", "global", or "both".
        /// "both" (default) typically gives best results but uses more GPU memory.
        /// Can be optimized post-evaluation if only one type dominates your boards.
        /// </summary>
        public string GcAnomalyNetworks { get; set; } = "both";

        /// <summary>
        /// Fraction of training data to use as validation. Typical: 0.1–0.2.
        /// </summary>
        public double ValidationFraction { get; set; } = 0.15;

        /// <summary>
        /// Threshold for anomaly score to mark a region as defective.
        /// Will be calibrated post-training via compute_dl_anomaly_thresholds.
        /// Placeholder value; overwritten by evaluation.
        /// </summary>
        public double AnomalyScoreThreshold { get; set; } = 0.5;

        /// <summary>
        /// Per-class thresholds (if multi-class anomaly detection is used).
        /// Maps class name (e.g., "solder", "scratch") to threshold.
        /// </summary>
        public Dictionary<string, double> PerClassThresholds { get; set; } = new Dictionary<string, double>();

        /// <summary>
        /// Augmentation strategy. "none", "light", "strong".
        /// </summary>
        public string AugmentationStrategy { get; set; } = "light";

        /// <summary>
        /// Random seed for reproducibility.
        /// </summary>
        public int RandomSeed { get; set; } = 42;

        /// <summary>
        /// Timestamp of config creation/last update.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Free-form notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Compute a hash of config parameters for cache-invalidation.
        /// If this changes, retrain is needed.
        /// </summary>
        public string GetHash()
        {
            var combined = $"{ImageWidth}|{ImageHeight}|{BatchSize}|{NumEpochs}|{LearningRate}|{GcAnomalyNetworks}";
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(combined));
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }
}
