using System.Diagnostics;
using System.Text.Json.Serialization;
using ResourceTypeID = string;

namespace Thinking;

// Singleton
// Async recurring task to check the process health
public class ProcessController : Controller<ProcessResource>
{
    private Dictionary<string, ProcessResource> _processes;

    public ProcessController(ProcessStore store, IHostProcessExecuter executer)
    {
        // Add the processes from the store to the dict
        // Check if the processes are still alive
        //   Check is the Host has restarted
    }

    public override void Recover(ProcessResource resource, ResourceManager rmgr)
    {
        throw new NotImplementedException();
    }

    public override void Added(ProcessResource resource, ResourceManager rmgr)
    {
        // Start the new process
        throw new NotImplementedException();
    }

    public override void Changed(ProcessResource resource, ResourceManager rmgr)
    {
        // Restart the process with the new settings
        throw new NotImplementedException();
    }

    public override void Removed(ProcessResource resource, ResourceManager rmgr)
    {
        // Stop the process
        throw new NotImplementedException();
    }
}

public class ProcessResource : Resource
{
    public override ResourceTypeID Type { get; }
    public override ProcessDescriptor _descriptor { get; }
    public override ProcessState _state { get; }

    public string HostID;
    
    public override bool IsHealthy { get; }
}

public class ProcessDescriptor : ResourceDescriptor
{
    public string executable;
    public string arguments;
    public string workingDirectory;
}

public class ProcessState: ResourceState
{
    public int PID;
    public string Status;
    public override bool IsRunning { get; }
    
    [JsonIgnore] protected Process _process;
}

public interface IHostProcessExecutor
{
    Process Start(ProcessStartInfo info);
    void Stop(Process proc);

    Process FindById(int pid)
    {
        return Process.GetProcessById(pid);
    }
}