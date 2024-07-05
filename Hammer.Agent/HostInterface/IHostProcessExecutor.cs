using System.Diagnostics;

namespace Hammer.Agent.HostInterface;

public interface IHostProcessExecutor
{
    System.Diagnostics.Process? Start(ProcessStartInfo info);
    void Stop(System.Diagnostics.Process proc);

    System.Diagnostics.Process? FindById(int pid)
    {
        try
        {
            return System.Diagnostics.Process.GetProcessById(pid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
        
    }
}

public class LinuxHostProcessExecutor : IHostProcessExecutor
{
    public System.Diagnostics.Process? Start(ProcessStartInfo info)
    {
        var linux = Wrap(info);
        return System.Diagnostics.Process.Start(linux);
    }

    public void Stop(System.Diagnostics.Process proc)
    {
        proc.Kill();
    }
    
    
    public ProcessStartInfo Wrap(ProcessStartInfo info)
    {

        string command = $"nohup {info.FileName} {string.Join(" ", info.ArgumentList)} &";
        
        var linux = new ProcessStartInfo("/usr/bin/sh", new [] { "-c", command })
        {
            WorkingDirectory = info.WorkingDirectory,
            UseShellExecute = true,
        };

        return linux;
    }
    
}