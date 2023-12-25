using Google.Protobuf.WellKnownTypes;

namespace Grpc.Web.Server.models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Timestamp RegisterDate { get; set; }
    }
}
