using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace WatchBuddy.IdentityServer.API.Configuration;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources =>
    [
        new("resource_catalog") { Scopes = { "catalog_fullpermission" } },
        new("resource_photo_stock") { Scopes = { "photo_stock_fullpermission" } },
        new(IdentityServerConstants.LocalApi.ScopeName)
    ];

    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.Email(),
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new() {Name = "roles", DisplayName =  "Roles" , Description = "List all roles.", UserClaims = ["role"] }
    ];

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
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes =
                { "catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
        },
        new()
        {
            ClientName = "Watch Buddy Client",
            ClientId = "WebMvcClientForUser",
            AllowOfflineAccess = true,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                IdentityServerConstants.LocalApi.ScopeName,
                "roles"
            },
            AccessTokenLifetime = 1 * 60 * 60,
            RefreshTokenExpiration = TokenExpiration.Absolute,
            AbsoluteRefreshTokenLifetime = (int) (DateTime.UtcNow.AddDays(60) - DateTime.UtcNow).TotalSeconds,
            RefreshTokenUsage = TokenUsage.ReUse
        }
    ];
}