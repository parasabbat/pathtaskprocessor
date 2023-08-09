// See https://aka.ms/new-console-template for more information
using AutomationPipeline;
using AutomationPipeline.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
         .ConfigureServices(services =>
         {
             services.AddSingleton<ITaskProcessor, DefaultTaskProcessor>()
             .AddSingleton<ICommandService, CommandService>();
         }).Build();

var assayIngestor = host.Services.GetRequiredService<ITaskProcessor>();

await assayIngestor.DoWorkAsync();
