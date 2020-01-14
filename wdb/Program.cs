using System;
using System.IO;
using wdb.Reader;

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
                Console.WriteLine("#                       WDB Reader v" + Dipendences.version + "                            #");
                Console.WriteLine("##################################################################");
                Console.WriteLine();
                
                Console.WriteLine("Programmed by: Leonardo Baldazzi");

                string[] cfg = File.ReadAllLines("./config.cfg");

                Dipendences.workingPath = cfg[0];
                Dipendences.requestPath = cfg[1];
                Dipendences.outputPath = cfg[2];
                
                Console.WriteLine("[WDB ENGINE]: Working path: " + Dipendences.workingPath);
                Console.WriteLine("[WDB ENGINE]: Request file path: " + Dipendences.requestPath);
                Console.WriteLine("[WDB ENGINE]: Output path: " + Dipendences.outputPath);
                
                //Check files
                Reader.Engine.Check();
                
                Console.WriteLine("[WDB ENGINE]: All files works, Reading request file");

                IO.ReadRequest();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                
                Console.WriteLine();
                
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                
                Console.Write("Press any key to continue........");
                Console.ReadKey();
            }
        }
    }
}