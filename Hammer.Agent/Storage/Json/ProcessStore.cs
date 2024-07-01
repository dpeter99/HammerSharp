namespace Hammer.Agent.Storage.Json;

public class ProcessStore(DirectoryInfo folder, bool singleFile) : JsonStore<ManagedProcess>(folder, singleFile)
{

    public override void Change(ManagedProcess item)
    {
        throw new NotImplementedException();
    }

    public override void Delete(ManagedProcess item)
    {
        throw new NotImplementedException();
    }

    protected override void HandleAdd(ManagedProcess newItem)
    {
        throw new NotImplementedException();
    }
}