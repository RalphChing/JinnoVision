using JinnoVision.Models;

namespace JinnoVision.Services.Vision
{
    public class CodeReaderModule : IVisionModule<string, CodeReadResult>
    {
        public CodeReadResult Run(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return new CodeReadResult
                {
                    Success = false,
                    ErrorMessage = "Image path is empty."
                };
            }

            // Placeholder for HALCON logic later
            return new CodeReadResult
            {
                Success = true,
                CodeText = "TEST-123456",
                CodeType = "DataMatrix"
            };
        }
    }
}