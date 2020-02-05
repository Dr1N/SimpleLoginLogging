using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EventGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Press [Enter] to stop...");
			try
			{
				using var generator = new EventGenerator(GetConnectionStringFromConfig());
				generator.Start();
				Console.ReadLine();
				generator.Stop();
				Console.WriteLine("Press any key to exit...");
				Console.ReadKey(true);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error. Critical: { ex.Message }");
			}		
        }

		static string GetConnectionStringFromConfig()
		{
			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();
			return config.GetConnectionString("Default");
		}
    }
}