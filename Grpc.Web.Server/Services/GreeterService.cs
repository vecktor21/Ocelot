using Grpc.Core;
using Grpc.Web.Server;
using UserRpc;
using AutoMapper;

namespace Grpc.Web.Server.Services
{
    public class UserService : UserRpc.UserRpc.UserRpcBase
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper mapper;
        private List<User> users = new List<User>
        {
            new User { Id = 1, age=20,name="Denis"},
            new User { Id = 2, age=21,name="Kirill"},
            new User { Id = 3, age=22,name="Artem"},
        };
        public UserService(ILogger<UserService> logger, IMapper mapper)
        {
            _logger = logger;
            this.mapper = mapper;
        }

        public override Task<UserRpc.User> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {

            return Task.FromResult(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserRpc.User>();
                })
                .CreateMapper().Map<UserRpc.User>(users.FirstOrDefault(x => x.Id == request.Id)));
        }

        private class User
        {
            public int Id { get; set; }
            public string name { get; set; }
            public int age { get; set; }
        }
    }

    public class Map: Profile
    {
        public Map()
        {
            CreateMap<User, UserRpc.User>();
        }
    }
}