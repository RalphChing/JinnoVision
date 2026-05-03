using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnoVision.App.Models
{
    public class InspectionResult
    {
        public string RoiName { get; set; } = "";

        public string ExpectedComponent { get; set; } = "";

        public string PredictedComponent { get; set; } = "";

        public string PredictedStatus { get; set; } = "";

        public float Confidence { get; set; }

        public float Threshold { get; set; }

        public string FinalResult { get; set; } = "";
    }
}
