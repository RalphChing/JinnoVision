using JinnoVision.App.Models;
using JinnoVision.App.Services;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace JinnoVision.App.Core
{
    public class InspectionEngine
    {
        private readonly OnnxClassifier _classifier;

        public InspectionEngine(OnnxClassifier classifier)
        {
            _classifier = classifier ?? throw new ArgumentNullException(nameof(classifier));
        }

        public List<InspectionResult> InspectFrame(Bitmap frame, List<RecipeRoi> rois)
        {
            var results = new List<InspectionResult>();

            if (frame == null || rois == null)
                return results;

            foreach (var roi in rois)
            {
                results.Add(InspectRoi(frame, roi));
            }

            return results;
        }

        private InspectionResult InspectRoi(Bitmap fullImage, RecipeRoi roi)
        {
            Rectangle rect = new Rectangle(
                roi.X,
                roi.Y,
                roi.Width,
                roi.Height);

            rect.Intersect(new Rectangle(0, 0, fullImage.Width, fullImage.Height));

            if (rect.Width <= 0 || rect.Height <= 0)
            {
                return new InspectionResult
                {
                    RoiName = roi.Name,
                    ExpectedComponent = roi.ComponentName,
                    FinalResult = "INVALID ROI"
                };
            }

            using (Bitmap cropped = fullImage.Clone(rect, fullImage.PixelFormat))
            {
                var prediction = _classifier.Predict(cropped);

                string predictedComponent = "";
                string predictedStatus = "";

                string[] parts = prediction.Label.Split('/');

                if (parts.Length >= 2)
                {
                    predictedComponent = parts[0];
                    predictedStatus = parts[1];
                }

                string finalResult;

                //if (predictedComponent != roi.ComponentName)
                //{
                //    finalResult = "WRONG COMPONENT";
                //}
                //else
                if (prediction.Confidence < roi.Threshold)
                {
                    finalResult = "UNCERTAIN";
                }
                else if (predictedStatus == "Pass")
                {
                    finalResult = "PASS";
                }
                else
                {
                    finalResult = "FAIL";
                }

                return new InspectionResult
                {
                    RoiName = roi.Name,
                    ExpectedComponent = roi.ComponentName,
                    PredictedComponent = predictedComponent,
                    PredictedStatus = predictedStatus,
                    Confidence = prediction.Confidence,
                    Threshold = roi.Threshold,
                    FinalResult = finalResult
                };
            }
        }
    }
}