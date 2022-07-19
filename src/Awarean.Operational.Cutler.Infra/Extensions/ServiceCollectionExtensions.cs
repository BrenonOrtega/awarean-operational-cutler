using Awarean.Operational.Cutler.Application.Repositories;
using Awarean.Operational.Cutler.Infra.Configurations;
using Awarean.Operational.Cutler.Infra.Constants;
using Awarean.Operational.Cutler.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Awarean.Operational.Cutler.Infra.Extensions;

public static class ServiceCollectionExtensions 
{
    /// <Summary>
    /// To configure this
    /// </Summary>
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDb(configuration);

        return services;
    } 

    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IFoodRepository, MongoDbFoodRepository>();
        services.AddSingleton<IMongoClient>(_ => new MongoClient(configuration.GetConnectionString(nameof(MongoDB))));
        services.Configure<DatabaseConfiguration>(DatabaseConstants.FoodsDatabase, configuration.GetSection(DatabaseConstants.FoodsDatabase));
        
        return services;
    }
}