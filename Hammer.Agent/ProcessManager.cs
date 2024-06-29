using System.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace Hammer.Agent;

public class ProcessManager
{
    private List<ManagedProcess> _processes = new();

    public ManagedProcess StartProcess(ProcessStartInfo info)
    {
        info.UseShellExecute = true;
        Process p = new Process();
        p.StartInfo = info;
        p.Start();

        var managedProcess = new ManagedProcess(p);
        _processes.Add(managedProcess);

        return managedProcess;
    }
}

public class ManagedProcess
{
    private Process _proc;

    public ManagedProcess(Process proc)
    {
        _proc = proc;
    }
    
    
}