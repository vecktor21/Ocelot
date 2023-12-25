using Grpc.Cli.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpc.Cli.Client
{
    public class Server
    {
        private MessengerService messenger;
        public Server(string conn)
        {
            messenger = new(conn);
        }
        public void Run()
        {
            Task.Run(async () => { await messenger.StartConnection(); });

            while (true)
            {

            }
        }
    }
}
