using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;
        public InventoryController(InventoryService inventoryService) 
        {
            _inventoryService = inventoryService;
        }

        [HttpGet] //Api/inventory/find
        [Route("find")]
        public IActionResult Find() 
        {
            try
            {
                var result = _inventoryService.FindProduct();
                return Ok(result);
            }
            catch(Exception Err)
            {
                return BadRequest(Err + _inventoryService.Message);
            }
        }

        [HttpPost] //Api/inventory/add
        [Route("add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Add([FromBody] Product _product)
        {
            try
            {
                var result = _inventoryService.AddProduct(_product);
                if (result == true)
                {
                    return Ok(_inventoryService.Message);
                }
                else
                {
                    return BadRequest(_inventoryService.Message);
                }
            }
            catch (Exception Err)
            {
                return BadRequest(Err + _inventoryService.Message);
            }
        }

        [HttpPut] //Api/inventory/update
        [Route("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update([FromBody] Product _product)
        {
            try
            {
                var result = _inventoryService.UpdateProduct(_product);
                if (result == true)
                {
                    return Ok(_inventoryService.Message);
                }
                else
                {
                    return BadRequest(_inventoryService.Message);
                }
            }
            catch (Exception Err)
            {
                return BadRequest(Err + _inventoryService.Message);
            }
        }

        [HttpDelete] //Api/inventory/delete/{ProductID}
        [Route("delete/{userID}/{ProductID}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int UserID, int ProductID)
        {
            try
            {
                var result = _inventoryService.DeleteProduct(UserID, ProductID);
                if (result == true)
                {
                    return Ok(_inventoryService.Message);
                }
                else
                {
                    return BadRequest(_inventoryService.Message);
                }
            }
            catch (Exception Err)
            {
                return BadRequest(Err + _inventoryService.Message);
            }
        }
    }
}