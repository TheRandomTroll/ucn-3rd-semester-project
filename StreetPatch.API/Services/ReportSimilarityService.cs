using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using StreetPatch.Data.Entities;
using StreetPatch.Data.Entities.Base;

namespace StreetPatch.API.Services
{
    public class ReportSimilarityService
    {
        public static double CalculateSimilarity(Report report1, Report report2)
        {
            // Calculate similarity for all fields, except title and description
            double similarity = 0;
            if (report1.Type == report2.Type)
            {
                similarity += 0.3;
            }

            var distance = Haversine(report1.Coordinates, report2.Coordinates);
            if (distance < 100) // Ensure we don't divide by zero
            {
                distance = 100;
            }

            similarity += 0.5 / Math.Log(distance, 100);

            var titleAndDescriptionSimilarity = GetTitleAndDescriptionSimilarity(report1, report2);

            similarity += titleAndDescriptionSimilarity[0] * 0.15 + titleAndDescriptionSimilarity[1] * 0.05;

            return Math.Round(similarity, 2);
        }

        private static double Haversine(Coordinates report1Coordinates, Coordinates report2Coordinates)
        {
            // Calculate distance using the Haversine formula
            const double radius = 6371e3; // Meters
            // Convert degrees to radians
            var phi1 = report1Coordinates.Latitude * Math.PI / 180d;
            var phi2 = report2Coordinates.Latitude * Math.PI / 180d;
            var deltaPhi = (report2Coordinates.Latitude - report1Coordinates.Latitude) * Math.PI / 180d;
            var deltaLambda = (report2Coordinates.Longitude - report1Coordinates.Longitude) * Math.PI / 180d;

            var a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                    Math.Cos(phi1) * Math.Cos(phi2) *
                    Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return radius * c;
        }

        private static double[] GetTitleAndDescriptionSimilarity(Report report1, Report report2)
        {
            var entries = Environment.GetEnvironmentVariable("path")?.Split(';');
            string pythonLocation = null;

            foreach (var entry in entries)
            {
                if (!entry.ToLower().Contains("python3")) continue;
                var breadcrumbs = entry.Split('\\');
                foreach (var breadcrumb in breadcrumbs)
                {
                    if (breadcrumb.ToLower().Contains("python"))
                    {
                        pythonLocation += breadcrumb + '\\';
                        break;
                    }
                    pythonLocation += breadcrumb + '\\';
                }
                break;
            }

            pythonLocation += "\\python.exe";

            var pythonScriptPath = FindPathToDir("calculate_similarity.py");

            Console.WriteLine(pythonLocation);
            var start = new ProcessStartInfo
            {
                FileName = pythonLocation,
                Arguments = $"{pythonScriptPath} \"${report1.Title}\" \"${report2.Title}\" \"${report2.Description}\" \"${report2.Description}\" ",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Verb = "runas"
            };
            using var process = Process.Start(start);
            using var reader = process.StandardOutput;
            return reader.ReadToEnd()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray(); // This returns the title similarity, then the description similarity
        }

        private static string FindPathToDir(string localPath)
        {
            var currentDir = Environment.CurrentDirectory;
            var directory = new DirectoryInfo(
                Path.GetFullPath(Path.Combine(currentDir, @"Services\PythonScripts\" + localPath)));
            return directory.ToString();
        }
    }
}
