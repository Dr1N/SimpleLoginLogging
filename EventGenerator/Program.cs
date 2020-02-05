using System;

namespace EventGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Press any key to stop...");
			try
			{
				using var generator = new EventGenerator();
				generator.Start();
				Console.ReadKey(true);
				generator.Stop();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error. Critical: { ex.Message }");
			}		
        }
    }
}