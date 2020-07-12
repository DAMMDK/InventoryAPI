using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Models
{
    public class Roles
    {
        public int IdRole { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public class Mapping
        {
            public Mapping(EntityTypeBuilder<Roles> mappingRoles) 
            {
                mappingRoles.HasKey(obj => obj.IdRole);
                mappingRoles.ToTable("Roles");

            }
        }
    }
}
