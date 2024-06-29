namespace Hammer.Agent.StateHolder;

public interface IStateHolder
{
    public  Dictionary<string, ProcessData> process { get; }

    public void AddProcess(ProcessData data);
    
    public void RemoveProcess(string processName);

    public void UpDateProcess(string FriendlyName);

    public void UpdateProcess(ProcessData data);

}