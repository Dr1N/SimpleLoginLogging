using System;
using System.Collections.Generic;
using System.IO;

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
                AddDrivers(ReadNames());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error. Critical: {ex.Message}");
            }
        }

        static IEnumerable<string> ReadNames()
        {
            var result = new List<string>();
            try
            {
                var content = File.ReadAllLines(FILE_PATH);
                if (content?.Length != 0)
                {
                    for (int i = 0; i < content.Length; i++)
                    {
                        if (i == 0) continue;

                        var line = content[i];

                        if (string.IsNullOrEmpty(line)) continue;

                        try
                        {
                            var parts = line.Split(CSV_SEPARATOR);
                            if (parts.Length >= NAME_PARTS)
                            {
                                result.Add($"{parts[0].Replace("\"", "")} {parts[1].Replace("\"", "")}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error. Name parsing: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error. File error: {ex.Message}");
            }

            return result;
        }

        static void AddDrivers(IEnumerable<string> names)
        {
            if (names == null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}