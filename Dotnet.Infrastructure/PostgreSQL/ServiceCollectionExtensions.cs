using Dotnet.Application.Interfaces;
using Dotnet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Infrastructure.PostgreSQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgreSql(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSql");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly("Dotnet.Infrastructure");
                npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
            });

            // Enable detailed errors in development
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        // Register PostgreSQL service for Product
        services.AddScoped<IService<Product>, PostgreSqlService>();

        return services;
    }
}
