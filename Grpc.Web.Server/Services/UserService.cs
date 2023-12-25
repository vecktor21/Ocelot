using Grpc.Core;
using Grpc.Web.Server;
using UserRpc;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace Grpc.Web.Server.Services
{
    public class UserService : UserRpc.UserRpc.UserRpcBase
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper mapper;
        private List<User> users = new List<User>
        {
            new User { Id = 1, Age=20,Name="Denis", RegisterDate = Timestamp.FromDateTime( DateTime.UtcNow.AddDays(-3))},
            new User { Id = 2, Age=21,Name="Kirill", RegisterDate = Timestamp.FromDateTime( DateTime.UtcNow.AddDays(-1))},
            new User { Id = 3, Age=22,Name="Artem", RegisterDate = Timestamp.FromDateTime( DateTime.UtcNow)},
        };
        public UserService(ILogger<UserService> logger, IMapper mapper)
        {
            _logger = logger;
            this.mapper = mapper;
        }

        public override Task<UserRpc.User> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {

            /*return Task.FromResult(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserRpc.User>();
                })
                .CreateMapper()*/
            return Task.FromResult(mapper
                .Map<UserRpc.User>(users.FirstOrDefault(x => x.Id == request.Id)));
        }
    }

}