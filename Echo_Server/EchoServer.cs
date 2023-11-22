using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Echo_Server
{
    public class EchoServer
    {
        private IPEndPoint ep;
        private TcpListener listener;
        private Thread th;
        private List<EchoWork> clients;

        public EchoServer(IPAddress ipLocal)
        {
            // Echo est sur le port 7
            ep = new IPEndPoint(ipLocal, 7);
            listener = new TcpListener(ep);
            //
            clients = new List<EchoWork>();
        }

        public void Start()
        {
            th = new Thread(this.threadStart);
            th.Start();
        }

        void threadStart()
        {
            listener.Start();
            do
            {
                try
                {
                    // l'appel à AcceptTcpClient est bloquant
                    TcpClient client = listener.AcceptTcpClient();
                    // On met ce client dans un Thread qui va renvoyer tout ce qu'il reçoit jusqu'à ce que le client se ferme
                    EchoWork work = new EchoWork(client);
                    clients.Add(work);
                    work.Start();
                    // et on retourne attendre une connection
                }
                catch
                {
                    // Quelle que soit la raison...On sort du While (et donc du Thread)
                    break;
                }
            } while (true);
        }

        public void Stop()
        {
            // On stop le server
            this.listener.Stop();
            // et tous les clients connectés
            foreach (var client in clients)
            {
                client.Stop();
            }
        }
    }
}
