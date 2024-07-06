using System.Collections.Concurrent;
using System.Dynamic;
using System.Text.Json;

namespace Hammer.Agent.Storage.Json;

public abstract class JsonStore<T> : IStore<string, T>, IDisposable 
{
    private readonly FileSystemWatcher _fileWatcher;

    protected ConcurrentDictionary<string, T> Store { get; } = new();

    protected bool AddingFile;

    protected JsonStore(DirectoryInfo folder, bool singleFile)
    {
        // File watcher
        _fileWatcher = new FileSystemWatcher();
        _fileWatcher.Path = folder.FullName;
        _fileWatcher.Filter = "*.json";
        _fileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        _fileWatcher.Changed += OnChanged;
        if (!singleFile)
            _fileWatcher.Created += OnCreated;
        _fileWatcher.Deleted += OnDeleted;
        
        _fileWatcher.EnableRaisingEvents = true;
        
        // Read in each file and deserialize it
        
    }

    protected virtual void Initialize(DirectoryInfo folder, bool singleFile)
    {
        if (singleFile)
        {
            var id = folder.FullName;
            var item = Deserialize(id);
            if(item == null) return;
            HandleAdd(id, item);
        }
        else
        {
            var files = folder.GetFiles("*.json");
            foreach (var file in files)
            {
                var id = file.FullName;
                var item = Deserialize(id);
                if(item == null) continue;
                HandleAdd(id, item);
            }
        }
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        var id = e.FullPath;
        if(!Store.TryGetValue(id, out var item)) return;
        Delete(id, item);

    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        // ToDo: Find a good way to check if File is not written anymore
        if(AddingFile) return;
        var id = e.FullPath;
        var file = Deserialize(id);
        if (file == null) return;
        Add(id, file);
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
            Change(id, file);
        }
        else
        {
            Add(id, file);
        }
    }

    public virtual IEnumerable<T> GetAll() => Store.Values;

    public void Add(string id, T newItem)
    {
        AddingFile = !Store.ContainsKey(id);

        HandleAdd(id, newItem);
        
        AddingFile = false;
    }

    public virtual void Change(string id, T item)
    {
        Serialize(item, id);
        Store[id] = item;
    }

    public virtual void Delete(string id, T item)
    {
        var result = Store.Remove(id, out _);
        if (result)
        {
            new FileInfo(id).Delete();
        }
    }

    protected virtual void HandleAdd(string id, T newItem)
    {
        Store.TryAdd(id, newItem);
        if (AddingFile)
        {
            Serialize(newItem, id);
        }
    }


    ~JsonStore()
    {
        Dispose(false);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _fileWatcher.Created -= OnCreated;
            _fileWatcher.Deleted -= OnDeleted;
            _fileWatcher.Changed -= OnChanged;
            _fileWatcher.Dispose();
        }
    }
}