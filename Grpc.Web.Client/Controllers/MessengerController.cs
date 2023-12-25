using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Grpc.Web.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessengerController : ControllerBase
    {
        Messenger.Messenger.MessengerClient client;
        public MessengerController(IOptions<Grpc.Web.Client.Options> options)
        {
            GrpcChannel chanel = GrpcChannel.ForAddress(options.Value.GrpcServerAddress);
            client = new Messenger.Messenger.MessengerClient(chanel);

        }
        [HttpGet]
        public async Task<List<string>> GetMessages()
        {
            var result = client.ServerStream(new Messenger.EmptyRequest());

            var resultStream = result.ResponseStream;

            List<string> messages = new();
            
            while(await resultStream.MoveNext(CancellationToken.None))
            {
                messages.Add(resultStream.Current.Content);
            }
            return messages;
        }
    }
}