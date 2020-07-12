using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Models
{
    public class TotalOrders
    {
        public int TransactionID { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public Orders Orders { get; set; }

        public class Mapping
        {
            public Mapping(EntityTypeBuilder<TotalOrders> mappingOrders) 
            {
                mappingOrders.HasKey(obj => obj.TransactionID);
                mappingOrders.ToTable("TotalOrders");
                mappingOrders.HasOne(obj => obj.Orders);
            }
        }
    }
}
