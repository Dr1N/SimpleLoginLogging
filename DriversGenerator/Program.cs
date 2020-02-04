using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DriversGenerator
{
    class Program
    {
        #region Constants

        const string FILE_PATH = @"Names\us-500.csv";
        const string CSV_SEPARATOR = ",";
        const int NAME_PARTS = 2;

        #endregion

        static void Main(string[] args)
        {
            try
            {
                var driversNames = ReadNamesFromFile(FILE_PATH);
                AddDrivers(driversNames);

                Console.WriteLine("Drivers Created!");
                Console.Write("Press any key to continue...");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error. Critical: { ex.Message }");
            }
        }

        static IEnumerable<string> ReadNamesFromFile(string filePath)
        {
            var content = ReadFile(filePath);
            if (content.Count() == 0)
            {
                throw new ArgumentException("File with drivers names is empty");
            }

            var result = new List<string>();
            var firstLine = true;
            foreach (var line in content)
            {
                if (firstLine) // Skip first line in csv
                {
                    firstLine = false;
                    continue;
                }
                if (string.IsNullOrEmpty(line)) continue; // Skip empty lines
                try
                {
                    var parts = line.Split(CSV_SEPARATOR);
                    if (parts.Length >= NAME_PARTS) // Skip wrong line
                    {
                        result.Add($"{ parts[0].Replace("\"", "") } { parts[1].Replace("\"", "") }");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error. Name parsing: { ex.Message }");
                }
            }

            return result;
        }

        static IEnumerable<string> ReadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new ArgumentException(nameof(filePath));
            }

            var result = new List<string>();
            try
            {
                var content = File.ReadAllLines(filePath);
                result.AddRange(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error. Reading file error: { ex.Message }");
            }

            return result;
        }

        static void AddDrivers(IEnumerable<string> names)
        {
            if (names == null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            using var repo = new DriversRepository();
            repo.ClearDrivers();
            foreach (var name in names)
            {
                try
                {
                    var driver = CreateDriver(name);
                    repo.AddDriver(driver);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error. Add Driver: { ex.Message }");
                }
            }
        }

        static Driver CreateDriver(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            var result = new Driver()
            {
                Number = GenerateNumber(),
                FirstName = name.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0],
                LastName = name.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1],
            };

            return result;
        }

        static string GenerateNumber()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
        }
    }
}