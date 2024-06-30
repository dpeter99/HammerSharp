namespace Thinking;

public interface IStore<T>
{
    public IEnumerable<T> GetAll();

    public void Add(T newItem);
}

public abstract class JsonStore<T> : IStore<T>
{
    private List<T> data;
    
    public JsonStore(DirectoryInfo folder, bool singleFile)
    {
        // File watcher
        
        // Read in each file and deserialize it
    }

    public abstract IEnumerable<T> GetAll();

    public abstract void Add(T newItem);

}

public class ResourceStore(DirectoryInfo folder, bool singleFile) : JsonStore<Resource>(folder, singleFile)
{
    public override IEnumerable<Resource> GetAll()
    {
        throw new NotImplementedException();
    }

    public override void Add(Resource newItem)
    {
        throw new NotImplementedException();
    }
}

public class ProcessStore(DirectoryInfo folder, bool singleFile) : JsonStore<ManagedProcess>(folder, singleFile)
{
    public override IEnumerable<ManagedProcess> GetAll()
    {
        throw new NotImplementedException();
    }

    public override void Add(ManagedProcess newItem)
    {
        throw new NotImplementedException();
    }
}
