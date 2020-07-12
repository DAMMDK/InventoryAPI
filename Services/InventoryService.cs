using InventoryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Services
{
    public class InventoryService
    {
        public string Message { get; set; }

        private readonly InventoryDB _inventoryDb;
        private readonly ValidationsUserService _validationsUserService;
        public InventoryService(InventoryDB inventoryDb, ValidationsUserService validationsUserService)
        {
            _inventoryDb = inventoryDb;
            _validationsUserService = validationsUserService;
        }

        public List<Product> FindProduct()
        {
            try
            {
                var result = _inventoryDb.Product.Where(find => find.Status == "A").OrderBy(O => O.ProductID).ToList();
                Trace.WriteLine("Find product Ok");
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

        public Boolean AddProduct(Product _product) 
        {
            var userID = _product.UserID;
            if (_validationsUserService.CheckRole(userID))
            {
                try
                {
                    var MaxID = ((_inventoryDb.Product.Max(obj => obj.ProductID)) + 1).ToString();
                    var code = _product.Code;
                    var SKU = ("0000000000" + (MaxID) + code);
                    var SKULength = 10;
                    _product.SKU = SKU.Substring(SKU.Length - SKULength);

                    DateTime now = DateTime.Now;
                    //_product.DateStart = now;
                    _product.DateUpdate = now;
                    _product.Status = "A";

                    _inventoryDb.Product.Add(_product);
                    _inventoryDb.SaveChanges();
                    Message = "Producto agregado correctamente";
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

        public Boolean UpdateProduct(Product _product)
        {
            var userID = _product.UserID;
            if (_validationsUserService.CheckRole(userID))
            {
                try
                {
                    var productDb = _inventoryDb.Product.Where(find => find.ProductID == _product.ProductID).FirstOrDefault();
                    var code = _product.Code;
                    var id = productDb.ProductID.ToString();
                    var SKU = ("0000000000" + id + code);
                    var SKULength = 10;
                    if (productDb.Status == "A")
                    {

                        productDb.UserID = _product.UserID;
                        productDb.Name = _product.Name;
                        productDb.Code = code;
                        productDb.Stock = _product.Stock;
                        productDb.SKU = SKU.Substring(SKU.Length - SKULength);
                        productDb.Description = _product.Description;

                        DateTime now = DateTime.Now;
                        productDb.DateUpdate = now;
                        _inventoryDb.SaveChanges();
                        Message = "Producto actualizado correctamente";
                    }
                    else
                    {
                        Message = "El producto ya esta eliminado";
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

        public Boolean DeleteProduct(int userID, int productID)
        {
            if (_validationsUserService.CheckRole(userID))
            {
                try
                {
                    var productDb = _inventoryDb.Product.Where(find => find.ProductID == productID).FirstOrDefault();
                    if (productDb.Status == "A")
                    {
                        productDb.Status = "R";
                        _inventoryDb.SaveChanges();
                        Message = "Producto eliminado correctamente";
                        return true;
                    }
                    else
                    {
                        Message = "El producto ya esta eliminado";
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
            else
            {
                Message = "Lo sentimos, su usuario no tiene privilegios para esta acción";
                return false;
            }
        }
    }
}
