namespace Hammer.Agent.Storage.Json;

using Hammer.Agent.Resource;

public class ResourceStore : JsonStore<Resource>
{

    public ResourceStore(DirectoryInfo folder, bool singleFile) : base(folder, singleFile)
    {
        Initialize(folder, singleFile);
    }
    
    public override void Change(string id, Resource item)
    {
        throw new NotImplementedException();
    }

    public override void Delete(string id, Resource item)
    {
        throw new NotImplementedException();
    }

    protected override void HandleAdd(string id, Resource newItem)
    {
        throw new NotImplementedException();
    }
}