﻿using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Store.Omda.Core.Entities;
using Store.Omda.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Repository.Data.Contexts
{

    public class StoreDbContext :DbContext
    {

        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> Types { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<Core.Entities.Order.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


    }
}
