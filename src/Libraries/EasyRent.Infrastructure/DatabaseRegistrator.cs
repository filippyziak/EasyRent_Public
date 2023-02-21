using EasyRent.Infrastructure.Abstractions.Database;
using EasyRent.Infrastructure.Abstractions.Extensions;
using EasyRent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Infrastructure;

public static class DatabaseRegistrator
{
    public static IServiceCollection AddPostgreSqlDbContext<TDbContext, TIUnitOfWork, TUnitOfWork, TDatabaseConfiguration>(
        this IServiceCollection services, IConfiguration configuration)
        where TDbContext : DatabaseContext<TDbContext>
        where TIUnitOfWork : class, IUnitOfWork
        where TUnitOfWork : class, TIUnitOfWork
        where TDatabaseConfiguration : DatabaseConfiguration, new()
        => services.AddDbContext<TDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString<TDatabaseConfiguration>()))
            .AddScoped<TIUnitOfWork, TUnitOfWork>();

    public static IServiceCollection AddDatabaseRepository<TIDatabaseRepository, TDatabaseRepository>(this IServiceCollection services)
        where TIDatabaseRepository : class
        where TDatabaseRepository : class, TIDatabaseRepository
        => services.AddScoped<TIDatabaseRepository, TDatabaseRepository>();

    public static IServiceCollection ConfigureDatabaseConfiguration<TDatabaseConfiguration>(this IServiceCollection services,
        IConfiguration configuration)
        where TDatabaseConfiguration : DatabaseConfiguration, new()
        => services.Configure<TDatabaseConfiguration>(configuration.GetSection(typeof(TDatabaseConfiguration).Name));
}