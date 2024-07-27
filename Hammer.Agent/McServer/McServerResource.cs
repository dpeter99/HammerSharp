using Hammer.Agent.Resource;

namespace Hammer.Agent.McServer;

public class McServerResource(McServerDescriptor descriptor, McServerState state): Resource.Resource
{
    public override ResourceTypeId Type => new("core:mc_server");
    public override McServerDescriptor Descriptor { get; } = descriptor;
    public override McServerState State { get; } = state;

    public override bool IsHealthy => Descriptor.IsEnabled ? State.IsRunning : !State.IsRunning;
}

[Serializable]
public class McServerDescriptor : ResourceDescriptor
{
    public bool IsEnabled;
    
    public string Version;
    public DirectoryInfo serverFolder;
    public FileInfo jarLocation;
}

public class McServerState : ResourceState
{
    
    public override bool IsRunning => false;
}