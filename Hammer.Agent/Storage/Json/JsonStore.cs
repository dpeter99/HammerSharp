using System.Dynamic;
using System.Text.Json;

namespace Hammer.Agent.Storage.Json;

public abstract class JsonStore<T> : IStore<T>, IDisposable
{
    private readonly DirectoryInfo _folder;
    private readonly bool _singleFile;
    protected List<T> _data;
    private readonly FileSystemWatcher _fileWatcher;

    protected Dictionary<string, T> Store { get; } = new();

    protected bool _addingFile;

    public JsonStore(DirectoryInfo folder, bool singleFile)
    {
        _folder = folder;
        _singleFile = singleFile;
        _fileWatcher = new FileSystemWatcher();
        _fileWatcher.Path = folder.FullName;
        _fileWatcher.Filter = "*.json";
        _fileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        _fileWatcher.Changed += OnChanged;
        _fileWatcher.Created += OnCreated;
        _fileWatcher.Deleted += OnDeleted;
        
        _fileWatcher.EnableRaisingEvents = true;
        // File watcher

        // Read in each file and deserialize it
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        var id = e.FullPath;
        var item = Store[id];
        OnDelete(item);

    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        // ToDo: Find a good way to check if File is not written anymore
        if(_addingFile) return;
        var id = e.FullPath;
        var file = Deserialize(id);
        if (file == null) return;
        Store.TryAdd(id, file);
        OnCreate(file);
    }

    protected T? Deserialize(string path)
    {
        return JsonSerializer.Deserialize<T>(File.ReadAllBytes(path));
    }

    protected void Serialize(T item, string path)
    {
        var jsonItem = JsonSerializer.Serialize(item, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        using StreamWriter sw = new StreamWriter(path, false);
        sw.Write(jsonItem);
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        var id = e.FullPath;
        var file = Deserialize(id);
        if(file == null) return;
        if (!Store.TryAdd(id, file))
        {
            Store[id] = file;
            OnChange(file);
        }
        else
        {
            OnCreate(file);
        }
    }

    protected void OnDelete(T item)
    {
        
    }

    protected void OnCreate(T item)
    {
        
    }

    protected void OnChange(T item)
    {
        
    }
    

    public virtual IEnumerable<T> GetAll() => Store.Values;

    public void Add(T newItem)
    {
        _addingFile = true;

        HandleAdd(newItem);
        
        _addingFile = false;
    }

    public abstract void Change(T item);

    public abstract void Delete(T item);
    
    protected abstract void HandleAdd(T newItem);


    public void Dispose()
    {
        _fileWatcher.Created -= OnCreated;
        _fileWatcher.Deleted -= OnDeleted;
        _fileWatcher.Changed -= OnChanged;
        _fileWatcher.Dispose();
    }
}