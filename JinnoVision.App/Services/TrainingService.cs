using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JinnoVision.App.Helpers;


namespace JinnoVision.App.Services
{
    public class TrainingService
    {
        public async Task<string> RetrainAsync()
        {
            return await Task.Run(() =>
            {
                var scriptPath = Path.Combine(AppPaths.Trainer, "train_classifier.py");

                if (!File.Exists(scriptPath))
                    throw new FileNotFoundException("Trainer script not found", scriptPath);

                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{scriptPath}\"",
                    WorkingDirectory = AppPaths.Trainer,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                        throw new Exception(error);

                    return output;
                }
            });
        }
    }
}
