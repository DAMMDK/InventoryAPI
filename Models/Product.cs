using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Code { get; set; }
        public int Stock { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime Removed { get; }
        public string Available { get; }
        public string Status { get; set; }
        public Users User { get; set; }

        public class Mapping
        {
            // Install dependencies Relational in console
            // Install-Package Microsoft.EntityFrameworkCore.Relational -Version 3.1.5
            public Mapping(EntityTypeBuilder<Product> mappingProduct)
            {
                mappingProduct.HasKey(obj => obj.ProductID);
                mappingProduct.ToTable("Product");
                mappingProduct.HasOne(obj => obj.User);
            }
        }
    }
}
