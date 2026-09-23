using HalconDotNet;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace JinnoVision.App.Services.Vision.GcadAnomalyDetection
{
    /// <summary>
    /// Inference-only HALCON Global Context Anomaly Detection service.
    ///
    /// Training and threshold calibration now happen OUTSIDE this codebase, in the MVTec Deep
    /// Learning Tool. That tool handles the full training/evaluation/postprocessing workflow
    /// (including the normalize_dl_gc_anomaly_scores and compute_dl_anomaly_thresholds steps that
    /// have no operator binding and previously required HDevEngine) and exports a finished,
    /// calibrated .hdl model file. This class's only job is to load that file and run inference.
    ///
    /// Everything below uses only confirmed real HalconDotNet operators -- no HDevEngine, no
    /// stubs. This replaces HalconGcadTrainer.cs; that file's training-side code (TrainAsync,
    /// PrepareDataAsync, TrainDlModelBatch usage) is obsolete under this architecture and can be
    /// deleted once you've confirmed this works end to end.
    ///
    /// CONFIRMED against the HALCON 25.11 operator reference:
    ///   - HOperatorSet.ReadDlModel / GetDlModelParam / ApplyDlModel
    ///   - HOperatorSet.CreateDict / SetDictObject / GetDictTuple
    ///   - DLSample key: 'image'. DLResult key: 'anomaly_score' (plus _local/_global/_combined
    ///     variants, not used here since the exported model already combines both subnetworks
    ///     unless you configured otherwise in the Deep Learning Tool).
    ///
    /// ONE REMAINING SOFT SPOT (same as before, not eliminated by this change): the
    /// ConvertImageType + ScaleImage preprocessing pair is a reasonable extrapolation from
    /// confirmed adjacent operators, not copied from a verified working example. If inference
    /// throws or scores look implausible, check this first.
    /// </summary>
    public class GcadInferenceService : IDisposable
    {
        private HTuple _dlModelHandle;
        private int _imageWidth;
        private int _imageHeight;
        private double _imageRangeMin;
        private double _imageRangeMax;
        private double _anomalyScoreThreshold;
        private bool _modelLoaded;
        private bool _disposed;

        /// <summary>
        /// Loads a trained, calibrated model exported from the MVTec Deep Learning Tool.
        /// </summary>
        /// <param name="modelPath">Path to the exported .hdl file.</param>
        /// <param name="anomalyScoreThreshold">
        /// The threshold from the tool's "Configure Postprocessing" step -- scores at or above
        /// this value are classified as an anomaly ('nok').
        /// </param>
        public void LoadModel(string modelPath, double anomalyScoreThreshold)
        {
            ThrowIfDisposed();

            // Only pre-check full/rooted paths ourselves. A bare filename (no directory, like
            // one of HALCON's own stock models) is expected to resolve via read_dl_model's own
            // search logic -- it looks in $HALCONROOT/dl/ as well as the current directory,
            // locations .NET's File.Exists has no visibility into. Pre-rejecting a bare filename
            // here would incorrectly block every stock pretrained model from ever loading.
            if (Path.IsPathRooted(modelPath) && !File.Exists(modelPath))
                throw new FileNotFoundException($"GCAD model not found: {modelPath}");

            HOperatorSet.ReadDlModel(new HTuple(modelPath), out _dlModelHandle);

            HTuple imageWidth, imageHeight, imageRangeMin, imageRangeMax;
            HOperatorSet.GetDlModelParam(_dlModelHandle, new HTuple("image_width"), out imageWidth);
            HOperatorSet.GetDlModelParam(_dlModelHandle, new HTuple("image_height"), out imageHeight);
            HOperatorSet.GetDlModelParam(_dlModelHandle, new HTuple("image_range_min"), out imageRangeMin);
            HOperatorSet.GetDlModelParam(_dlModelHandle, new HTuple("image_range_max"), out imageRangeMax);

            _imageWidth = imageWidth.I;
            _imageHeight = imageHeight.I;
            _imageRangeMin = imageRangeMin.D;
            _imageRangeMax = imageRangeMax.D;
            _anomalyScoreThreshold = anomalyScoreThreshold;
            _modelLoaded = true;

            Debug.WriteLine(
                $"[GcadInferenceService] Loaded {Path.GetFileName(modelPath)}: " +
                $"{_imageWidth}x{_imageHeight}, range [{_imageRangeMin}, {_imageRangeMax}], " +
                $"threshold {_anomalyScoreThreshold}");
        }

        /// <summary>
        /// Runs GCAD inference on an image already on disk (useful for testing/evaluation against
        /// a folder of saved images).
        /// </summary>
        public GcadInspectionResult Inspect(string imagePath)
        {
            ThrowIfDisposed();
            ThrowIfModelNotLoaded();

            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Image not found: {imagePath}");

            HObject image;
            HOperatorSet.ReadImage(out image, imagePath);

            var result = Inspect(image);
            result.ImagePath = imagePath;
            return result;
        }

        /// <summary>
        /// Runs GCAD inference on an image already in memory -- the path for live inspection,
        /// so a captured board never has to round-trip through disk. Caller retains ownership of
        /// <paramref name="image"/> (HObject reference-counts internally; HALCON disposes the
        /// underlying data once the last reference goes out of scope).
        /// </summary>
        public GcadInspectionResult Inspect(HObject image)
        {
            ThrowIfDisposed();
            ThrowIfModelNotLoaded();

            HObject imageResized;
            HOperatorSet.ZoomImageSize(image, out imageResized, _imageWidth, _imageHeight, "constant");

            HObject imageReal;
            HOperatorSet.ConvertImageType(imageResized, out imageReal, "real");

            double mult = (_imageRangeMax - _imageRangeMin) / 255.0;
            double add = _imageRangeMin;
            HObject imageScaled;
            HOperatorSet.ScaleImage(imageReal, out imageScaled, mult, add);

            HTuple dlSample;
            HOperatorSet.CreateDict(out dlSample);
            HOperatorSet.SetDictObject(imageScaled, dlSample, new HTuple("image"));

            HTuple dlResult;
            HOperatorSet.ApplyDlModel(_dlModelHandle, dlSample, new HTuple(), out dlResult);

            HTuple anomalyScore;
            HOperatorSet.GetDictTuple(dlResult, new HTuple("anomaly_score"), out anomalyScore);

            double score = anomalyScore.D;

            return new GcadInspectionResult
            {
                ImagePath = null,
                AnomalyScore = score,
                Threshold = _anomalyScoreThreshold,
                IsAnomaly = score >= _anomalyScoreThreshold
            };
        }

        /// <summary>
        /// Runs GCAD inference on a System.Drawing.Bitmap -- the path for your camera service,
        /// which hands back Bitmap from both CaptureFrame() and the FrameReceived event.
        /// </summary>
        public GcadInspectionResult Inspect(Bitmap bitmap)
        {
            ThrowIfDisposed();
            ThrowIfModelNotLoaded();

            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap));

            HObject image = BitmapToHObject(bitmap);
            return Inspect(image);
        }

        /// <summary>
        /// Converts a GDI+ Bitmap to an HALCON HObject via the confirmed real operator
        /// GenImageInterleaved.
        ///
        /// CONFIRMED from the HALCON operator reference: GenImageInterleaved's C# signature is
        /// (out HObject, HTuple pixelPointer, HTuple colorFormat, HTuple originalWidth,
        /// HTuple originalHeight, HTuple alignment, HTuple type, HTuple imageWidth,
        /// HTuple imageHeight, HTuple startRow, HTuple startColumn, HTuple bitsPerChannel,
        /// HTuple bitShift). Passing 0 for imageWidth/imageHeight/startRow/startColumn means
        /// "same size as input, no cropping" -- also documented explicitly.
        ///
        /// Two things this depends on getting right, both confirmed separately:
        ///  - colorFormat "bgr": GDI+'s Format24bppRgb is misleadingly named -- the actual byte
        ///    order in memory is B, G, R per pixel, which is exactly what the 'bgr' color format
        ///    value is documented to mean (24-bit bgr triple, 8 bits per channel).
        ///  - The pixel pointer must be passed as a 64-bit value (via IntPtr.ToInt64()) on this
        ///    x64 build, or the address truncates -- called out explicitly in HALCON's own docs.
        ///
        /// SOFT SPOT: the 'alignment' parameter's exact accepted value range wasn't visible in
        /// what I could pull from the docs (truncated before that section). Passing 4 here on the
        /// reasoning that it means "row stride padded to a 4-byte boundary," which is what GDI+
        /// always does for Format24bppRgb -- if this throws or produces a visibly skewed/sheared
        /// image, this is the parameter to check first.
        /// </summary>
        private static HObject BitmapToHObject(Bitmap bitmap)
        {
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            try
            {
                HObject image;
                HOperatorSet.GenImageInterleaved(
                    out image,
                    new HTuple(bmpData.Scan0.ToInt64()),
                    new HTuple("bgr"),
                    new HTuple(bitmap.Width),
                    new HTuple(bitmap.Height),
                    new HTuple(4),
                    new HTuple("byte"),
                    new HTuple(0),
                    new HTuple(0),
                    new HTuple(0),
                    new HTuple(0),
                    new HTuple(8),
                    new HTuple(0));

                return image;
            }
            finally
            {
                bitmap.UnlockBits(bmpData);
            }
        }

        // TEST-ONLY: exposes the private conversion for isolated testing in GcadSmokeTest.
        // Do not carry this method back into the production copy of this file.
        public static HObject DebugBitmapToHObject(Bitmap bitmap) => BitmapToHObject(bitmap);

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(GcadInferenceService));
        }

        private void ThrowIfModelNotLoaded()
        {
            if (!_modelLoaded)
                throw new InvalidOperationException("Call LoadModel before Inspect");
        }
    }

    public class GcadInspectionResult
    {
        public string ImagePath { get; set; }
        public double AnomalyScore { get; set; }
        public double Threshold { get; set; }
        public bool IsAnomaly { get; set; }
    }
}