using messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using messenger;
using Grpc.Net.Client;
using System.Collections.Concurrent;

namespace Grpc.Cli.Client.Services
{
    internal class MessengerService 
    {
        private readonly Messenger.MessengerClient client;
        private ConcurrentQueue<messenger.StreamRequest> messageQueue = new();
        private CancellationTokenSource tokenSource;
        public MessengerService(string conn)
        {

            var chanel = GrpcChannel.ForAddress(!String.IsNullOrEmpty(conn) ? conn : "http://grpc.web.server:80");
            client = new Messenger.MessengerClient(chanel);
            tokenSource = new CancellationTokenSource();

        }

        public async Task StartConnection()
        {
            Task.Run(async () => { await StartSending(tokenSource.Token.Register(CancelCallback).Token); });
            Task.Run(async () => { await StartReading(tokenSource.Token.Register(CancelCallback).Token); });
        }

        private async Task StartSending(CancellationToken token)
        {
            var chanel = client.ClientStream();
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                if(messageQueue.TryDequeue(out var message))
                {
                    try
                    {
                        Console.WriteLine($"[{DateTime.Now}] Writing to server");
                        await chanel.RequestStream.WriteAsync(message);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"[{DateTime.Now}] Error while sending message");
                    }
                }
            }
            await chanel.RequestStream.CompleteAsync();
            var res = await chanel.ResponseAsync;
            Console.WriteLine(res.Content);
        }

        private async Task StartReading(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                Console.WriteLine($"[{DateTime.Now}] Enter text:");
                var value = Console.ReadLine();
                if (value == "\\!")
                {
                    tokenSource.Cancel();
                    Console.WriteLine($"[{DateTime.Now}] Stopped reading");
                    break;
                }
                messageQueue.Enqueue(new StreamRequest
                {
                    Content = value
                });
            }
        }

        private void CancelCallback()
        {
            Console.WriteLine($"[{DateTime.Now}] TokenCancelled");
        }
    }
}
