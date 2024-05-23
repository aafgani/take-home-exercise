using System;
using API.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API;

public static class Extension
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseConfiguration>(configuration.GetSection("DatabaseConfiguration"));

        return services;
    }
}
