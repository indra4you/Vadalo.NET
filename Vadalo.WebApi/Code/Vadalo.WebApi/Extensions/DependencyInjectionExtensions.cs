using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Vadalo.Web.Api;

internal static class DependencyInjectionExtensions
{
    internal static IServiceCollection AddDatabase(
        this IServiceCollection serviceCollection,
        ConfigurationManager configurationManager
    )
    {
        var databaseConnectionString = configurationManager
            .GetValue<string>(
                "ConnectionString"
            );
        if (databaseConnectionString.HasNoValue())
            throw new ConfigurationValidationException(
                "'ConnectionString' is null or empty"
            );

        serviceCollection
            .AddSingleton(
                new Data
                    .SqlServerDataOptions(
                        databaseConnectionString!
                    )
            )
            .AddSingleton<Data.IDataProvider, Data.SqlServerDataProvider>()
            .AddTransient<Vadalo.HealthCheck.IHealthCheck, HealthCheck.SqlServerHealthCheque>();

        return serviceCollection;
    }

    internal static IServiceCollection AddNotification(
        this IServiceCollection serviceCollection,
        ConfigurationManager configurationManager,
        IWebHostEnvironment environment
    )
    {
        if (environment.IsDevelopment())
        {
            var emailNotificationOptions = configurationManager
                .GetSection(
                    "EmailNotification"
                )
                .Get<Notification.EmailNotificationToDirectoryOptions>();
            if (null == emailNotificationOptions)
                throw new ConfigurationValidationException(
                    "'EmailNotification' section for 'ToDirectory' is null"
                );

            serviceCollection
                .AddSingleton(
                    emailNotificationOptions
                )
                .AddTransient<Notification.IEmailNotificationService, Notification.EmailNotificationToDirectoryService>();
        }
        else
        {
            var emailNotificationOptions = configurationManager
                .GetSection(
                    "EmailNotification"
                )
                .Get<Notification.EmailNotificationToServerOptions>();
            if (null == emailNotificationOptions)
                throw new ConfigurationValidationException(
                    "'EmailNotification' section for 'ToServer' is null"
                );

            serviceCollection
                .AddSingleton(
                    emailNotificationOptions
                )
                .AddTransient<Notification.IEmailNotificationService, Notification.EmailNotificationToServerService>();
        }

        serviceCollection
            .AddTransient<Vadalo.HealthCheck.IHealthCheck, HealthCheck.EmailNotificationHealthCheque>();

        return serviceCollection;
    }

    internal static IServiceCollection AddIdentity(
        this IServiceCollection serviceCollection,
        ConfigurationManager configurationManager
    )
    {
        var oneTimePasswordOptions = configurationManager
            .GetSection(
                "OneTimePassword"
            )
            .Get<Identity.Providers.OneTimePasswordOptions>();
        if (null == oneTimePasswordOptions)
            throw new ConfigurationValidationException(
                "'OneTimePassword' section is null"
            );

        serviceCollection
            .AddSingleton(
                oneTimePasswordOptions
            );

        serviceCollection
            .AddTransient<Identity.Providers.IIdentityDataProvider, Identity.Providers.IdentityDataProvider>()
            .AddTransient<Identity.Providers.IPasswordProvider, Identity.Providers.OneTimePasswordProvider>()
            .AddTransient<Identity.Providers.IEmailNotificationProvider, Identity.Providers.EmailNotificationProvider>()
            .AddTransient<Identity.IdentityService>();

        return serviceCollection;
    }
}