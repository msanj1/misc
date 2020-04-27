// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Fonetrak.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>(){ "role" }
                )
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                //new ApiResource("api1", "My API #1")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // SPA client using code flow + pkce
                new Client
                {
                    //AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 600, //10 minutes
                    
                    //AllowOfflineAccess = true,
                    //UpdateAccessTokenClaimsOnRefresh = true,
                    AllowAccessTokensViaBrowser = true,
                    ClientName = "Self Service SPA Client",
                    ClientId = "selfservicespaclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RequireConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "http://localhost:4200/signin-callback","http://localhost:4200/assets/silent-callback.html"
                    },
                    PostLogoutRedirectUris = new List<string>(){
                        "http://localhost:4200/signout-callback"
                    },
                    AllowedScopes = {
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        //"imagegalleryapi",
                        //"country",
                        //"subscriptionlevel"
                    },
                    //ClientSecrets =
                    //{
                    //    new Secret("5elf5erv1c6".Sha256())
                    //}
                }
            };
    }
}