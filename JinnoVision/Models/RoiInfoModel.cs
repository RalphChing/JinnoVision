namespace JinnoVision.Models
{
    public class RoiInfoModel
    {
        public string RoiId { get; set; }
        public string RoiName { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public string RoiType { get; set; }

        public string ImagePath { get; set; }
        public string TargetClassName { get; set; }
        public string Classification { get; set; }
        public int ConfidenceThreshold { get; set; }
    }
}