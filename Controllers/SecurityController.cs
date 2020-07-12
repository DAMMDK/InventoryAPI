using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {

        private readonly SecurityService _securityService;
        private readonly ValidationsUserService _validationsUserService;
        private IConfiguration _iConfiguration;
        public SecurityController(SecurityService securityService, 
            ValidationsUserService validationsUserService,
            IConfiguration iConfiguration) 
        {
            _securityService = securityService;
            _validationsUserService = validationsUserService;
            _iConfiguration = iConfiguration;
        }

        [HttpPost]
        [Route("login")] // api/security/login
        public IActionResult Login([FromBody] UserAccess _userAccess)
        {
            try
            {
                var result = _securityService.Login(_userAccess);
                return Ok(result);
            }
            catch (Exception Err)
            {
                return BadRequest(Err + _securityService.Message);
            }
        }

        [HttpGet]
        [Route("findusr")] // api/security/getusr
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult FindUser()
        {
            try
            {
                var result = _securityService.FindUsr();
                return Ok(result);
            }
            catch (Exception Err)
            {
                return BadRequest(Err + _securityService.Message);
            }
        }

        [HttpPost]
        [Route("addusr")] // api/security/addusr
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddUser([FromBody] Users _users) {
            try
            {
                var _email = _users.Email;
                var isValid = _validationsUserService.CheckEmail(_email);
                if (isValid == false)
                {
                    _securityService.AddUser(_users);
                    return Ok(_securityService.Message);
                }
                else
                {
                    return BadRequest(_securityService.Message);
                }
            }
            catch (Exception Err)
            {
                return BadRequest(Err + _securityService.Message);
            }
        }

        [HttpPut]
        [Route("updusr")] // api/security/updusr
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateUser([FromBody] Users _users)
        {
            try
            {
                var _email = _users.OldEmail;
                var isValid = _validationsUserService.CheckEmail(_email);
                if (isValid == true)
                {
                    _securityService.UpdateUser(_users);
                    return Ok(_securityService.Message);
                }
                else
                {
                    return BadRequest(_validationsUserService.Message);
                }
            }
            catch (Exception Err)
            {
                return BadRequest(Err + _validationsUserService.Message);
            }
        }

        [HttpDelete]
        [Route("delusr")] // api/security/delusr/
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteUser(DelUser _delUser)
        {
            try
            {
                var result = _securityService.DeleteUser(_delUser);
                if (result == true)
                {
                    return Ok(_securityService.Message);
                }
                else
                {
                    return BadRequest(_securityService.Message);
                }
            }
            catch (Exception Err)
            {
                return BadRequest(Err + _securityService.Message);
            }
        }
    }
}