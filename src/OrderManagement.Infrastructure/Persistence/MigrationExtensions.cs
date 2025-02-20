using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace OrderManagement.Infrastructure.Persistence
{
    public static class MigrationExtensions
    {
        public static void ApplyOrderManagementMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using OrderManagementContext workContext = scope.ServiceProvider.GetRequiredService<OrderManagementContext>();

            //workContext.Database.EnsureDeleted();

            workContext.Database.Migrate();
        }
    }
}
