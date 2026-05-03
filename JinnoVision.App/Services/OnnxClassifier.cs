using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using JinnoVision.App.Helpers;
using JinnoVision.App.ModelML;

namespace JinnoVision.App.Services
{
    public class OnnxClassifier : IDisposable
    {
        private readonly InferenceSession _session;
        private readonly List<string> _labels;

        public OnnxClassifier()
        {
            string modelPath = Path.Combine(AppPaths.Models, "component_passfail.onnx");
            string labelsPath = Path.Combine(AppPaths.Models, "labels.json");

            if (!File.Exists(modelPath))
                throw new FileNotFoundException("ONNX model not found", modelPath);

            if (!File.Exists(labelsPath))
                throw new FileNotFoundException("Labels file not found", labelsPath);

            _session = new InferenceSession(modelPath);

            string json = File.ReadAllText(labelsPath);
            _labels = JsonSerializer.Deserialize<List<string>>(json);
        }

        public ModelPrediction Predict(Bitmap bitmap)
        {
            using (var resized = new Bitmap(bitmap, new Size(224, 224)))
            {
                var input = new DenseTensor<float>(new[] { 1, 3, 224, 224 });

                for (int y = 0; y < 224; y++)
                {
                    for (int x = 0; x < 224; x++)
                    {
                        Color pixel = resized.GetPixel(x, y);

                        input[0, 0, y, x] = ((pixel.R / 255f) - 0.485f) / 0.229f;
                        input[0, 1, y, x] = ((pixel.G / 255f) - 0.456f) / 0.224f;
                        input[0, 2, y, x] = ((pixel.B / 255f) - 0.406f) / 0.225f;
                    }
                }

                var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input", input)
            };

                using (var results = _session.Run(inputs))
                {
                    float[] output = results.First().AsEnumerable<float>().ToArray();
                    float[] probabilities = Softmax(output);

                    int bestIndex = Array.IndexOf(probabilities, probabilities.Max());

                    return new ModelPrediction
                    {
                        Label = _labels[bestIndex],
                        Confidence = probabilities[bestIndex]
                    };
                }
            }
        }

        private float[] Softmax(float[] values)
        {
            float max = values.Max();
            float[] exp = values.Select(v => (float)Math.Exp(v - max)).ToArray();
            float sum = exp.Sum();

            return exp.Select(v => v / sum).ToArray();
        }

        public void Dispose()
        {
            _session?.Dispose();
        }
    }
}
