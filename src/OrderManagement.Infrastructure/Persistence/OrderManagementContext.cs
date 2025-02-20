using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Contract;
using OrderManagement.Domain.Orders;
using OrderManagement.Domain.Products;
using OrderManagement.Infrastructure.Domain.Orders;
using OrderManagement.Infrastructure.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Persistence
{
    public class OrderManagementContext : DbContext, IOrderManagementContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }



        public OrderManagementContext(DbContextOptions<OrderManagementContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderManagementContext).Assembly);

            //modelBuilder.ApplyConfiguration(new OrderConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
