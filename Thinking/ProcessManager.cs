using System.Diagnostics;
using System.Text.Json.Serialization;
using ResourceTypeID = string;

namespace Thinking;

// Singleton
// Async recurring task to check the process health
public class ProcessManager
{
    private Dictionary<string, ManagedProcess> _processes;

    public ProcessManager(ProcessStore store, IHostProcessExecuter executer)
    {
        // Add the processes from the store to the dict
        // Check if the processes are still alive
        //   Check is the Host has restarted
    }

    public async Task<ManagedProcess> Start(ProcessStartInfo startInfo, ResourceTypeID type, string id)
    {
        throw new NotImplementedException();
    }

    public async Task Stop(ManagedProcess process)
    {
        throw new NotImplementedException();
    }

}

public interface IHostProcessExecuter
{
    Process Start(ProcessStartInfo info);
    void Stop(Process proc);

    Process FindById(int pid)
    {
        return Process.GetProcessById(pid);
    }
}

public class ManagedProcess
{
    [JsonInclude] private int PID;
    
    // Connected resource data
    [JsonInclude] private ResourceTypeID Type;
    [JsonInclude] private string ID;

    protected Process _process;
    
    public bool IsRunning { get; }
}