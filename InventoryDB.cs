using Microsoft.EntityFrameworkCore;
using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI
{
    public class InventoryDB : DbContext
    {

        public InventoryDB(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<TypeProduct> TypeProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<TotalOrders> TotalOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelCreator) 
        {
            new Roles.Mapping(modelCreator.Entity<Roles>());
            new Users.Mapping(modelCreator.Entity<Users>());
            new TypeProduct.Mapping(modelCreator.Entity<TypeProduct>());
            new Product.Mapping(modelCreator.Entity<Product>());
            new Orders.Mapping(modelCreator.Entity<Orders>());
            new TotalOrders.Mapping(modelCreator.Entity<TotalOrders>());
        }

    }
}
