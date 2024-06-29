using System.Diagnostics;

namespace Hammer.Agent.GameServers;

public class MinecraftServer
{
    private string _serverName;
    private readonly DirectoryInfo _serverFolder;
    private readonly FileInfo _serverJar;

    private Process _serverProcess;

    public MinecraftServer(string serverName, DirectoryInfo serverFolder, FileInfo serverJar)
    {
        _serverName = serverName;
        _serverFolder = serverFolder;
        _serverJar = serverJar;
    }

    private ProcessStartInfo GetStartInfo()
    {
        var linux = new ProcessStartInfo("/usr/bin/sh", new []
        {
            "-c",
            "nohup /usr/bin/java"+
            " -Xmx1024M" +
            " -Xms1024M" +
            " -jar " +
            _serverJar.FullName +
            "" +
            " &"
        })
        {
            WorkingDirectory = _serverFolder.FullName,
            UseShellExecute = true,
        };

        return linux;
    }
    
    public Task StartServer()
    {
        MakeEula();

        Process p = Process.Start(GetStartInfo())!;
        return Task.CompletedTask;
    }

    public Task StopServer()
    {
        
        
    }
    
    void MakeEula()
    {
        var eula = new FileInfo(_serverFolder.FullName + "/eula.txt");
        if (eula.Exists) return;
        
        var writer = new StreamWriter(eula.FullName);
        writer.Write(
            """
            #By changing the setting below to TRUE you are indicating your agreement to our EULA (https://aka.ms/MinecraftEULA).
            #Fri Jun 28 22:34:40 CEST 2024
            eula=true
            """);
        writer.Close();
    }
    
}