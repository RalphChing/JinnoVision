using System.Collections.Generic;

namespace JinnoVision.Models
{
    public class RecipeModel
    {
        public string RecipeId { get; set; }
        public string RecipeName { get; set; }
        public bool IsActive { get; set; }

        public List<InspectionStepModel> Steps { get; set; } = new List<InspectionStepModel>();

        public string GcadModelPath { get; set; }

        public double GcadAnomalyThreshold { get; set; } = 0.5;
    }
}