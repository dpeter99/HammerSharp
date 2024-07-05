using System.Text.Json.Serialization;

namespace Hammer.Agent.Resource;

/*
public interface IResource
{
    static abstract ResourceTypeId ResourceTypeId { get; } // built-in:mc-server
}
*/


public abstract class Resource
{
    [JsonInclude] public abstract ResourceTypeId Type { get; }
    
    [JsonInclude] public string ID { get; set; } // test_mc_server_01
    [JsonInclude] public string Name { get; set; } // My little test server
    
    [JsonInclude] public string? ParentID { get; set; } // test_mc_server_01

    [JsonInclude] public abstract ResourceDescriptor Descriptor { get;}
    public abstract ResourceState State { get; }
    
    
    public abstract bool IsHealthy { get; }
}