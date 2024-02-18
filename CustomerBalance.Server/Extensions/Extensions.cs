using CustomerBalance.Core.Shared;
using CustomerBalance.Server.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerBalance.Server.Extensions;

public static class Extensions
{
    public static IServiceCollection AddSqlServerRepository<T>(this IServiceCollection services) where T : class, IEntity
    {
        services.AddScoped<IBaseCrud<T>, SqlServerRepository<T>>();

        return services;
    }
}
