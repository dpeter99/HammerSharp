using Grpc.Core;
using Hammer.Agent.Resource;

namespace Hammer.Agent.grpc.Services;

public class ServerListerService(ResourceManager rm) : ServerLister.ServerListerBase
{


    public override async Task GetServers(ListArgs request, IServerStreamWriter<Server> responseStream, ServerCallContext context)
    {
        foreach (var resource in rm.Resources)
        {
            await responseStream.WriteAsync(new Server()
            {
                ServerName = resource.Name,
                Type = resource.Type.Name,
                Running = resource.State.IsRunning
            });
        }
    }
}