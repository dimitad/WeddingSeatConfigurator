using System;
using System.Linq;

namespace WeddingSeating
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                ShowUsage();
                return;
            }

            var fileLoader = new FileLoader(args[0]);
            var config = fileLoader.Read();

            if (!config.Any())
            {
                Console.WriteLine("Source file is blank. Please provide a valid file.");
                return;
            }

            var weddingSeatingCreator = new WeddingSeatingCreator.WeddingSeatingCreator(config);
            weddingSeatingCreator.AssignWeddingTables();
            
            Console.WriteLine(weddingSeatingCreator.GetTableAssignments());
            Console.ReadLine();
        }

        private static void ShowUsage()
        {
            Console.WriteLine("To run this application please use the following syntax: WeddingSeater [SeatingFile.txt]");
        }
    }
}
