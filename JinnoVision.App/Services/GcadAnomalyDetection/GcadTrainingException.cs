using System;

namespace JinnoVision.App.Services.Vision.GcadAnomalyDetection
{
    public class GcadTrainingException : Exception
    {
        public GcadTrainingException(string message) : base(message) { }
        public GcadTrainingException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}