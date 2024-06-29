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
        //ProcessStartInfo("/usr/bin/sh -c ");
        
        var s = new ProcessStartInfo("java", new []
        {
            //"java"
            "-Xmx1024M",
            "-Xms1024M",
            "-jar",
            _serverJar.FullName,
            "nogui",
        })
        {
            WorkingDirectory = _serverFolder.FullName,
            CreateNoWindow = true,
            UseShellExecute = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardError = false,
            RedirectStandardInput = false,
            RedirectStandardOutput = false,
        };

        return s;
    }
    
    public Task StartServer()
    {
        MakeEula();

        Process p = Process.Start(GetStartInfo())!;

        return Task.CompletedTask;
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