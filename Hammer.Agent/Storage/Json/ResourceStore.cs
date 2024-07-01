namespace Hammer.Agent.Storage.Json;

public class ResourceStore(DirectoryInfo folder, bool singleFile) : JsonStore<Resource>(folder, singleFile)
{

    public override void Change(Resource item)
    {
        throw new NotImplementedException();
    }

    public override void Delete(Resource item)
    {
        throw new NotImplementedException();
    }

    protected override void HandleAdd(Resource newItem)
    {
        throw new NotImplementedException();
    }
}