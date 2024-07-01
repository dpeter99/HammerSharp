namespace Hammer.Agent.Storage;

public interface IStore<T>
{
    public IEnumerable<T> GetAll();

    public void Add(T newItem);

    public void Change(T item);

    public void Delete(T item);
}