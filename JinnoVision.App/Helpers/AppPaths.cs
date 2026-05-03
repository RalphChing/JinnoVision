using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnoVision.App.Helpers
{
    public static class AppPaths
    {
        public static readonly string Root =
        @"C:\Users\user\source\repos\JinnoVision\JinnoVision.App";

        //Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

        public static readonly string Trainer =
            Path.Combine(Root, "Trainer");

        public static readonly string TrainingData =
            Path.Combine(Root, "TrainingData");

        public static readonly string Recipes =
            Path.Combine(Root, "Recipes");

        public static readonly string Models =
            Path.Combine(Root, "Models");
    }
}
