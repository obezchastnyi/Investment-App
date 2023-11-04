using System;
using InvestmentApp;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class DbServiceCollectionsExtensions
{
    public static IServiceCollection AddDbServices<T>(
        this IServiceCollection services, string connectionString, string pgVersionString)
        where T : DbContext
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException(string.Format(Constants.NullOrEmptyErrorMessage, nameof(connectionString)));
        }

        if (string.IsNullOrEmpty(pgVersionString))
        {
            throw new ArgumentException(string.Format(Constants.NullOrEmptyErrorMessage, nameof(pgVersionString)));
        }

        var pgVersion = new Version(pgVersionString);
        services.AddDbContextPool<T>(contextOptions =>
        {
            contextOptions.UseNpgsql(connectionString, npgOptions =>
            {
                npgOptions.SetPostgresVersion(pgVersion);
                npgOptions.MigrationsAssembly("InvestmentApp-Server")
                    .EnableRetryOnFailure();
            });
        });

        return services;
    }
}
