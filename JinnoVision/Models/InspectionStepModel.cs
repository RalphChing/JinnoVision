using System.Collections.Generic;

namespace JinnoVision.Models
{
    public class InspectionStepModel
    {
        public int StepNo { get; set; }
        public string StepName { get; set; }

        public List<RoiInfoModel> Rois { get; set; } = new List<RoiInfoModel>();
    }
}