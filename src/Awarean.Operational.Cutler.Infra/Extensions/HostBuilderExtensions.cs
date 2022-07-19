using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Awarean.Operational.Cutler.Infra.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureApplicationLogging(this IHostBuilder builder)
    {
        builder.UseSerilog((context, logging) =>
        {
            logging.Enrich.FromLogContext();
            logging.WriteTo.Debug();
            logging.WriteTo.Console();
            logging.WriteTo.MongoDBBson($"{context.Configuration.GetConnectionString(nameof(MongoDB))}Logging");
            logging.ReadFrom.Configuration(context.Configuration);
        });

        return builder;
    }
}