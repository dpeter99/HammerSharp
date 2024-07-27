// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using System.Net.Sockets;
using Grpc.Net.Client;
using Hammer.Agent.Cli.Commands;
using Hammer.Agent.Cli.IPC.UDS;

namespace Hammer.Agent.Cli;

static class Program
{
    public static readonly string SocketPath = Path.Combine(Path.GetTempPath(), "socket.tmp");
    
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Hammer Agent ClI");
        rootCommand.AddCommand(new ServerCommand());


        try
        {
            return await rootCommand.InvokeAsync(args);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}