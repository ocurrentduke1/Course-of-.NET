using Microsoft.EntityFrameworkCore;
using Usuarios.Infrastructure;

namespace Usuarios.Api;

public static class ApplicationBuilderExtensions
{
    public static async void ApplyMigrations(
        this IApplicationBuilder app
    )
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var Services = scope.ServiceProvider;
            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = Services.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while applying migrations.");
                throw;
            }
        }
    }
}