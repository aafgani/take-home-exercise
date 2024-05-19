using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace API.Core
{
    public static class Startup
    {
        public static IServiceCollection AddCore(
           this IServiceCollection services)
        {
            services.AddMediatR(config => {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

            return services;
        }
    }
}
