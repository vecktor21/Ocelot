using AutoMapper;
using UserRpc;

namespace Grpc.Web.Server.Maps
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<Grpc.Web.Server.models.User, UserRpc.User>();
        }
    }
}
