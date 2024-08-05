using System.Resources;
using Hammer.Agent.Controller;
using Hammer.Agent.Process;
using Hammer.Agent.Resource;
using Microsoft.Extensions.Logging;
using ResourceManager = Hammer.Agent.Resource.ResourceManager;

namespace Hammer.Agent.McServer;

public class McServerController(ResourceManager rmg, ILogger<McServerController> logger) : BasicController<McServerResource>(rmg)
{
    protected override void Recover(ResourceEvent<McServerResource> resourceEvent)
    {
        logger.LogError("Found a McServerResource in the store.");
        CheckResource(resourceEvent.Resource, resourceEvent.ResourceManager);
    }

    protected override void Added(ResourceEvent<McServerResource> resourceEvent)
    {
        logger.LogWarning("Added a McServerResource to the store.");
    }

    protected override void Changed(ResourceEvent<McServerResource> resourceEvent)
    {
        logger.LogWarning("Changed a McServerResource in the store.");
    }

    protected override void Removed(ResourceEvent<McServerResource> resourceEvent)
    {
        logger.LogWarning("Removed a McServerResource from the store.");
    }
    
    private void CheckResource(McServerResource resource, ResourceManager rmg)
    {
        var processes = rmg.Resources
            .FirstOrDefault(r => r is ProcessResource && r.ParentID == resource.ID);

        if (processes is null)
        {
            
        }

    }
    
}