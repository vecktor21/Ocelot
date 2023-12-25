using Grpc.Core;
using Messenger;

namespace Grpc.Web.Server.Services
{
    public class MessengerService : Messenger.Messenger.MessengerBase
    {
        private List<string> content = new List<string> { "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй","заебал че молчишь"};
        private readonly ILogger<MessengerService> logger;

        public MessengerService(ILogger<MessengerService> logger)
        {
            this.logger = logger;
        }

        public override async Task ServerStream(EmptyRequest request, IServerStreamWriter<StreamResponse> responseStream, ServerCallContext context)
        {
            foreach (var item in content)
            {
                await responseStream.WriteAsync(new StreamResponse { Content = item });
                Task.Delay(1000).Wait();
            }
        }

        public override async Task<StreamResponse> ClientStream(IAsyncStreamReader<StreamRequest> requestStream, ServerCallContext context)
        {
            List<string> res = new();
            while(await requestStream.MoveNext())
            {
                res.Add(requestStream.Current.Content);
                logger.LogInformation(requestStream.Current.Content);
            }
            return new StreamResponse { Content = "END" };
        }

    }
}
