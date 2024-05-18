using API.Core;
using API.Infrastructure.Managers;
using API.Infrastructure.Maps;
using Dapper.FluentMap;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services)
        {
            services.AddScoped<IListManager, ListManager>();

            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ListingMap());
            });

            return services;
        }
    }
}
