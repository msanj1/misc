// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Marvin.IDP.Data;
using Marvin.IDP.Identity.Models;
using Marvin.IDP.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Marvin.IDP
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=MarvinIDPDataDB;Trusted_Connection=True;";

            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                        sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    }));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();

            var builder = services.AddIdentityServer();
                //.AddInMemoryIdentityResources(Config.Ids)
                //.AddInMemoryApiResources(Config.Apis)
                //.AddInMemoryClients(Config.Clients)
                //.AddTestUsers(TestUsers.Users);

            // not recommended for production - you need to store your key material somewhere secure
            //builder.AddDeveloperSigningCredential();

            builder.AddSigningCredential(LoadCertificateFromStore());
            builder.AddAspNetIdentity<ApplicationUser>();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            builder.AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
                    options => options.MigrationsAssembly(migrationsAssembly));
            });

            builder.AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                {
                    builder.UseSqlServer(connectionString,
                        options => options.MigrationsAssembly(migrationsAssembly));
                };
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

            InitializeDatabase(app);

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }

        public X509Certificate2 LoadCertificateFromStore()
        {
            var thumbPrint = "6e4d9b7fc5af7f558a4ec0def862673602fae1d4";
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, true);

                if (certCollection.Count == 0) throw new Exception("The specified certificate wasn't found");
                return certCollection[0];
            }
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                var userContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                userContext.Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                context.Database.Migrate();

                if (!userContext.Users.Any())
                {
                    var user1 = new ApplicationUser
                    {
                        Id = "d860efca-22d9-47fd-8249-791ba61b07c7",
                        UserName = "Frank"
                    };
                    user1.PasswordHash = passwordHasher.HashPassword(user1, "password");

                    var user2 = new ApplicationUser
                    {
                        Id = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                        UserName = "Claire"
                    };
                    

                    user2.PasswordHash = passwordHasher.HashPassword(user2, "password");

                    userManager.CreateAsync(user1).Wait();
                    userManager.CreateAsync(user2).Wait();

                    userManager.AddClaimsAsync(user1, new List<Claim>()
                    {
                        new Claim(JwtClaimTypes.GivenName, "Frank"),
                        new Claim(JwtClaimTypes.FamilyName, "Underwood"),
                        new Claim(JwtClaimTypes.Address, "Main Road 1"),
                        new Claim(JwtClaimTypes.Role, "FreeUser"),
                        new Claim("subscriptionlevel", "FreeUser"),
                        new Claim("country", "nl"),
                    }).Wait();

                    userManager.AddClaimsAsync(user2, new List<Claim>()
                    {
                        new Claim(JwtClaimTypes.GivenName, "Claire"),
                        new Claim(JwtClaimTypes.FamilyName, "password"),
                        new Claim(JwtClaimTypes.Address, "Big Road 2"),
                        new Claim(JwtClaimTypes.Role, "PayingUser"),
                        new Claim("subscriptionlevel", "PayingUser"),
                        new Claim("country", "be"),
                    }).Wait();
                }

                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients) context.Clients.Add(client.ToEntity());
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.Ids) context.IdentityResources.Add(resource.ToEntity());
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.Apis) context.ApiResources.Add(resource.ToEntity());
                    context.SaveChanges();
                }
            }
        }
    }
}