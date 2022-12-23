using Idvisory.IdentityServer.Infrastructure.Models;
using Idvisory.IdentityServer.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Idvisory.IdentityServer.Infrastructure;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInsfrastructureIdentityServer(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityServerDbContext>()
            .AddDefaultTokenProviders();

        var identity = services.AddIdentityServer(option =>
        {
            option.Events.RaiseFailureEvents = true;
            option.Events.RaiseSuccessEvents = true;
            option.Events.RaiseErrorEvents = true;
            option.Events.RaiseInformationEvents = true;
            option.EmitStaticAudienceClaim = true;
        })
        .AddInMemoryIdentityResources(SD.IdentityResources)
        .AddInMemoryApiScopes(SD.ApiScopes)
        .AddInMemoryClients(SD.Clients)
        .AddAspNetIdentity<ApplicationUser>()
        .AddProfileService<ProfileService>();

        identity.AddDeveloperSigningCredential();

        return services;
    }
}
