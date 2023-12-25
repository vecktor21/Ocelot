using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserRpc;

namespace Grpc.Web.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRpc.UserRpc.UserRpcClient client;
        public UserController(IOptions<Grpc.Web.Client.Options> options)
        {
            GrpcChannel chanel = GrpcChannel.ForAddress(options.Value.GrpcServerAddress);
            client = new UserRpc.UserRpc.UserRpcClient(chanel);
            
        }
        [HttpGet("{userId}")]
        public async Task<User> GetUser(int userId)
        {
            return await client.GetUserByIdAsync(new GetUserByIdRequest { Id = userId });
        }
    }
}
