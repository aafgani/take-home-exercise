using System;
using API.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace API;

public static class Extension
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
        {
            configuration.Bind("AzureAd", options);

            options.TokenValidationParameters.NameClaimType = "name";
        },
        options => { configuration.Bind("AzureAd", options); });

        // services.Configure<DatabaseConfiguration>(configuration.GetSection("DatabaseConfiguration"));
        services.Configure<AzureAd>(configuration.GetSection("AzureAd"));

        //test config
        var testConfig = new AzureAd();
        configuration.GetSection("AppConfig").Bind(testConfig);

        return services;
    }
}
