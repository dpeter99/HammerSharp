namespace Hammer.Agent.Resource;

public class ResourceTypeId
{
    private string Name;

    public ResourceTypeId(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}