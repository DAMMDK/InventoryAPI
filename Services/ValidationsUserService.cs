using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Services
{

    public class ValidationsUserService
    { 
        public string Message { get; set; }

        private readonly InventoryDB _inventoryDb;
        public ValidationsUserService(InventoryDB inventoryDb)
        {
            _inventoryDb = inventoryDb;
        }

        public Boolean CheckRole(int _userID)
        {
            try
            {
                var result = _inventoryDb.Users.Where(find => find.UserID == _userID).DefaultIfEmpty().First();
                var Role = result.IdRole;
                var isValid = _inventoryDb.Roles.Where(find => find.IdRole == Role).DefaultIfEmpty().First();
                if (isValid.IdRole == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception Err)
            {
                Trace.WriteLine("The error is: " + Err);
                Console.WriteLine(Err.InnerException.Message);
                Message = "Opps, ocurrido un error, intente nuevamente o más tarde";
                return false;
            }
        }

        public Boolean CheckEmail(string _email)
        {
            try
            {
                var result = _inventoryDb.Users.Where(find => find.Email == _email).DefaultIfEmpty().First();
                var Email = "";
                if (result == null)
                {
                    Email = "";
                }
                else
                {
                    Email = result.Email;
                }

                if (Email != "")
                {
                    Message = "El email ingresado ya existe.";
                    Trace.WriteLine("The event is: " + Message);
                    return true;
                }
                else
                {
                    Message = "El email no existe.";
                    Trace.WriteLine("The event is: " + Message);
                    return false;
                }

            }
            catch (Exception Err)
            {
                Trace.WriteLine("The error is: " + Err);
                Console.WriteLine(Err.InnerException.Message);
                Message = "Opps, ocurrido un error, intente nuevamente o más tarde.";
                return false;
            }
        }
    }
}
