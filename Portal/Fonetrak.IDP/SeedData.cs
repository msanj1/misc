// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Fonetrak.IDP.Data;
using Fonetrak.IDP.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Fonetrak.IDP
{
    public class SeedData
    {
        public static void EnsureSeedData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                var configurationContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                applicationDbContext.Database.Migrate();
                configurationContext.Database.Migrate();

                if (!configurationContext.Clients.Any())
                {
                    var clients = configuration.GetSection("Clients").Get<List<Client>>();

                    foreach (var client in clients) configurationContext.Clients.Add(client.ToEntity());
                    configurationContext.SaveChanges();
                }

                if (!configurationContext.IdentityResources.Any())
                {
                    foreach (var resource in Config.Ids) configurationContext.IdentityResources.Add(resource.ToEntity());
                    configurationContext.SaveChanges();
                }

                if (!configurationContext.ApiResources.Any())
                {
                    foreach (var resource in Config.Apis) configurationContext.ApiResources.Add(resource.ToEntity());
                    configurationContext.SaveChanges();
                }
            }
        }
    }
}
