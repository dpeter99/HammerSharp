using System.Net.Sockets;
using Grpc.Net.Client;
using Hammer.Agent.Cli.IPC.UDS;

namespace Hammer.Agent.Cli.Services;

public interface IGrpcChannelService
{
    public GrpcChannel GetChannel();
}

public class GrpcChannelService(string socketPath) : IGrpcChannelService
{
    private GrpcChannel? _channel;

    public GrpcChannel GetChannel()
    {
        if (_channel != null) return _channel;
        
        var udsEndPoint = new UnixDomainSocketEndPoint(socketPath);
        var connectionFactory = new UnixDomainSocketsConnectionFactory(udsEndPoint);
        var socketsHttpHandler = new SocketsHttpHandler
        {
            ConnectCallback = connectionFactory.ConnectAsync
        };
        
        _channel = GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
        {
            HttpHandler = socketsHttpHandler
        });
        return _channel;
    }
}