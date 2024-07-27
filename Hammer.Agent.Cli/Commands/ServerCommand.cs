using System.CommandLine;
using System.Net.Sockets;
using System.Threading.Channels;
using Grpc.Net.Client;
using Hammer.Agent.Cli.IPC.UDS;

namespace Hammer.Agent.Cli.Commands;

public class ServerCommand : Command
{
    private GrpcChannel _channel;

    public ServerCommand() : base("server", "Base command to handle everything to do with servers")
    {
        var listCommand = new Command("list", "Lists all currently configured servers");
        listCommand.SetHandler(ListCommandHandler);
        AddCommand(listCommand);
        var udsEndPoint = new UnixDomainSocketEndPoint(Program.SocketPath);
        var connectionFactory = new UnixDomainSocketsConnectionFactory(udsEndPoint);
        var socketsHttpHandler = new SocketsHttpHandler
        {
            ConnectCallback = connectionFactory.ConnectAsync
        };
        
        _channel = GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
        {
            HttpHandler = socketsHttpHandler
        });
    }

    private void ListCommandHandler()
    {
        var client = new ServerLister.ServerListerClient(_channel);
        client.GetServers(new ListArgs());
    }
}