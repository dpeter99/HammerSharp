using System.CommandLine;
using Grpc.Core;
using Grpc.Net.Client;
using Hammer.Agent.Cli.Services;
using Spectre.Console;

namespace Hammer.Agent.Cli.Commands.Server;

public class ListServerCommand()
    : Command<ListServerCommandOptions, ListServerCommandOptionHandler>("list", "Lists all currently configured servers");

public class ListServerCommandOptions: ICommandOptions{}

public class ListServerCommandOptionHandler(IAnsiConsole console, IGrpcChannelService grpcChannelService)
    : ICommandOptionsHandler<ListServerCommandOptions>
{
    private readonly GrpcChannel _channel = grpcChannelService.GetChannel();

    public async Task<int> HandleAsync(ListServerCommandOptions options, CancellationToken cancellationToken)
    {
        var client = new ServerLister.ServerListerClient(_channel);
        using var servers = client.GetServers(new ListArgs());
        var table = new Table();
        table.AddColumn("Server Name");
        table.AddColumn("Type");
        table.AddColumn("Is Running");
        await foreach (var server in servers.ResponseStream.ReadAllAsync(cancellationToken: cancellationToken))
        {
            table.AddRow(server.ServerName.EscapeMarkup(), server.Type.EscapeMarkup(), server.Running ? "Y" : "N");
        }
        console.Write(table);
        return 0;
    }
}