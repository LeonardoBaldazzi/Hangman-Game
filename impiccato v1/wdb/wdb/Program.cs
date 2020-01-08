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
                Console.WriteLine("#                       WDB Reader v" + Dipendences.version + "                            #");
                Console.WriteLine("##################################################################");
                Console.WriteLine();
                Console.WriteLine("[WDB ENGINE]: Working path: ../../src/");
                
                //Check files
                Reader.Engine.Check();
                
                Console.WriteLine("[WDB ENGINE]: All files works, starting local server");
                
                //Starting server via socket
                Server.Main.Start();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                
                Console.WriteLine();
                
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                
                Console.Write("Press any key to continue........");
            }

            Console.ReadKey();
        }
    }
}