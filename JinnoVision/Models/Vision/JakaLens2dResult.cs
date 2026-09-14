namespace JinnoVision.Models.Vision
{
    public class JakaLens2DResult
    {
        public bool Success { get; set; }
        public int TargetCount { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Rz { get; set; }

        public string RawResponse { get; set; }
    }
}
