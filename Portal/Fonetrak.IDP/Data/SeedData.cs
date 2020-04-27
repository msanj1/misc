// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Fonetrak.IDP.Models;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fonetrak.IDP.Data
{
    public class SeedData
    {
        public static void EnsureSeedData(IApplicationBuilder app)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                if (configuration["Environment:Seed"] == "True")
                {
                    var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                    var configurationContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                    applicationDbContext.Database.Migrate();
                    configurationContext.Database.Migrate();

                    if (!configurationContext.Clients.Any())
                    {
                        var clients = configuration.GetSection("Clients").Get<List<Client>>();

                        foreach (var client in clients)
                        {
                            configurationContext.Clients.Add(client.ToEntity());
                            configurationContext.SaveChanges();
                        }
                    }

                    if (!configurationContext.IdentityResources.Any())
                    {
                        foreach (var resource in Config.Ids)
                            configurationContext.IdentityResources.Add(resource.ToEntity());
                        configurationContext.SaveChanges();
                    }

                    if (!configurationContext.ApiResources.Any())
                    {
                        foreach (var resource in Config.Apis)
                            configurationContext.ApiResources.Add(resource.ToEntity());
                        configurationContext.SaveChanges();
                    }

                    if (!userManager.Users.Any())
                    {
                        var developmentUser = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = "Development",
                            Email = "foneboxdevelopmentteam@fonebox.com.au"
                        };
                        var seedPassword = configuration.GetSection("Seed:RootPassword").Get<string>();
                        developmentUser.PasswordHash = passwordHasher.HashPassword(developmentUser, seedPassword);

                        userManager.CreateAsync(developmentUser).Wait();
                        userManager.AddClaimsAsync(developmentUser, new List<Claim>
                        {
                            new Claim(JwtClaimTypes.GivenName, "Development"),
                            new Claim(JwtClaimTypes.FamilyName, ""),
                            new Claim(JwtClaimTypes.Address, "300 Adelaide st, Brisbane City QLD 4000"),
                            new Claim(JwtClaimTypes.Role, "SysAdmin")
                        }).Wait();

                        var normalUser = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = "John",
                            Email = "foneboxdevelopmentteam@fonebox.com.au"
                        };
                        normalUser.PasswordHash = passwordHasher.HashPassword(normalUser, seedPassword);

                        userManager.CreateAsync(normalUser).Wait();
                        userManager.AddClaimsAsync(normalUser, new List<Claim>
                        {
                            new Claim(JwtClaimTypes.GivenName, "John"),
                            new Claim(JwtClaimTypes.FamilyName, "Doe"),
                            new Claim(JwtClaimTypes.Address, "300 Adelaide st, Brisbane City QLD 4000"),
                            new Claim(JwtClaimTypes.Role, "Admin")
                        }).Wait();
                    }
                }
            }
        }
    }
}