using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Echo_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--=== Echo Client ===--");
            Console.Write("Adresse IP du serveur :");
            String ip = Console.ReadLine();
            // On crée un EndPoint
            IPAddress ipLocal = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipLocal, 7);
            // Puis un client
            TcpClient client = new TcpClient();
            // Qui se connecte au EndPoint
            client.Connect(ep);
            NetworkStream flux = client.GetStream();
            do
            {
                // Que doit on envoyer ?
                Console.Write("Message : ");
                string message = Console.ReadLine();
                if ( string.IsNullOrEmpty( message ))
                {
                    break;
                }
                // On transforme le texte en octets
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(message);
                // On envoie
                flux.Write(buffer, 0, buffer.Length );
                // On attend le retour
                flux.Read(buffer, 0, buffer.Length);
                String recu = ASCIIEncoding.ASCII.GetString(buffer);
                if ( String.Compare( message, recu ) == 0 )
                {
                    Console.WriteLine("Retour OK !");
                }
                else
                {
                    Console.WriteLine("Retour en Erreur !");
                }

            } while (true);
            //
            Console.WriteLine("Appuyez sur Entree pour terminer...");
            Console.ReadLine();
        }

    }
}
