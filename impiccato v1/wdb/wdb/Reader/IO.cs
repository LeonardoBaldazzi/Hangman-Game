using System;
using System.IO;

namespace wdb.Reader
{
    public class IO
    {
        public static void ReadRequest()
        {
            //Directory.SetCurrentDirectory("./");
            
            string[] reqContent = File.ReadAllLines(Dipendences.requestPath);
            
            //First line = file name
            string fileName = reqContent[0];
            
            //Second line = group name
            string grName = reqContent[1];
            
            //Third line = what search?
            string whSearch = reqContent[2];
            
            //Read fileName
            Console.Write("[WDB ENGINE]: Reading '" + Dipendences.workingPath + fileName + "'....");
            
            string[] fileContent = File.ReadAllLines("./"+ fileName);
            Console.WriteLine("OK!");
            
            Console.Write("[WDB ENGINE]: Searching'" + grName + "'....");

            int startPos = -1;
            
            //Search startPos
            for (int i = 0; i < fileContent.Length; i++)
            {
                if (fileContent[i] == "<gr=" + grName + ">")
                    startPos = 0;
            }
            
            if(startPos == -1)
                throw  new Exception(grName + " not found");

            if (whSearch == "wd")
            {
                //Return all words
            } else if (whSearch.StartsWith("desc"))
            {
                //Return description
            }
            else
            {
                throw  new Exception(whSearch + " not valid");
            }
        }
    }
}