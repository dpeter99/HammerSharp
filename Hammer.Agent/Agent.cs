using Hammer.Agent.GameServers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Hammer.Agent;

public class Agent : IHostedService
{
    private readonly ProcessManager _prcMgr;
    private MinecraftServer server;

    public Agent(
        IOptions<GameConfiguration> gameConfig,
        IOptions<AgentConfiguration> agentConfig,
        ProcessManager prcMgr)
    {
        _prcMgr = prcMgr;
        if (gameConfig.Value.GameType == "Minecraft")
        {
            var workingDir = new DirectoryInfo(agentConfig.Value.WorkingDir);
            server = new MinecraftServer(
                gameConfig.Value.Name,
                workingDir.CreateSubdirectory(gameConfig.Value.Name),
                new FileInfo(gameConfig.Value.ServerJar)
            );
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    { 
        server.StartServer();
        return Task.Delay(TimeSpan.FromSeconds(30));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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