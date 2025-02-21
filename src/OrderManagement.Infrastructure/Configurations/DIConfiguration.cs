using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Contract;
using System.Data;
using OrderManagement.Domain.Orders;
using OrderManagement.Domain.Products;
using OrderManagement.Infrastructure.Domain.Orders;
using OrderManagement.Infrastructure.Domain.Products;

namespace OrderManagement.Infrastructure.Configurations
{
    public static class DIConfiguration
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderManagementContext>((sp, options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Database"));
            });


            services.AddScoped<IDbConnection>(sp =>
            {
                var dbContext = sp.GetRequiredService<OrderManagementContext>();
                var connection = dbContext.Database.GetDbConnection();
               
                return connection;
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(IApplication).Assembly);
            });

            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();


            return services;
        }
    }
}
