using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Library.Application;
using Newtonsoft.Json.Serialization;
using Library.Application.Common.Interfaces;
using Library.Infrastructure;
using NSwag;
using NSwag.Generation.Processors.Security;
using Library.API.Services;


namespace Library.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration; 
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();

            services.AddInfrastructure(Configuration, Environment);

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();


            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true;
            });

            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;

            }).AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            }).ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "https://library.com/modelvalidationproblem",
                        Title = "One or more model validation errors occurred.",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "See the errors property for details.",
                        Instance = context.HttpContext.Request.Path
                    };

                    problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                    return new UnprocessableEntityObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            }).AddXmlDataContractSerializerFormatters();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // register PropertyMappingService
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            // register PropertyCheckerService
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //services.AddSwaggerDocument();

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Library API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });

            }

            //app.UseCustomExceptionHandler();
            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthentication();
            //app.UseIdentityServer();
            app.UseAuthorization();

            app.UseResponseCaching();

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(120)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };


                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
