using System.Collections.Generic;

namespace JinnoVision.App.Models
{
    public class Recipe
    {
        public string RecipeId { get; set; } = "";

        public string RecipeName { get; set; } = "";

        public List<RecipeRoi> Rois { get; set; } = new List<RecipeRoi>();
    }
}
