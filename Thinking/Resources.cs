using System.Text.Json.Serialization;

namespace Thinking;

using System.Collections.Generic;
using ResourceTypeID = string;

public class ResourceManager
{
    private readonly ResourceStore _store;
    private Dictionary<string, Resource> resources;

    public ResourceManager(ResourceStore store)
    {
        _store = store;
        //_store.OnChange += This.AddedNewResource
    }

    public void StartUp()
    {
        // Ask the store for the resources
        // For each resource
        //  call the correct controller's Recover
    }
    
    // Similar to add and 
    public void AddedNewResource(Resource r)
    {
        
    }
}

public abstract class Resource
{
    [JsonInclude] public string ID { get; set; } // test_mc_server_01
    [JsonInclude] public string Name { get; set; } // My little test server
    [JsonInclude] public abstract ResourceTypeID Type { get; } // built-in:mc-server

    [JsonInclude] public abstract ResourceDescriptor _descriptor { get; }
    public abstract ResourceState _state { get;}
    
    
    public abstract bool IsHealthy { get; }
}

// what is currently running
// In the future multiple subclasses for LongRunningTask, FireAndForgetTask etc.
public abstract class ResourceState
{
    public abstract bool IsRunning { get; }
    // public List<Warning> Warnings;
}

// What the user wants 
public class ResourceDescriptor
{
    
}

public class ControllerRegistry
{
    private Dictionary<ResourceTypeID, Controller<Resource>> _controllers;
}

public abstract class Controller<T> where T : Resource
{
    public abstract void Recover(T resource, ResourceManager rmgr);
    public abstract void Added(T resource, ResourceManager rmgr);
    public abstract void Changed(T resource, ResourceManager rmgr);
    public abstract void Removed(T resource, ResourceManager rmgr);
}



