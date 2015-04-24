using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace client
{
   public class Communication
    {

       String host = "localhost";
       Int32 inPort = 7000;
       Int32 outPort = 6000;

        public void sendMsg(String message) {

            try {
                
                TcpClient outMsg = new TcpClient(host, outPort);

                Byte[] sentData = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream streamOut = outMsg.GetStream();
                streamOut.Write(sentData, 0, sentData.Length);

                Console.WriteLine("Sent: {0}", message);

                streamOut.Close();
                outMsg.Close();




            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
 
                
        }

        public void receiveMsg() {

             TcpListener listner=null;

            try {

               listner=new TcpListener(IPAddress.Parse("127.0.0.1"),inPort);

               listner.Start();
               Byte[] recData = new Byte[256];
               String response;

               while (true) {
                   TcpClient inMsg = listner.AcceptTcpClient();
                   NetworkStream inStream=inMsg.GetStream();
                   response=String.Empty;

                   int i=0;


                   while ((i = inStream.Read(recData, 0, recData.Length)) != 0) {
                       response = System.Text.Encoding.ASCII.GetString(recData, 0, i);
                       Console.WriteLine("Received: {0}", response);
                   
                   }

                   inStream.Close();
                   inMsg.Close();

               }
               
               
               
               
               

              

            }
           
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                listner.Stop();
            }
 
        
        
        }

    }
}
