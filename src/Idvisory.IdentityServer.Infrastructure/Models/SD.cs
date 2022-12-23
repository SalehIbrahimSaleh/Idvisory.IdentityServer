using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Idvisory.IdentityServer.Infrastructure.Models;

public static class SD
{
    public const string Admin = "Admin";
    public const string User = "User";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address()
        };
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>()
        {
                new ApiScope("advisory","advisory server"),
                new ApiScope(name: "read", displayName: "Read your data."),
                new ApiScope(name: "write", displayName: "Write your data"),
                new ApiScope(name: "delete", displayName: "Delete your data")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>()
        {
                new Client
                {
                    ClientId="user",
                    ClientSecrets= { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={ "read", "write","profile"}
                },
                 new Client
                {
                    ClientId = "Admin",
                    ClientSecrets= { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:7238/signin-oidc" },
                    PostLogoutRedirectUris={ "https://localhost:7238/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.Email,
                         IdentityServerConstants.StandardScopes.Phone,
                         IdentityServerConstants.StandardScopes.Address,
                         "advisory"
                    }
                }
        };
}
