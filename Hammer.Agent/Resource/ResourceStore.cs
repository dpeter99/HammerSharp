using Hammer.Agent.McServer;
using Hammer.Agent.Process;

namespace Hammer.Agent.Resource;

public class ResourceStore : IResourceStore
{
    private List<Resource> _resources = new();

    public IEnumerable<Resource> Resources => _resources;

    public ResourceStore()
    {
        var processResource = new ProcessResource(
            new ProcessDescriptor(
                executable: "/usr/bin/java",
                arguments:
                [
                    "-Xmx1024M",
                    "-Xms1024M",
                    "-jar",
                    "/home/unbekannt/RiderProjects/HammerSharpRunTest/versions/server.jar"
                ],
                workingDirectory: "/home/unbekannt/RiderProjects/HammerSharpRunTest/test"
            ),
            new ProcessState()
            {
                PID = -500,
                Status = ProcessStatus.Running
            }
        )
        {
            ID = "test_process_01",
            Name = "Mc Server Process",
            ParentID = null,
        };
        _resources.Add(processResource);
    }

    public async Task<bool> Add(Resource resource)
    {
        _resources.Add(resource);
        ChangeEvent?.Invoke(resource);
        return true;
    }

    public ResourceChangeEvent ChangeEvent { get; set; }
}