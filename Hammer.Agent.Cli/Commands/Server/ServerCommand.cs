using System.CommandLine;
using System.Net.Sockets;
using Grpc.Core;
using Grpc.Net.Client;
using Hammer.Agent.Cli.IPC.UDS;

namespace Hammer.Agent.Cli.Commands.Server;

public class ServerCommand : Command
{
    public ServerCommand() : base("server", "Base command to handle everything to do with servers")
    {
        AddCommand(new ListServerCommand());
    }
}