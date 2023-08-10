// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RefrigeratorApp;
using RefrigeratorApp.Interfaces;
using RefrigeratorApp.Repositories;

IConfiguration Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
        .AddSingleton(Configuration)
        .AddScoped<ITaskProcessor, DefaultTaskProcessor>()
        .AddDbContext<RaContext>(options => options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase")))
        .AddScoped<IProductRepository, ProductRepository>();
    }).Build();

var assayIngestor = host.Services.GetRequiredService<ITaskProcessor>();

await assayIngestor.DoWorkAsync();
