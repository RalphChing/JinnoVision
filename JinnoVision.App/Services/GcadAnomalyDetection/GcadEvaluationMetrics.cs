using System;
using System.Collections.Generic;

namespace JinnoVision.App.Services.Vision.GcadAnomalyDetection
{
    /// <summary>
    /// Board-level evaluation metrics for GCAD model.
    /// Focuses on pass/fail detection rates (the 90% reliability KPI) rather than just pixel-level IoU.
    /// </summary>
    public class GcadEvaluationMetrics
    {
        /// <summary>
        /// Total number of boards in evaluation set.
        /// </summary>
        public int TotalBoards { get; set; }

        /// <summary>
        /// Number of boards correctly identified as OK (true negatives).
        /// </summary>
        public int TrueNegatives { get; set; }

        /// <summary>
        /// Number of good boards incorrectly flagged as defective (false positives).
        /// </summary>
        public int FalsePositives { get; set; }

        /// <summary>
        /// Number of defective boards correctly identified (true positives).
        /// </summary>
        public int TruePositives { get; set; }

        /// <summary>
        /// Number of defective boards missed (false negatives).
        /// </summary>
        public int FalseNegatives { get; set; }

        /// <summary>
        /// Board-level recall: TP / (TP + FN).
        /// This is the "catch rate" — what fraction of actual defects do we catch?
        /// Target: >= 0.90 for 90% reliability KPI.
        /// </summary>
        public double Recall { get; set; }

        /// <summary>
        /// Board-level precision: TP / (TP + FP).
        /// What fraction of flagged boards are actually defective?
        /// High FP rate = wasting operator time, so balance with Recall.
        /// </summary>
        public double Precision { get; set; }

        /// <summary>
        /// Board-level F1 score: harmonic mean of Recall and Precision.
        /// Good balance metric.
        /// </summary>
        public double F1Score { get; set; }

        /// <summary>
        /// False positive rate: FP / (FP + TN).
        /// Fraction of good boards we incorrectly reject.
        /// Target: as low as possible (ideally < 0.05 for 5% false reject).
        /// </summary>
        public double FalsePositiveRate { get; set; }

        /// <summary>
        /// Per-defect-type metrics (if available from pixel-level annotations).
        /// Key = defect type (e.g., "solder", "scratch"), Value = per-type recall.
        /// </summary>
        public Dictionary<string, double> PerDefectTypeRecall { get; set; } = new Dictionary<string, double>();

        /// <summary>
        /// Pixel-level mean IoU (for reference; not the primary KPI).
        /// </summary>
        public double PixelLevelIoU { get; set; }

        /// <summary>
        /// Timestamp of evaluation.
        /// </summary>
        public DateTime EvaluatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Recommended anomaly score threshold based on calibration.
        /// (Will be set by compute_dl_anomaly_thresholds during evaluation.)
        /// </summary>
        public double RecommendedThreshold { get; set; }

        /// <summary>
        /// Overall pass/fail: do we meet the 90% reliability target?
        /// </summary>
        public bool MeetsReliabilityTarget => Recall >= 0.90;

        /// <summary>
        /// Human-readable summary.
        /// </summary>
        public override string ToString()
        {
            return $"GCAD Evaluation\n" +
                   $"  Boards: {TotalBoards}\n" +
                   $"  Recall (catch rate): {Recall:P2}\n" +
                   $"  Precision: {Precision:P2}\n" +
                   $"  F1: {F1Score:P2}\n" +
                   $"  False Positive Rate: {FalsePositiveRate:P2}\n" +
                   $"  Pixel IoU: {PixelLevelIoU:P2}\n" +
                   $"  Recommended Threshold: {RecommendedThreshold:F4}\n" +
                   $"  Meets 90% Target: {MeetsReliabilityTarget}";
        }
    }
}
