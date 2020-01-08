using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

namespace wdb.Server
{
    public class Main
    {
        public static string data = null;  

        public static void Start()
        {
            //Buffer per i dati
            byte[] bytes = new Byte[1024];
            
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
            IPAddress ipAddress = ipHostInfo.AddressList[0];  
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11100);  
            
            Console.Write("[WDB SERVER]: Starting socket.......");
            Socket listener = new Socket(ipAddress.AddressFamily,  
                SocketType.Stream, ProtocolType.Tcp );
            Console.WriteLine("OK!");

            listener.Bind(localEndPoint);  
            listener.Listen(10);  
  
            // Start listening for connections.  
            while (true)
            {
                Console.Write("[WDB SERVER]: Waiting for a connection....");

                Socket handler = listener.Accept();
                data = null;
                
                Console.WriteLine("OK!");

                while (true)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    
                    if (data.IndexOf("<EOF>") > -1) //Stop Listening
                    {
                        break;
                    }

                }
                
                Console.WriteLine("[WDB SERVER]: Recived: " + data);
                
                if (data == "ex")
                {
                    Console.WriteLine("[WDB ENGINE]: Stopping WDB Server");

                    break;
                } else if (data == "wd italiano")
                {
                    //Restituisci tutte le parole italiane
                    Console.WriteLine("[WDB SERVER]: Sending Italian wd");
                    
                } else if (data == "wd inglese")
                {
                    //Restituisci tutte le parole inglesi
                    Console.WriteLine("[WDB SERVER]: Sending English wd");
                    
                } else if (data == "wd spagnolo")
                {
                    //Restituisci tutte le parole italiane
                    Console.WriteLine("[WDB SERVER]: Sending Spanish wd");
                    
                }
                else
                {
                    if (data.StartsWith("desc italiano"))
                    {
                        //Ritorna descrizione parola iesima in italiano
                        Console.WriteLine("[WDB SERVER]: Sending Italian desc");
                        
                    } else if (data.StartsWith("desc inglese"))
                    {
                        //Ritorna descrizione parola iesima in inglese
                        Console.WriteLine("[WDB SERVER]: Sending English desc");
                        
                    } else if (data.StartsWith("desc spagnolo"))
                    {
                        //Ritorna descrizione parola iesima in spagnolo
                        Console.WriteLine("[WDB SERVER]: Sending Spanish desc");
                        
                    }
                    else
                    {
                        //Errore di trasmissione
                        Console.ForegroundColor = ConsoleColor.Red;
                
                        Console.WriteLine();
                        
                        Console.WriteLine("Trasmission Error!");
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}