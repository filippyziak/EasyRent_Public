using System;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Infrastructure.Abstractions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Infrastructure;

public static class MongoDbRegistrator
{
    public static IServiceCollection AddMongoDbDocumentRepository<TIMongoDbRepository,
        TMongoDbRepository,
        TDocumentStoreConfiguration>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TIMongoDbRepository : class
        where TMongoDbRepository : class, TIMongoDbRepository
        where TDocumentStoreConfiguration : DocumentStoreConfiguration, new()
        => services.AddScoped<TIMongoDbRepository, TMongoDbRepository>(_ =>
        {
            var mongoDbRepository = Activator.CreateInstance(typeof(TMongoDbRepository),
                new[]
                {
                    configuration.GetConnectionString<TDocumentStoreConfiguration>(),
                    configuration.GetSection(typeof(TDocumentStoreConfiguration).Name).GetValue<string>("DatabaseName")
                });

            return (TMongoDbRepository)mongoDbRepository;
        });
}