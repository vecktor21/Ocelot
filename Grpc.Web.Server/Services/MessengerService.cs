using Grpc.Core;
using Messenger;

namespace Grpc.Web.Server.Services
{
    public class MessengerService : Messenger.Messenger.MessengerBase
    {
        private List<string> content = new List<string> { "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй", "хуй","заебал че молчишь"};
        public MessengerService()
        {
            
        }

        public override async Task ServerStream(EmptyRequest request, IServerStreamWriter<StreamResponse> responseStream, ServerCallContext context)
        {
            foreach (var item in content)
            {
                await responseStream.WriteAsync(new StreamResponse { Content = item });
                Task.Delay(1000).Wait();
            }
        }
    }
}
