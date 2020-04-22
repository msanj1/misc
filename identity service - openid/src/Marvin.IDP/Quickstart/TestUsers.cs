// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace Marvin.IDP
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{SubjectId = "d860efca-22d9-47fd-8249-791ba61b07c7", Username = "Frank", Password = "password", 
                Claims = 
                {
                    //new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Frank"),
                    new Claim(JwtClaimTypes.FamilyName, "Underwood"),
                    new Claim(JwtClaimTypes.Address, "Main Road 1"),
                     new Claim(JwtClaimTypes.Role, "FreeUser"),
                     new Claim("subscriptionlevel", "FreeUser"),
                     new Claim("country", "nl"),
                    //new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    //new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    //new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    //new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }
            },
            new TestUser{SubjectId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7", Username = "Claire", Password = "password", 
                Claims = 
                {
                    //new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Claire"),
                    new Claim(JwtClaimTypes.FamilyName, "password"),
                     new Claim(JwtClaimTypes.Address, "Big Road 2"),
                     new Claim(JwtClaimTypes.Role, "PayingUser"),
                     new Claim("subscriptionlevel", "PayingUser"),
                     new Claim("country", "be"),
                    //new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                    //new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    //new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    //new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    //new Claim("location", "somewhere")
                }
            }
        };
    }
}