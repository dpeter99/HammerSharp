namespace Hammer.Agent.Storage.Json;

public class ProcessStore : JsonStore<ManagedProcess>
{

    public ProcessStore(DirectoryInfo folder, bool singleFile) : base(folder, singleFile)
    {
        Initialize(folder, singleFile);
    }
    
    public override void Change(string id, ManagedProcess item)
    {
        throw new NotImplementedException();
    }

    public override void Delete(string id, ManagedProcess item)
    {
        throw new NotImplementedException();
    }

    protected override void HandleAdd(string id, ManagedProcess newItem)
    {
        throw new NotImplementedException();
    }
}