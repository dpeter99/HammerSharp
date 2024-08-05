using System.Resources;

namespace Hammer.Agent.Controller;

using Hammer.Agent.Resource;

public interface IController
{
}

public abstract class BasicController<T> : IController where T : Resource
{
    protected BasicController(ResourceManager rmg)
    {
        rmg.Listen<T>(Recover, Added, Changed, Removed);
    }


    protected abstract void Recover(ResourceEvent<T> resourceEvent);
    protected abstract void Added(ResourceEvent<T> resourceEvent);
    protected abstract void Changed(ResourceEvent<T> resourceEvent);
    protected abstract void Removed(ResourceEvent<T> resourceEvent);
}