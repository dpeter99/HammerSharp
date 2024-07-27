using System.Diagnostics;
using Hammer.Agent.Controller;
using Hammer.Agent.HostInterface;
using Hammer.Agent.Resource;
using Microsoft.Extensions.Logging;

namespace Hammer.Agent.Process;

public class ProcessController(ResourceManager rmg, IHostProcessExecutor executor, ILogger<ProcessController> logger) : BasicController<ProcessResource>(rmg)
{
    
    
    protected override void Recover(ResourceEvent<ProcessResource> resourceEvent)
    {
        logger.LogInformation($"{nameof(Process)}: {resourceEvent.Resource.Name} needs to be recovered");
        if(resourceEvent.Resource.State.PID is null)
        {
            switch (resourceEvent.Resource.Descriptor.Target)
            {
                case ProcessTarget.Stopped:
                    logger.LogInformation($"Process: \"{resourceEvent.Resource.Name}\" is stopped, no need to recover");
                    break;
                default:
                    StartProcess(resourceEvent);
                    break;
            }
        }
        else
        {
            var process = executor.FindById(resourceEvent.Resource.State.PID.Value);
            if (process is null)
            {
                logger.LogInformation($"Process: \"{resourceEvent.Resource.Name}\" is not running, starting process...");
                StartProcess(resourceEvent);
            }
            else
            {
                logger.LogInformation($"Process: \"{resourceEvent.Resource.Name}\" is running with PID: {process.Id}");
                resourceEvent.Resource.State.Status = ProcessStatus.Running;
            }
        }
        
    }

    private void StartProcess(ResourceEvent<ProcessResource> resourceEvent)
    {
        logger.LogInformation($"Process: \"{resourceEvent.Resource.Name}\" has no PID, starting process...");
        
        var process = executor.Start(GetProcessStartInfo(resourceEvent.Resource));
        if (process == null)
        {
            logger.LogError($"Failed to start process: {resourceEvent.Resource.Name}");
            resourceEvent.Resource.State.Status = ProcessStatus.Failed;
            return;
        }

        logger.LogInformation($"Process: {resourceEvent.Resource.Name} started with PID: {process.Id}");
        resourceEvent.Resource.State.PID = process.Id;
        resourceEvent.Resource.State.Status = ProcessStatus.Running;
    }

    private static ProcessStartInfo GetProcessStartInfo(ProcessResource resource)
    {
        return new ProcessStartInfo(
            resource.Descriptor.Executable,
            resource.Descriptor.Arguments)
        {
            WorkingDirectory = resource.Descriptor.WorkingDirectory
        };
    }

    protected override void Added(ResourceEvent<ProcessResource> resourceEvent)
    {
        logger.LogInformation("There is a new process resource added");
    }

    protected override void Changed(ResourceEvent<ProcessResource> resourceEvent)
    {
        throw new NotImplementedException();
    }

    protected override void Removed(ResourceEvent<ProcessResource> resourceEvent)
    {
        throw new NotImplementedException();
    }
}