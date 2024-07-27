// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Hammer.Agent;
using Hammer.Agent.Controller;
using Hammer.Agent.HostInterface;
using Hammer.Agent.McServer;
using Hammer.Agent.Process;
using Hammer.Agent.Resource;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

CliHeaderPrinter.PrintHeader();

var builder =
    Host.CreateDefaultBuilder(args)
        .UseSystemd()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddSerilog(configuration =>
            {
                configuration.WriteTo.Console();
            });

            //If on linux add the linux host process executor
            if (OperatingSystem.IsLinux())
            {
                services.AddSingleton<IHostProcessExecutor, LinuxHostProcessExecutor>();
            }
            
            services.AddHostedService<ControllerRegistry>();
            services.AddSingleton<IResourceStore, ResourceStore>();
            services.AddSingleton<ResourceManager>();
            services.AddHostedService<ResourceManagerStartup>();
            
            services.AddSingleton<IController,McServerController>();
            services.AddSingleton<IController,ProcessController>();
        });


var host = builder.Build();
await host.StartAsync();

