namespace Hammer.Agent.Resource;

public class ResourceEvent(Resource resource, ResourceManager resourceManager)
{
    public Resource Resource { get; set; } = resource;
    public ResourceManager ResourceManager { get; set; } = resourceManager;
    
    public ResourceEvent<T> As<T>() where T : Resource
    {
        return new ResourceEvent<T>((Resource as T)!, ResourceManager);
    }
}

public class ResourceEvent<TResource>(TResource resource, ResourceManager resourceManager)
{
    public TResource Resource { get; set; } = resource;
    public ResourceManager ResourceManager { get; set; } = resourceManager;
}