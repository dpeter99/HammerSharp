namespace Hammer.Agent.Resource;

/// <summary>
/// 
/// </summary>
public interface IResourceStore
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<Resource> Resources { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    Task<bool> Add(Resource resource);

    abstract ResourceChangeEvent ChangeEvent { get; set; }
}

public delegate void ResourceChangeEvent(Resource resource);