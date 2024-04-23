using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Vadalo.Web.Api;

public static class Program
{
    public static void Main(
        string[] args
    )
    {
        var webApplicationBuilder = WebApplication
            .CreateBuilder(
                args
            );

        webApplicationBuilder.Configuration
            .AddEnvironmentVariables();

        // Add logger
        webApplicationBuilder.Services
            .AddLogging(
                configure =>
                {
                    configure
                        .AddConsole()
                        .AddConfiguration(
                            webApplicationBuilder
                                .Configuration
                                .GetSection(
                                    "Logging"
                                )
                        );

                    if (webApplicationBuilder.Environment.IsDevelopment())
                        configure
                            .SetMinimumLevel(
                                LogLevel.Information
                            );
                }
            );

        webApplicationBuilder.Services
            .AddDatabase(
                webApplicationBuilder.Configuration
            )
            .AddNotification(
                webApplicationBuilder.Configuration,
                webApplicationBuilder.Environment
            )
            .AddIdentity(
                webApplicationBuilder.Configuration
            );

        // Add controller
        webApplicationBuilder.Services
            .AddControllers();

        // Add Health Check
        webApplicationBuilder.Services
            .AddHealthChecks();

        var webApplication = webApplicationBuilder
            .Build();

        webApplication
            .MapControllers();

        webApplication
            .UseHttpsRedirection();

        webApplication
            .Run();
    }
}