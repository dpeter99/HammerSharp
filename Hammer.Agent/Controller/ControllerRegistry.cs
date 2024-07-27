using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hammer.Agent.Controller;

using Hammer.Agent.Resource;

public class ControllerRegistry : IHostedService
{
    public ControllerRegistry(IEnumerable<IController> controllers, ILogger<ControllerRegistry> logger)
    {
        foreach (var controller in controllers)
        {
            logger.LogInformation($"Found a controller: {controller.GetType().Name}");
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}