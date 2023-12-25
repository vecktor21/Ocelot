using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRpc;

namespace Grpc.Web.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRpc.UserRpc.UserRpcClient client;
        public UserController()
        {
            GrpcChannel chanel = GrpcChannel.ForAddress("http://grpc.web.server:80");
            client = new UserRpc.UserRpc.UserRpcClient(chanel);
            
        }
        [HttpGet("{userId}")]
        public async Task<User> GetUser(int userId)
        {
            return await client.GetUserByIdAsync(new GetUserByIdRequest { Id = userId });
        }
    }
}
