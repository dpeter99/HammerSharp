using System.Diagnostics;
using System.Reactive.Linq;

namespace Hammer.Agent.Resource;



public class ResourceObserver<TResource> : IDisposable where TResource : Resource
{
    // private Func<TResource, bool> _filter;

    private readonly IDisposable _onRecoverSubscription;
    private readonly IDisposable _onAddSubscription;
    private readonly IDisposable _onChangeSubscription;
    private readonly IDisposable _onDeleteSubscription;
    
    public ResourceObserver(
        ResourceManager rmg,
        Action<ResourceEvent<TResource>> onRecoverEvent,
        Action<ResourceEvent<TResource>> onAddEvent,
        Action<ResourceEvent<TResource>> onChangeEvent,
        Action<ResourceEvent<TResource>> onDeleteEvent)
    {
        _onRecoverSubscription = rmg.OnRecover
            .Where(resource => resource.Resource is TResource)
            .Select(@event => @event.As<TResource>())
            .Subscribe(onRecoverEvent);
        
        _onAddSubscription = rmg.OnAdd
            .Where(resource => resource is TResource)
            .Select(@event => @event.As<TResource>())
            .Subscribe(onAddEvent);

        _onChangeSubscription = rmg.OnChange
            .Where(resource => resource is TResource)
            .Select(@event => @event.As<TResource>())
            .Subscribe(onChangeEvent);
        
        _onDeleteSubscription = rmg.OnDelete
            .Where(resource => resource is TResource)
            .Select(@event => @event.As<TResource>())
            .Subscribe(onDeleteEvent);
    }

    public void Dispose()
    {
        _onRecoverSubscription.Dispose();
    }
}

public static class ResourceObserverExtension
{
    public static ResourceObserver<TResource> Listen<TResource>(
        this ResourceManager rmg,
        Action<ResourceEvent<TResource>> onRecoverEvent,
        Action<ResourceEvent<TResource>> onAddEvent,
        Action<ResourceEvent<TResource>> onChangeEvent,
        Action<ResourceEvent<TResource>> onDeleteEvent) where TResource : Resource
    {
        return new ResourceObserver<TResource>(rmg, onRecoverEvent, onAddEvent, onChangeEvent, onDeleteEvent);
    }
}