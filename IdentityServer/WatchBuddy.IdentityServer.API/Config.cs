using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace WatchBuddy.IdentityServer.API;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources =>
    [
        new("resource_catalog") { Scopes = { "catalog_fullpermission" } },
            new("photo_stock_catalog") { Scopes = { "photo_stock_fullpermission" } },
            new(IdentityServerConstants.LocalApi.ScopeName)
    ];

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
        };

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new("catalog_fullpermission", "Catalog full permission"),
        new("photo_stock_fullpermission", "Photo Stock full permission"),
        new(IdentityServerConstants.LocalApi.ScopeName, "Identity Server API full permission")
    ];

    public static IEnumerable<Client> Clients =>
    [
        new()
            {
                ClientName = "Watch Buddy Client",
                ClientId = "WebMvcClient",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName}
            }
    ];
}