using System.Text.Json.Serialization;
using Hammer.Agent.Resource;

namespace Hammer.Agent.Process;

public class ProcessResource(ProcessDescriptor processDescriptor, ProcessState processState) : Resource.Resource
{
    public override ResourceTypeId Type => new("core:process");
    public override ProcessDescriptor Descriptor { get; } = processDescriptor;
    public override ProcessState State { get; } = processState;

    public string HostID;
    
    public override bool IsHealthy { get; }
}

public class ProcessDescriptor(string executable, string[] arguments, string workingDirectory)
    : ResourceDescriptor
{
    public string Executable = executable;
    public string[] Arguments = arguments;
    public string WorkingDirectory = workingDirectory;
    
    public ProcessTarget Target = ProcessTarget.Running;
}

public class ProcessState: ResourceState
{
    public int? PID;
    public ProcessStatus Status = ProcessStatus.Unknown;
    public override bool IsRunning => Status == ProcessStatus.Running;
    
    [JsonIgnore] protected System.Diagnostics.Process _process;
}

public enum ProcessTarget
{
    Running,
    Stopped,
}

public enum ProcessStatus
{
    Running,
    Stopped,
    Failed,
    Unknown
}