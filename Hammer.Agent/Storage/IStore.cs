namespace Hammer.Agent.Storage;

public interface IStore<in TId, T>
{
    public IEnumerable<T> GetAll();

    public void Add(TId id, T newItem);

    public void Change(TId id, T item);

    public void Delete(TId id, T item);
}