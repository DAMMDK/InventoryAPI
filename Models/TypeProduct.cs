using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Models
{
    public class TypeProduct
    {
        public int TypeID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }

        public class Mapping {
            public Mapping(EntityTypeBuilder<TypeProduct> mappingTypeProduct) 
            {
                mappingTypeProduct.HasKey(obj => obj.TypeID);
                mappingTypeProduct.ToTable("TypeProduct");
            }
        }

    }
}
