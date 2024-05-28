using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Web.UI;

public static class Extension
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                       .AddMicrosoftIdentityWebApp(options =>
                       {
                           configuration.Bind("AzureAD", options);
                           options.ResponseType = OpenIdConnectResponseType.Code;
                           options.Events ??= new OpenIdConnectEvents();
                           options.Events.OnTokenValidated += OnTokenValidatedFunc;
                           options.Events.OnAuthorizationCodeReceived += OnAuthorizationCodeReceivedFunc;
                           options.SaveTokens = true;
                       });

        services.AddRazorPages()
        .AddMicrosoftIdentityUI();

        return services;
    }

    private static async Task OnAuthorizationCodeReceivedFunc(AuthorizationCodeReceivedContext context)
    {
        await Task.CompletedTask.ConfigureAwait(false);
    }

    private static async Task OnTokenValidatedFunc(TokenValidatedContext context)
    {
        var token = context.SecurityToken.RawData;
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
