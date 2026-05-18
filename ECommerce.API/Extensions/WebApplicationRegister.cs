using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Contracts;
using ECommerce.Persistence.Data.Context;

namespace ECommerce.API.Extensions
{
    public static class WebApplicationRegister
    {
        public static async Task<WebApplication> MigrateDataBaseAsync(this WebApplication app)
        {
            // Create a new DI scope manually because we are outside of an HTTP request pipeline (Program.cs)
            // Services registered as Scoped (like DbContext) require a scope to be resolved correctly
            await using var scope = app.Services.CreateAsyncScope();

            // Resolve StoreDbContext (EF Core DbContext) from the DI container
            var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

            // Check if there are any pending EF Core migrations that have not been applied yet
            // This ensures the database schema is up to date before running the application logic
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                // Apply all pending migrations to the database
                await dbContext.Database.MigrateAsync();
            }
            return app;
        }

        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            // Create a new DI scope manually because we are outside of an HTTP request pipeline (Program.cs)
            // Services registered as Scoped (like DbContext) require a scope to be resolved correctly
            using var scope = app.Services.CreateScope();

            // Resolve IDataInitializer from the DI container within the created scope
            // Inject it manually because no constructor injection in program.cs, and we need to execute its logic during application startup
            var dataInitializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();

            // Execute the database initialization logic (seeding default data, etc.)
            // This is typically used to insert initial data like admin users, roles, or default products
            await dataInitializer.InitializeAsync();

            return app;
        }
    }
}
