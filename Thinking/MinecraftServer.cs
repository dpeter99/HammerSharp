using ResourceTypeID = string;

namespace Thinking;
class McServer : Resource
{
    public override ResourceTypeID Type => "core:mc_server";
    
    public override McServerDescriptor _descriptor { get; }
    public override McServerState _state { get; }

    public override bool IsHealthy => _descriptor.IsEnabled ? _state.IsRunning : !_state.IsRunning;
}

[Serializable]
class McServerDescriptor : ResourceDescriptor
{
    public bool IsEnabled;
    
    private string Version;
    private DirectoryInfo serverFolder;
    private FileInfo jarLocation;
}

class McServerState : ResourceState
{
    private ManagedProcess Process;
    public override bool IsRunning => Process.IsRunning;
}

class MCServerController : Controller<McServer>
{
    private readonly ProcessManager _processManager;

    public MCServerController(ProcessManager processManager)
    {
        _processManager = processManager;
    }

    public override void Recover(McServer resource, ResourceManager rmgr)
    {
        // Ask ProcessManager for any related processes
        // If there is we add it to the state
        throw new NotImplementedException();
    }

    public override void Added(McServer resource, ResourceManager rmgr)
    {
        throw new System.NotImplementedException();
    }

    public override void Changed(McServer resource, ResourceManager rmgr)
    {
        throw new System.NotImplementedException();
    }

    public override void Removed(McServer resource, ResourceManager rmgr)
    {
        throw new System.NotImplementedException();
    }
}