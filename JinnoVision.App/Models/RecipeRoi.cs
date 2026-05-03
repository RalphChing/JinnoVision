using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnoVision.App.Models
{
    public class RecipeRoi
    {
        public string Name { get; set; } = "";

        public string ComponentName { get; set; } = "";

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        // Example: 0.85 = 85%
        public float Threshold { get; set; } = 0.85f;
    }
}
