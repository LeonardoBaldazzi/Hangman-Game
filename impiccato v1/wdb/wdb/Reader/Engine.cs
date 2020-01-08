using System;
using System.IO;

namespace wdb.Reader
{
    public class Engine
    {
        public static void Check()
        {
            int count = 0;
            
            int fileCount = Directory.GetFiles("../../src/", "*.wdb", SearchOption.TopDirectoryOnly).Length; //Count of 'wdb files

            if (fileCount > 0)
            {
                Directory.SetCurrentDirectory("../../src/");
                
                Console.WriteLine("[WDB ENGINE]: Checked " + fileCount + " '.wdb' files!");
                
                Dipendences.fileNames = new string[fileCount];
                
                Console.Write("[WDB ENGINE]: Getting file names.....");

                DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());
                
                //Get filenames and put in an array
                FileInfo[] Files = d.GetFiles("*.wdb");

                foreach (FileInfo file in Files)
                {
                    Dipendences.fileNames[count] = file.Name;

                    count++;
                }

                Console.WriteLine("OK!");
                
                //Check if files are empty
                Console.WriteLine("[WDB ENGINE]: Checking if files are empty......");
            }
            else
            {
                Console.WriteLine("[WDB ENGINE]: No files in '../../src/' directory");
            }
        }
    }
}