namespace Hammer.Agent.Resource;

// what is currently running
// In the future multiple subclasses for LongRunningTask, FireAndForgetTask etc.
public abstract class ResourceState
{
    public abstract bool IsRunning { get; }
    // public List<Warning> Warnings;
}