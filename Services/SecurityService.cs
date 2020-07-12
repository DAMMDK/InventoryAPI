using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAPI.Services
{
    public class SecurityService
    {
        public string Message { get; set; }

        private readonly InventoryDB _inventoryDb;
        private readonly ValidationsUserService _validationsUserService;
        public SecurityService(InventoryDB inventoryDb, ValidationsUserService validationsUserService) 
        {
            _inventoryDb = inventoryDb;
            _validationsUserService = validationsUserService;
        }

        public Boolean Login(UserAccess _userAccess)
        {
            try
            {
                var Email   = _userAccess.Email;
                var PWD     = _userAccess.PWD;
                // Convierte el string en SHA256
                PWD = EncodeSecurityService.EncodeTo(PWD);
                var result = _inventoryDb.Users.Where(find => find.Status == "A" && find.Email == Email && find.PWD == PWD).DefaultIfEmpty();

                Message = "Usuario encontrado!!";
                Trace.WriteLine(Message);
                return true;
            }
            catch (Exception Err)
            {

                Trace.WriteLine("The error is: " + Err);
                Console.WriteLine(Err.InnerException.Message);
                Message = "Opps, ocurrido un error, intente nuevamente o más tarde";
                throw new Exception(Err.Message);
            }
        }

        public List<UserInfo> FindUsr()
        {
            try
            {
                var result = _inventoryDb.Users.Select(U => new UserInfo() { 
                UserId      = U.UserID,
                Name        = U.Name,
                Birtday     = U.Birtday,
                Age         = U.Age,
                IdRole      = U.IdRole,
                Email       = U.Email,
                DateStart   = U.DateStart,
                DateUpdate  = U.DateUpdate,
                Status      = U.Status
                }).Where(find => find.Status == "A").OrderBy(O => O.UserId).DefaultIfEmpty().ToList();

                Trace.WriteLine("Find product Ok");
                Message = "Listado de usuarios listo!!";
                return result;
            }
            catch (Exception Err)
            {

                Trace.WriteLine("The error is: " + Err);
                Console.WriteLine(Err.InnerException.Message);
                Message = "Opps, ocurrido un error, intente nuevamente o más tarde";
                throw new Exception(Err.Message);
            }
        }

        public Boolean AddUser(Users _users) 
        {
            try 
            {
                // Convierte el string en SHA256
                var _pdw    = _users.PWD;
                _pdw        = EncodeSecurityService.EncodeTo(_pdw);
                _users.PWD  = _pdw;

                DateTime now = DateTime.Now;
                _users.DateStart    = now;
                _users.DateUpdate   = now;
                _users.Status       = "A";

                _inventoryDb.Users.Add(_users);
                _inventoryDb.SaveChanges();
                Message = "Usuario creado exitosamente!!";
                return true;
            }
            catch (Exception Err)
            {
                Trace.WriteLine("The error is: " + Err);
                Console.WriteLine(Err.InnerException.Message);
                Message = "Opps, ocurrido un error, intente nuevamente o más tarde.";
                return false;
            }
        }

        public Boolean UpdateUser(Users _users)
        {
            var userID      = _users.UserID;
            var _userDb     = _inventoryDb.Users.Where(find => find.UserID == userID).DefaultIfEmpty().First();
            var _oldPdw     = _users.OldPWD;
            var _newPdw     = _users.PWD;
            var _oldEmail   = _users.OldEmail;
            var _newEmail   = _users.Email;

            // Convierte el string en SHA256
            _oldPdw = EncodeSecurityService.EncodeTo(_oldPdw);
            _newPdw = EncodeSecurityService.EncodeTo(_newPdw);

            if (_userDb.PWD == _oldPdw && (_userDb.Email != _oldEmail || _userDb.PWD != _newPdw))
            {
                try
                {
                    _userDb.Name        = _users.Name;
                    _userDb.Birtday     = _users.Birtday;
                    _userDb.IdRole      = _users.IdRole;
                    _userDb.OldPWD      = _oldPdw;          //viejo PWD
                    _userDb.PWD         = _newPdw;          //nuevo PWD
                    _userDb.OldEmail    = _users.OldEmail;  //viejo Email
                    _userDb.Email       = _users.Email;     //nuevo Email
                    DateTime now        = DateTime.Now;
                    //_userDb.DateStart = _userDb.DateStart;
                    _userDb.DateUpdate  = now;
                    _userDb.Status      = "A";

                    _inventoryDb.SaveChanges();
                    _inventoryDb.SaveChanges();
                    Message = "Usuario actualizado exitosamente!!";
                    return true;
                }
                catch (Exception Err)
                {
                    Trace.WriteLine("The error is: " + Err);
                    Console.WriteLine(Err.InnerException.Message);
                    Message = "Opps, ocurrido un error, intente nuevamente o más tarde.";
                    return false;
                }
            }
            else
            {
                Message = "Contraseña incorrecta/Email y contraseña identicos a los actuales.";
                return false;
            }
        }

        public Boolean DeleteUser(DelUser _delUser)
        {
            var userID      = _delUser.UserID;
            var delUserID   = _delUser.DelUserID;
            var Status      = _delUser.Status;
            if (_validationsUserService.CheckRole(_delUser.UserID))
            {
                try
                {
                    var userDb = _inventoryDb.Users.Where(find => find.UserID == delUserID).DefaultIfEmpty().First();

                    if (userDb.Status == "A" && userDb.Status == Status) {
                        Message = "El usuario ya esta habilitado";
                        return false;
                    }
                    else if (userDb.Status == "R" && userDb.Status == Status)
                    {
                        Message = "El usuario ya esta eliminado";
                        return false;
                    }

                    userDb.Status = Status;
                    _inventoryDb.SaveChanges();

                    if (Status == "A") 
                    {
                        Message = "Usuario habilitado correctamente";
                    }
                    else if (Status == "R")
                    {
                        Message = "Usuario eliminado correctamente";
                    }

                    return true;

                }
                catch (Exception Err)
                {
                    Trace.WriteLine("The error is: " + Err);
                    Console.WriteLine(Err.InnerException.Message);
                    Message = "Opps, ocurrido un error, intente nuevamente o más tarde";
                    return false;
                }
            }
            else
            {
                Message = "Lo sentimos, su usuario no tiene privilegios para esta acción";
                return false;
            }
        }
    }
}
