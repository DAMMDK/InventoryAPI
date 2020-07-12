using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Models
{
    public class UserAccess
    {
        public string PWD { get; set; }
        public string Email { get; set; }
    }

    public class DelUser
    {
        public int UserID { get; set; }
        public int DelUserID { get; set; }
        public string Status { get; set; }
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime Birtday { get; set; }
        public int Age { get; set; }
        public int IdRole { get; set; }
        public string Email { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }
        public string Status { get; set; }
    }
}
