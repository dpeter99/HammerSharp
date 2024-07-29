// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using Hammer.Agent.Cli.Commands;
using Hammer.Agent.Cli.Commands.Server;
using Hammer.Agent.Cli.Middleware;
using Hammer.Agent.Cli.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace Hammer.Agent.Cli;

static class Program
{
    private static readonly string SocketPath = Path.Combine(Path.GetTempPath(), "socket.tmp");
    
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Hammer Agent ClI");
        rootCommand.AddCommand(new ServerCommand());


        var builder = new CommandLineBuilder(rootCommand).UseDefaults()
            .UseDependencyInjection(services =>
            {
                services.AddSingleton(AnsiConsole.Console);
                services.AddSingleton<IGrpcChannelService>(provider => new GrpcChannelService(SocketPath));
            });
        
        
        try
        {
            return await builder.Build().InvokeAsync(args);
        }
        catch (Exception e)
        {
            AnsiConsole.Console.WriteException(e);
            return 1;
        }
    }
}