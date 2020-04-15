using Library.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Library.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Identity;
using Library.Infrastructure.Identity;

namespace Library.FunctionalTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) 
        {
            builder
                .ConfigureServices(async services => {
                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    // Add a database context (AppDbContext) using an in-memory
                    // database for testing.
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("LibraryDbTesting");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    //services.AddScoped<IDomainEventDispatcher, NoOpDomainEventDispatcher>();

                    // Build the service provider.
                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    // context (AppDbContext).
                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var context = scopedServices.GetRequiredService<ApplicationDbContext>();

                        var logger = scopedServices
                            .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                        // Ensure the database is created.
                        context.Database.EnsureCreated();

                        var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

                        try
                        {
                            await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager);
                            // Seed the database with test data.
                            ApplicationDbContextSeed.SeedSampleDataAsync(context);

                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding the " +
                                $"database with test messages. Error: {ex.Message}");
                        }
                    }



                });
        }
    }
}
