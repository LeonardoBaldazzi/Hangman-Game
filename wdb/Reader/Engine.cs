using System;
using System.IO;

namespace wdb.Reader
{
    public class Engine
    {
        public static void Check()
        {
            int count = 0;
            
            int fileCount = Directory.GetFiles(Dipendences.workingPath, "*.wdb", SearchOption.TopDirectoryOnly).Length; //Count of 'wdb files

            if (fileCount > 0)
            {
                Directory.SetCurrentDirectory(Dipendences.workingPath);
                
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

                count = 0;
                
                //Check if files are empty
                Console.Write("[WDB ENGINE]: Checking if files are empty......");

                for (int i = 0; i < fileCount; i++)
                {
                    if (new FileInfo(Dipendences.fileNames[count]).Length == 0)
                    {
                        throw  new Exception("File " + Dipendences.fileNames[count] + " is empty, aborting"); //File is empty!
                    }

                    count++;
                }
                
                Console.WriteLine("OK!");

                count = 0;
                
                Console.WriteLine("[WDB ENGINE]: Compiling Files....");

                for (int i = 0; i < fileCount; i++)
                {
                    CheckFileGrammar(Dipendences.fileNames[count]);

                    count++;
                }
            }
            else
            {
                throw  new Exception("No files in '" + Dipendences.workingPath + "' directory"); //No files matched
            }
        }

