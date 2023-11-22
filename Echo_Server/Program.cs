﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Echo_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> ips = Program.GetLocalIPAddresses();
            string localIP = null;
            if ( ips.Count > 1 )
            {
                Console.WriteLine("--=== Choix adresse IP ===--");
                for ( int i = 0; i< ips.Count; i++)
                {
                    Console.WriteLine(i.ToString() + " : " + ips[i]);
                }
                Console.Write("Choix : ");
                string choix = Console.ReadLine();
                localIP = ips[int.Parse(choix)];
            }
            else
            {
                localIP = ips[0];
            }
            // Echo peut fonctionner en TCP, sur le Port 7
            IPAddress ipLocal = IPAddress.Parse(localIP);
            EchoServer server = new EchoServer(ipLocal);
            Console.WriteLine("--=== Serveur Echo ===--");
            Console.WriteLine( "Adresse IP : " + localIP);
            Console.WriteLine("Serveur démarré...");
            server.Start();
            //
            Console.WriteLine("Appuyez sur Entree pour terminer...");
            Console.ReadLine();
            server.Stop();
        }

        public static List<string> GetLocalIPAddresses()
        {
            List<String> ips = new List<string>();

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ips.Add(ip.ToString());
                }
            }
            if ( ips.Count > 0 )
                return ips;
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
