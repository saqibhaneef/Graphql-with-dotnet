using Microsoft.EntityFrameworkCore;
using PizzaOrder.Data.Entities;
using PizzaOrder.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrder.Data
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext()
        {
            
        }

        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base(options)
        {
            
        }


        public DbSet<PizzaDetail> PizzaDetails { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
            .Property(o => o.OrderStatus)
            .HasConversion<string>(); // Store enum as string

            modelBuilder.Entity<PizzaDetail>()
                .Property(p => p.Topping)
                .HasConversion<string>(); // Store enum as string

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var fixedDate = new DateTime(2025, 07, 14);

            modelBuilder.Entity<OrderDetail>().HasData(new OrderDetail
            (
                addressLine1: "123 Pizza Street",
                mobileNo: "1234567890",
                amount: 1200
            )
            {
                Id = 1,
                AddressLine2 = "Apt 2B",
                OrderStatus = OrderStatus.InProgress,
                Date = fixedDate
            });

            modelBuilder.Entity<PizzaDetail>().HasData(
                new PizzaDetail
                {
                    Id = 1,
                    Description = "Pepperoni Pizza",
                    Topping = Topping.Sausage,
                    OrderDetailId = 1
                },
                new PizzaDetail
                {
                    Id = 2,
                    Description = "Cheesy Delight",
                    Topping = Topping.ExtraCheese,
                    OrderDetailId = 1
                }
            );
        }
    }
}
