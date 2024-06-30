namespace Hammer.Agent;

/// <summary>
/// Gets the current state and the desired state
/// Tries to match them up and update the current state to match the one expected
/// </summary>
public class Reconciler
{
    public Reconciler()
    {
        
    }


    public void Reconcile(Server s)
    {
        
    }
}

public class Server
{
    public ServerState Sate;
    public ServerDescriptor Descriptor;
    
}

public abstract class ServerDescriptor
{
    public string Name;
    public string Type; // Maybe an enum? or some modular registry thing
    public string Folder;
    public int[] Ports;
}


/// <summary>
/// The mostly read only state of the server
/// And functions to update/change the state
/// </summary>
public abstract class ServerState
{
    /// <summary>
    /// The process that is backing this game server
    /// If null there is no process associated with it
    /// </summary>
    public ManagedProcess? Process;

    /// <summary>
    /// True if the server process is running
    /// </summary>
    public bool IsRunning => Process?.IsRunning ?? false;
    
    /// <summary>
    /// Checks if the server is fully up and running
    /// This needs to be implemented per game type
    /// </summary>
    public bool IsHealthy { get; }
    
    /// <summary>
    /// The network ports that are currently in use by the server
    /// </summary>
    public int[] Ports { get; }
}