        private static void CheckFileGrammar(string fileName)
        {
            string[] content = new string[new FileInfo(fileName).Length];
            int[] alreadyExistId = new int[1000];
            string tmpID = "";
            string tmpContent = "";
            
            string param = "";

            bool idFatto = false;
            
            Console.WriteLine("              [WDB CHECKER]: Checking wdb grammar in " + fileName + " file");

            content = File.ReadAllLines(fileName);
            
            Console.WriteLine("                  [WDB CHECKER]: Checking lines....");
            for (int i = 0; i < content.Length; i++)
            {
                tmpID = "";
                tmpContent = "";
                
                if (content[i].StartsWith("<wd") || content[i].StartsWith("<desc") || content[i].StartsWith("<gr") )
                {
                    if (content[i].StartsWith("<wd"))
                    {
                        //Word
                        
                        param = "wd";
                        
                        //Check id
                        for (int x = 0; x < 7; x++)
                        {
                            if (content[i][x] == 'i' && content[i][x + 1] == 'd')
                            {
                                idFatto = true;
                                int counter = 7;

                                //Calc tmpID
                                for (; true;)
                                {
                                    if (content[i][counter] == ' ')
                                    {
                                        break;
                                    }
                                    
                                    if (content[i][counter] == '0' || content[i][counter] == '1' ||
                                        content[i][counter] == '2' || content[i][counter] == '3' ||
                                        content[i][counter] == '4' || content[i][counter] == '5' ||
                                        content[i][counter] == '6' || content[i][counter] == '7' ||
                                        content[i][counter] == '8' || content[i][counter] == '9')
                                    {
                                        tmpID = tmpID + content[i][counter];
                                    } else
                                    {
                                        throw new Exception("Line: " + i + ": Id not correct");
                                    }
                                    
                                    counter++;
                                }
                                
                                if (Array.IndexOf(alreadyExistId, Convert.ToInt32(tmpID)) == Convert.ToInt32(tmpID) + 1)
                                {
                                    throw new Exception("Line: " + i + ": Id already exist");
                                }
                                else
                                {
                                    alreadyExistId[Convert.ToInt32(tmpID)] = Convert.ToInt32(tmpID) + 1;
                                }
                                
                                break;
                            }
                        }

                        if (idFatto == false)
                        {
                            throw new Exception("Line: " + i + ": No id matched");
                        }
                        
                        //Match Contents
                        int count = 8 + tmpID.Length;
                        string matched = "";

                        if (count < content[i].Length)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                matched += content[i][count];

                                count++;
                            }

                            if (matched == "content=")
                            {
                                if (count < content[i].Length)
                                {
                                    for (; true;)
                                    {
                                        if (content[i][count] == '>')
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            tmpContent += content[i][count];
                                        }

                                        count++;
                                    }

                                    if (string.IsNullOrEmpty(tmpContent))
                                    {
                                        throw new Exception("Line: " + i + ": No content matched");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Line: " + i + ": Grammar error");
                                }
                            }
                            else
                            {
                                throw new Exception("Line: " + i + ": Grammar error in content side: " + matched + " not works");
                            }
                        }
                        else
                        {
                            throw new Exception("Line: " + i + ": Grammar error");
                        }
                    }
                    else if (content[i].StartsWith("<desc"))
                    {
                        //Description
                        
                        param = "desc";
                        
                         //Check id
                        for (int x = 0; x < 7; x++)
                        {
                            if (content[i][x] == 'i' && content[i][x + 1] == 'd')
                            {
                                idFatto = true;
                                int counter = 9;

                                //Calc tmpID
                                for (; true;)
                                {
                                    if (content[i][counter] == ' ')
                                    {
                                        break;
                                    }
                                    
                                    if (content[i][counter] == '0' || content[i][counter] == '1' ||
                                        content[i][counter] == '2' || content[i][counter] == '3' ||
                                        content[i][counter] == '4' || content[i][counter] == '5' ||
                                        content[i][counter] == '6' || content[i][counter] == '7' ||
                                        content[i][counter] == '8' || content[i][counter] == '9')
                                    {
                                        tmpID = tmpID + content[i][counter];
                                    } else
                                    {
                                        throw new Exception("Line: " + i + ": Id not correct");
                                    }
                                    
                                    counter++;
                                }
                                
                                /*
                                if ((Array.IndexOf(alreadyExistId, Convert.ToInt32(tmpID)) != Convert.ToInt32(tmpID) + 1))
                                {
                                    throw new Exception("Line: " + i + ": Id not exist");
                                } */

                                break;
                            }
                        }

                        if (idFatto == false)
                        {
                            throw new Exception("Line: " + i + ": No id matched");
                        }
                        
                        //Match Contents
                        int count = 10 + tmpID.Length;
                        string matched = "";

                        if (count < content[i].Length)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                matched += content[i][count];

                                count++;
                            }

                            if (matched == "content=")
                            {
                                if (count < content[i].Length)
                                {
                                    for (; true;)
                                    {
                                        if (content[i][count] == '>')
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            tmpContent += content[i][count];
                                        }

                                        count++;
                                    }

                                    if (string.IsNullOrEmpty(tmpContent))
                                    {
                                        throw new Exception("Line: " + i + ": No content matched");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Line: " + i + ": Grammar error");
                                }
                            }
                            else
                            {
                                throw new Exception("Line: " + i + ": Grammar error in content side: " + matched + " not works");
                            }
                        }
                        else
                        {
                            throw new Exception("Line: " + i + ": Grammar error");
                        }
                    }
                    else
                    {
                        //Group
                        
                        param = "gr";

                        if (i < content.Length)
                        {
                            if (4 < content[i].Length)
                            {
                                //Get group name
                                for (int x = 4; x < content[i].Length; x++)
                                {
                                    if (content[i][x] == '>')
                                    { break; }
                                    
                                    tmpContent += content[i][x];
                                }

                                int l = 0;
                                
                                //Get group lenght
                                for (int x = i; x < content.Length; x++)
                                {
                                    if (content[i] == "</gr>")
                                    { break; }

                                    l++;
                                }

                                tmpContent = tmpContent + " (" + l + ")";
                            }
                            else
                            {
                                throw new Exception("Line: " + i + ": Grammar error");
                            }
                        }
                        else
                        {
                            throw new Exception("Line: " + i + ": Grammar error");
                        }
                    }
                }
                else if (content[i] == "</gr>")
                { //Nothing
                    param = "Stop gr";
                    tmpContent = "No Content";
                } else
                {
                    throw new Exception("Line: " + i + ": Grammar error");
                }
                
                Console.WriteLine("                  [WDB CHECKER]: Matched a " + param + " line (" + i +") with content: " + tmpContent);
            }
        }
    }
}