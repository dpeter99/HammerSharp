using Hammer.Agent.GameServers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Hammer.Agent;

public class Agent : IHostedService
{
    private readonly ProcessManager _prcMgr;
    private MinecraftServer server;

    public Agent(IOptions<AgentConfiguration> agentConfig)
    {
        
    }

    public Task StartAsync(CancellationToken cancellationToken)
    { 
        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        
    }
}

public class GameConfiguration
{
    public required string GameType { get; set; }
    public required string Name { get; set; }
    
    public required string ServerJar { get; set; }
}

public class AgentConfiguration
{
    public required string WorkingDir { get; set; }
}