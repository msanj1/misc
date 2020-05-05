using System;
using System.Linq;
using AutoMapper;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Services;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace CourseLibrary.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpCacheHeaders(expirationModelOptionsAction =>
            {
                expirationModelOptionsAction.MaxAge = 60;
                expirationModelOptionsAction.CacheLocation = CacheLocation.Private;
            }, validationModelOptions =>
            {
                validationModelOptions.MustRevalidate = true;
            });
            services.AddResponseCaching();
            services.AddControllers(setupAction =>
                {
                    setupAction.ReturnHttpNotAcceptable = true;
                    setupAction.CacheProfiles.Add("240SecondsCacheProfile",
                        new CacheProfile()
                        {
                            Duration = 240
                        });
                })
                .AddNewtonsoftJson(setupAction =>
                {
                    setupAction.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction =>
                    {
                        setupAction.InvalidModelStateResponseFactory = context =>
                        {
                            var problemDetailsFactory = context.HttpContext.RequestServices
                                .GetRequiredService<ProblemDetailsFactory>();
                            var problemDetail = problemDetailsFactory.CreateValidationProblemDetails(
                                context.HttpContext,
                                context.ModelState);
                            problemDetail.Detail = "See the errors for details.";
                            problemDetail.Instance = context.HttpContext.Request.Path;

                            var actionExecutingContext = context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                            if (context.ModelState.ErrorCount > 0 &&
                                actionExecutingContext?.ActionArguments.Count ==
                                context.ActionDescriptor.Parameters.Count)
                            {
                                problemDetail.Type = "https://courselibrary.com/modelvalidationproblem";
                                problemDetail.Status = StatusCodes.Status422UnprocessableEntity;
                                problemDetail.Title = "One or more validation errors occured";

                                return new UnprocessableEntityObjectResult(problemDetail)
                                {
                                    ContentTypes = {"application/problem+json"}
                                };
                            }

                            problemDetail.Status = StatusCodes.Status400BadRequest;
                            problemDetail.Title = "One or more errors on input occured";
                            return new UnprocessableEntityObjectResult(problemDetail)
                            {
                                ContentTypes = {"application/problem+json"}
                            };
                        };
                    }
                );

            services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsonOutputFormatter = config.OutputFormatters
                    .OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

                if (newtonsoftJsonOutputFormatter != null)
                {
                    newtonsoftJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.marvin.hateoas+json");
                }
            });

            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();

            services.AddDbContext<CourseLibraryContext>(options =>
            {
                options.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=CourseLibraryDB;Trusted_Connection=True;");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler(async appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        //log error
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });

            //app.UseResponseCaching();

            app.UseHttpCacheHeaders();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}