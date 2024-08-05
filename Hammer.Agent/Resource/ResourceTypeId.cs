namespace Hammer.Agent.Resource;

public class ResourceTypeId
{
    private string _name;

    public ResourceTypeId(string name)
    {
        _name = name;
    }

    public string Name => _name;

    public override string ToString()
    {
        return _name;
    }
}