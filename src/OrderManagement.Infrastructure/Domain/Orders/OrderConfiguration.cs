using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Domain.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.CreatedOn).IsRequired();

            builder.Property(o => o.CustomerFullName).IsRequired().HasMaxLength(100);   

            builder.Property(o => o.CustomerPhone).IsRequired().HasMaxLength(15);

            builder.OwnsMany(o => o.OrderProducts, a =>
            {
                a.WithOwner(op => op.Order)
                 .HasForeignKey(op => op.OrderId);

                a.Property(op => op.Id).ValueGeneratedOnAdd();

                a.HasKey(op => op.Id);

                a.Property(op => op.Amount).IsRequired();

                a.Property(op => op.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
            });
        }
    }
}
