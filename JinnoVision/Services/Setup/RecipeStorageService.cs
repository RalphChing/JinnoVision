using JinnoVision.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace JinnoVision.Services.Setup
{
    public class RecipeStorageService
    {
        private readonly string _baseFolder;

        public RecipeStorageService()
        {
            _baseFolder = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TempRecipes")
            );

            Directory.CreateDirectory(_baseFolder);
        }

        public void SaveRecipe(RecipeModel recipe)
        {
            string recipeFolder = Path.Combine(
                _baseFolder,
                MakeSafeFileName($"{recipe.RecipeId}_{recipe.RecipeName}")
            );

            Directory.CreateDirectory(recipeFolder);

            foreach (var step in recipe.Steps)
            {
                string stepFolder = Path.Combine(recipeFolder, $"Step_{step.StepNo:000}");
                Directory.CreateDirectory(stepFolder);

                foreach (var roi in step.Rois)
                {
                    string roiFolder = Path.Combine(
                        stepFolder,
                        MakeSafeFileName($"{roi.RoiId}_{roi.RoiName}")
                    );

                    Directory.CreateDirectory(roiFolder);

                    roi.ImagePath = Path.Combine(roiFolder, "template.png");

                    SaveJson(Path.Combine(roiFolder, "roi.json"), roi);
                }

                SaveJson(Path.Combine(stepFolder, "step.json"), step);
            }

            SaveJson(Path.Combine(recipeFolder, "recipe.json"), recipe);
        }

        private void SaveJson<T>(string path, T data)
        {
            string json = JsonSerializer.Serialize(
                data,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(path, json);
        }

        private string MakeSafeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');

            return name.Trim();
        }
        public List<RecipeModel> LoadAllRecipes()
        {
            var recipes = new List<RecipeModel>();

            if (!Directory.Exists(_baseFolder))
                return recipes;

            foreach (var folder in Directory.GetDirectories(_baseFolder))
            {
                string jsonPath = Path.Combine(folder, "recipe.json");

                if (!File.Exists(jsonPath))
                    continue;

                string json = File.ReadAllText(jsonPath);

                var recipe = JsonSerializer.Deserialize<RecipeModel>(json);

                if (recipe != null)
                    recipes.Add(recipe);
            }

            return recipes;
        }

        public RecipeModel LoadMostRecentRecipe()
        {
            if (!Directory.Exists(_baseFolder))
                return null;

            var latestDir = new DirectoryInfo(_baseFolder)
                .GetDirectories()
                .OrderByDescending(d => d.LastWriteTime)
                .FirstOrDefault();

            if (latestDir == null)
                return null;

            string jsonPath = Path.Combine(latestDir.FullName, "recipe.json");

            if (!File.Exists(jsonPath))
                return null;

            string json = File.ReadAllText(jsonPath);

            return JsonSerializer.Deserialize<RecipeModel>(json);
        }
    }
}