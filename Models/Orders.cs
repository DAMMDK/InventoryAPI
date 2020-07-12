using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Models
{
    public class Orders
    {
        public int OrderID { get; set; }
        public int TransactionID { get; set; }
        public int ProductID { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime GenerateDate { get; set; }
        public string Status { get; set; }
        public Users Users { get; set; }
        public Product Product { get; set; }

        public class Mapping 
        {
            public Mapping(EntityTypeBuilder<Orders> mappingOrders) {
                mappingOrders.HasKey(obj => obj.OrderID);
                mappingOrders.ToTable("Orders");
                mappingOrders.HasOne(obj => obj.Users);
                mappingOrders.HasOne(obj => obj.Product);
            }
        }
    }
}
