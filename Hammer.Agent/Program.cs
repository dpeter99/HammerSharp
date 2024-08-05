// See https://aka.ms/new-console-template for more information

using Hammer.Agent;
using Hammer.Agent.Controller;
using Hammer.Agent.grpc.Services;
using Hammer.Agent.HostInterface;
using Hammer.Agent.McServer;
using Hammer.Agent.Process;
using Hammer.Agent.Resource;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;


CliHeaderPrinter.PrintHeader();

var socketPath = Path.Combine(Path.GetTempPath(), "socket.tmp");

var builder =
    Host.CreateDefaultBuilder(args)
        .UseSystemd()
        .ConfigureWebHostDefaults(webHostBuilder =>
        {
            webHostBuilder.ConfigureKestrel(options =>
            {
                options.ListenUnixSocket(socketPath, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
            });

            webHostBuilder.Configure(applicationBuilder =>
            {

                applicationBuilder.UseSerilogRequestLogging();
                applicationBuilder.UseRouting();
                
                applicationBuilder.UseEndpoints(routeBuilder =>
                {
                    routeBuilder.MapGrpcService<ServerListerService>();
                });
            });

        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddGrpc();
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

await host.RunAsync();