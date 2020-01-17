using System;
using System.IO;

namespace wdb.Reader
{
    public class IO
    {
        public static void ReadRequest()
        {
            //Array to write
            string[] arrToWrite = new string[1000];

            string[] reqContent = File.ReadAllLines(Dipendences.requestPath);
            
            //First line = file name
            string fileName = reqContent[0];
            
            //Second line = group name
           // string grName = reqContent[1];
            
            //Third line = what search?
            string whSearch = reqContent[2];
            
            //Read fileName
            Console.Write("[WDB ENGINE]: Reading '" + Dipendences.workingPath + fileName + "'....");
            
            string[] fileContent = File.ReadAllLines("./"+ fileName);
            Console.WriteLine("OK!");
            
            //Console.Write("[WDB ENGINE]: Searching'" + grName + "'....");
            
            Console.WriteLine("OK!");
            
            Console.Write("[WDB ENGINE]: Matching all components in " + fileName + " file.....");

            if (whSearch == "wd")
            {
                //Return all words
                string tmpID = "";
                string tmpContent = "";
                
                for (int i = 0; i < fileContent.Length; i++)
                {
                    tmpID = "";
                    tmpContent = "";
                   

                    if (fileContent[i].StartsWith("<wd"))
                    {
                        //Check id

                        for (int x = 0; x < 7; x++)
                        {
                            if (fileContent[i][x] == 'i' && fileContent[i][x + 1] == 'd')
                            {
                                int counter = 7;

                                //Calc tmpID
                                for (; true;)
                                {
                                    if (fileContent[i][counter] == ' ')
                                    {
                                        break;
                                    }

                                    if (fileContent[i][counter] == '0' || fileContent[i][counter] == '1' ||
                                        fileContent[i][counter] == '2' || fileContent[i][counter] == '3' ||
                                        fileContent[i][counter] == '4' || fileContent[i][counter] == '5' ||
                                        fileContent[i][counter] == '6' || fileContent[i][counter] == '7' ||
                                        fileContent[i][counter] == '8' || fileContent[i][counter] == '9')
                                    {
                                        tmpID = tmpID + fileContent[i][counter];
                                    }

                                    counter++;
                                }

                                break;
                            }
                        }

                        //Match Contents
                        int count = 8 + tmpID.Length;
                        string matched = "";

                        if (count < fileContent[i].Length)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                matched += fileContent[i][count];

                                count++;
                            }

                            if (matched == "content=")
                            {
                                if (count < fileContent[i].Length)
                                {
                                    for (; true;)
                                    {
                                        if (fileContent[i][count] == '>')
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            tmpContent += fileContent[i][count];
                                        }

                                        count++;
                                    }
                                }
                            }
                        }
                        arrToWrite[int.Parse(tmpID)] = tmpContent;
                    }
                }
            } else if (whSearch.StartsWith("desc"))
            {
                //Return description
                string id = "-1";
                string idS = "-1";
                string tmpContent = "";
                int _f = 0;

                for (int i = 0; i < whSearch.Length; i++)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(whSearch[i])))
                        break;
                    
                    if (whSearch[i] == '0' || whSearch[i] == '1' ||
                        whSearch[i] == '2' || whSearch[i] == '3' ||
                        whSearch[i] == '4' || whSearch[i] == '5' ||
                        whSearch[i] == '6' || whSearch[i] == '7' ||
                        whSearch[i] == '8' || whSearch[i] == '9')
                    {
                        if (_f == 0)
                        {
                            id = "";   
                        }
                        id = id + whSearch[i];
                        _f++;
                    }
                }
                
                if(id == "-1")
                    throw new Exception("Not valid id passed");

                //Search desc content

                for (int i = 0; i < fileContent.Length; i++)
                {
                    idS = "-1";
                    
                    if (fileContent[i].StartsWith("<desc"))
                    {
                        //Check id
                        int f = 0;
                        
                        for (int x = 7; x < fileContent[i].Length; x++)
                        {
                            if (fileContent[i][x] == ' ')
                                break;
                            
                            if (fileContent[i][x] == '0' || fileContent[i][x] == '1' ||
                                fileContent[i][x] == '2' || fileContent[i][x] == '3' ||
                                fileContent[i][x] == '4' || fileContent[i][x] == '5' ||
                                fileContent[i][x] == '6' || fileContent[i][x] == '7' ||
                                fileContent[i][x] == '8' || fileContent[i][x] == '9')
                            {
                                if (f == 0)
                                {
                                    idS = "";   
                                }
                                idS = idS + fileContent[i][x];
                                f++;
                            }
                        }

                        if (id == idS)
                        {
                            //Content!
                            int count = 10 + idS.Length;
                            string matched = "";

                            if (count < fileContent[i].Length)
                            {
                                for (int x = 0; x < 8; x++)
                                {
                                    matched += fileContent[i][count];

                                    count++;
                                }

                                if (matched == "content=")
                                {
                                    if (count < fileContent[i].Length)
                                    {
                                        for (; true;)
                                        {
                                            if (fileContent[i][count] == '>')
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                tmpContent += fileContent[i][count];
                                            }

                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                arrToWrite[0] = tmpContent;
            }
            else
            {
                throw  new Exception(whSearch + " not valid");
            }
            
            Console.WriteLine("OK!");
            
            Console.Write("[WDB ENGINE]: Starting writing in output file...");
            
            File.WriteAllLines(Dipendences.outputPath, arrToWrite);
            
            Console.WriteLine("OK!");
            
            Console.Write("[WDB ENGINE]: Executing Finished");
        }
    }
}