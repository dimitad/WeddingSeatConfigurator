using System;

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

            try
            {
                var fileLoader = new FileLoader(args[0]);
                var config = fileLoader.Read();

                var weddingSeatingCreator = new WeddingSeatingCreator.WeddingSeatingCreator(config);
                weddingSeatingCreator.AssignWeddingTables();

                Console.WriteLine(weddingSeatingCreator.GetTableAssignments());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadLine();
        }

        private static void ShowUsage()
        {
            Console.WriteLine("To run this application please use the following syntax: WeddingSeater [SeatingFile.txt]");
        }
    }
}
