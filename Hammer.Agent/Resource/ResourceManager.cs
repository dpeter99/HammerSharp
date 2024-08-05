using System.Reactive.Subjects;
using Microsoft.Extensions.Hosting;

namespace Hammer.Agent.Resource;

/// <summary>
/// 
/// </summary>
public class ResourceManager
{
    private readonly IResourceStore _store;

    public Subject<ResourceEvent> OnRecover = new();
    public Subject<ResourceEvent> OnAdd = new();
    public Subject<ResourceEvent> OnChange = new();
    public Subject<ResourceEvent> OnDelete = new();

    public IQueryable<Resource> Resources => _store.Resources.AsQueryable();
    
    public ResourceManager(IResourceStore store)
    {
        _store = store;
        _store.ChangeEvent += ResourceChanged;
    }

    private void ResourceChanged(Resource resource)
    {
        
    }

    public async Task StartUp()
    {
        var res = _store.Resources;

        foreach (var resource in res)
        {
            OnRecover.OnNext(new ResourceEvent(resource, this));
        }
    }

    public Task Add(Resource resource)
    {
        _store.Add(resource);
        OnAdd.OnNext(new ResourceEvent(resource, this));
        return Task.CompletedTask;
    }
}

public class ResourceManagerStartup : IHostedService
{
    private readonly ResourceManager _resourceManager;
    private readonly IHostApplicationLifetime _appLifetime;

    public ResourceManagerStartup(ResourceManager resourceManager, IHostApplicationLifetime appLifetime)
    {
        _resourceManager = resourceManager;
        _appLifetime = appLifetime;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _resourceManager.StartUp();
        await Task.Delay(10000);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}