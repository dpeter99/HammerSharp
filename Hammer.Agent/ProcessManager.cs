using System.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace Hammer.Agent;

/// <summary>
/// Responsible for knowing what processes are running
/// And starting/stopping them
/// These are not necessarily game server processes.
/// Later these could be other tasks like making backups or similar 
/// </summary>
public class ProcessManager
{
    // List of processes that are managed by this Agent
    private List<ManagedProcess> _processes = new();

    public ProcessManager(
        // JsonStore store for running processes
        )
    {
        // Read in the old known running servers from the store
        
        // Check what servers are still running
        // How to know if the same PID hasn't been reused?
        
        // Add still alive servers to the list
    }

    public ManagedProcess Start()
    {
        throw new NotImplementedException();
    }
}

public class ManagedProcess
{
    public int PID;
    public bool IsRunning { get; }
    public DirectoryInfo WorkingDir;
    public string[] arguments;
    
}