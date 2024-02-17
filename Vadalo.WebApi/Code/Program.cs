using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Vadalo.Web.Api;

public class Program
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

        // Add services to the container
        webApplicationBuilder.Services
            .AddLogging(
                configure =>
                {
                    configure
                        .AddConsole()
                        .AddConfiguration(webApplicationBuilder.Configuration.GetSection("Logging"))
                        .SetMinimumLevel(LogLevel.Information);
                }
            )
            .AddControllers();

        // Add health check to the container
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