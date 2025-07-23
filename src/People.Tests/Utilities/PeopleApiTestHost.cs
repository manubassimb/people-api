using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using People.Data.Context;


namespace People.Tests.Utilities;

public class PeopleApiTestHost : WebApplicationFactory<Program>
{    
    private static readonly string InMemoryDbName = $"TestDb-{Guid.NewGuid()}	";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        { 
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<Context>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
                     
            services.AddDbContext<Context>(options =>
            {
                options.UseInMemoryDatabase(InMemoryDbName);
            });
                        
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            context.Database.EnsureCreated();
        });

        builder.UseEnvironment("Testing");
    }
}