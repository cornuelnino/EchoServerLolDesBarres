using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Echo_Server
{
    class EchoWork
    {
        private TcpClient client;
        private NetworkStream flux;
        private Thread th;

        public EchoWork( TcpClient clt)
        {
            client = clt;
            flux = client.GetStream();
        }

        public void Start()
        {
            th = new Thread(this.threadStart);
            th.Start();
        }

        void threadStart()
        {
            byte[] buffer = new byte[2048];
            do
            {
                try
                {
                    int read = flux.Read(buffer, 0, 2048);
                    flux.Write(buffer, 0, read);
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
            // On peut fermer la connection en fermant le TcpClient
            client.Close();
        }


    }
}
