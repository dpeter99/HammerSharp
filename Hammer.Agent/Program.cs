// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Hammer.Agent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Console.WriteLine("Hello, World!");


var builder =
    Host.CreateDefaultBuilder(args)
        .UseSystemd()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddSerilog(configuration =>
            {
                configuration.WriteTo.Console();
            });
            services.AddHostedService<Agent>();
            services.AddSingleton<ProcessManager>();
            services.Configure<GameConfiguration>(hostContext.Configuration.GetSection("Game"));
            services.Configure<AgentConfiguration>(hostContext.Configuration.GetSection("Agent"));
        });

var host = builder.Build();
host.Start();
  