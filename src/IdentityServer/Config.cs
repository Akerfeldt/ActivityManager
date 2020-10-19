// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("amgr_api", "Activity Manager API")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) },
                    UserClaims = { IdentityServerConstants.StandardScopes.Email }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    AccessTokenType = AccessTokenType.Reference,
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "amgr_api"
                    },
                    AllowOfflineAccess = true,
                    ClientId = "amgr.app",
                    ClientName = "Activity Manager Angular App",
                    ClientUri = "http://localhost:4200",
                    IdentityProviderRestrictions = Array.Empty<string>(),
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    RedirectUris = { "http://localhost:4200/authorized" },
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = true,
                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedCorsOrigins = { "https://localhost:44336" },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "amgr_api"
                    },
                    AllowOfflineAccess = true,
                    ClientId = "amgr.blazor",
                    ClientName = "Activity Manager Blazor App",
                    ClientUri = "https://localhost:44336",
                    IdentityProviderRestrictions = Array.Empty<string>(),
                    PostLogoutRedirectUris = { "https://localhost:44336/authentication/logout-callback" },
                    RedirectUris = { "https://localhost:44336/authentication/login-callback" },
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = true,
                    UpdateAccessTokenClaimsOnRefresh = true
                }
            };

    }
}