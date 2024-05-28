﻿using API.Core;
using API.Core.Interface;
using API.Infrastructure.Managers;
using API.Infrastructure.Maps;
using API.Infrastructure.Repositories;
using Dapper.FluentMap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddScoped<IListManager, ListManager>();

            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ListingMap());
            });

            services.AddScoped((s) => new SqlConnection(configuration.GetConnectionStringOrThrown("Todo")));
            services.AddScoped<IDbContext, DbContext>(s =>
            {
                SqlConnection conn = s.GetRequiredService<SqlConnection>();
                conn.Open();
                return new DbContext(conn);
            });
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }
    }

    public static class ConfigurationExtension
    {
        public static string GetConnectionStringOrThrown(this IConfiguration configuration, string name)
        {
            return configuration
                .GetConnectionString(name) ?? throw new InvalidOperationException($"The connection string {name} was not supported.");
        }
    }
}
