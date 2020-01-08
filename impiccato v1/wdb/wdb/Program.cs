using System;

namespace wdb
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //Splash Screen
                Console.WriteLine("##################################################################");
                Console.WriteLine("#                   WDB Reader v" + Dipendences.version + "                                #");
                Console.WriteLine("##################################################################");
                Console.WriteLine();
                Console.WriteLine("Checking files..........");
                
                //Check files
                Reader.Engine.Check();
                
                Console.WriteLine("Done!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                
                Console.Write("Press any key to continue........");
            }

            Console.ReadKey();
        }
    }
}