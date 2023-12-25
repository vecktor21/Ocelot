namespace Grpc.Cli.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(args[0]);

            server.Run();
        }
    }
}