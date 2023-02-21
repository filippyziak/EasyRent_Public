using System;
using System.Linq;
using EasyRent.Configuration;
using EasyRent.Identity;
using EasyRent.Infrastructure.Logging;
using EasyRent.Libraries.Provider;
using EasyRent.Management;
using EasyRent.Modules;
using EasyRent.RentalAd;
using EasyRent.Reservation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;

const string ServiceName = "EasyRent";
const string Version = "v1";

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

var logger = NLogFactory.GetLoggerFromConfiguration();

builder.Logging.ClearProviders();

var environmentProvider = new EnvironmentProvider();

try
{
    logger.Info("Starting application: {ServiceName}", ServiceName);
    logger.Info(environmentProvider.Stage);

    var moduleConfiguration = new ModulesConfiguration(new ModuleManifest[]
    {
        new IdentityModule(),
        new RentalAdModule(),
        new ManagementModule(),
        new ReservationModule()
    });

    builder.Configuration.GetSection(nameof(ModulesConfiguration)).Bind(moduleConfiguration);
    var enabledModules = moduleConfiguration.GetEnabledModules();

    logger.Info("Enabled modules loaded from the configuration. Count: {ModulesCount}", moduleConfiguration.EnabledModules.Count);

    builder.Host.ConfigureModules(environmentProvider);
    logger.Trace("> Modules configurations added to the configuration root");

    builder.Services.AddInfrastructure(ServiceName,
        Version,
        builder.Configuration,
        moduleConfiguration.EnabledModulesAssembliesNames.ToList(), environmentProvider);
    logger.Trace("> Infrastructure services registered successfully");

    foreach (var module in enabledModules)
    {
        module.Register(builder.Services, builder.Configuration, environmentProvider);
        logger.Trace("> Services for module {ModuleName} registered successfully", module.Name);
    }

    var app = builder.Build();

    app.UseInfrastructure(environmentProvider);
    logger.Trace("> Infrastructure middlewares registered successfully");

    foreach (var module in enabledModules)
    {
        module.Use(app, environmentProvider);
        logger.Trace("> Middlewares for module {ModuleName} registered successfully", module.Name);
    }

    logger.Info("Application {ServiceName} with enabled modules registered successfully", ServiceName);
    logger.Info("Application started and listening on the host: {ApplicationHosts}", environmentProvider.ApplicationUrls);
    logger.Info("Hosting environment stage: {Stage}", environmentProvider.Stage);

    await app.RunAsync();
}
catch (Exception e)
{
    logger.Error("An error occurred during application startup", e);
}
finally
{
    LogManager.Shutdown();
}