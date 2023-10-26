using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketCliente
{
    class Program
    {
        static void Main(string[] args)
        {
            //Configuracion para conectarse con el servidor 
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11200);


            try
            {
                //Cear socket para enviar datos 
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Socket le indicamos conectarse conel servodr 
                sender.Connect(remoteEP);

                //Mensaje de configuracion de conexion
                Console.WriteLine("Conectado conel servidor");

                //Pedimos al usuario que ingrese un texto para enviar al servidor 
                Console.WriteLine("Ingrese un texto para enviar");
                string texto = Console.ReadLine();

                //Convertir el texto en un arreglo de bytes
                byte[] msg = Encoding.ASCII.GetBytes(texto + "<EOF>");

                //Enviar al servidor el mensaje 
                int byteSent = sender.Send(msg);

                //Cerrar la conexion con el servidor
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
