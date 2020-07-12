using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Models
{
    public class Users
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public DateTime Birtday { get; set; }
        public int Age { get; }
        [ForeignKey("IdRole")]
        public int IdRole { get; set; }
        public string OldPWD { get; set; }
        public string PWD { get; set; }
        public string OldEmail { get; set; }
        public string Email { get; set; }  
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }
        public string Status { get; set; }

        public class Mapping 
        {
            public Mapping(EntityTypeBuilder<Users> mappingUsers) 
            {
                mappingUsers.HasKey(obj => obj.UserID);
                mappingUsers.ToTable("Users");
            }
        }
    }

}
