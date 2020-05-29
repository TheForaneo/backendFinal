using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;  
using MongoDB.Bson;  
using apiweb.Models;
using apiweb.Services;
using Microsoft.AspNetCore.Authorization;

namespace apiweb.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class RestartPasswordController : Controller{
        private readonly ClienteService _clienteService;
        public RestartPasswordController(ClienteService clienteService){
            _clienteService=clienteService;
        }
        
        [HttpPost]
        public ActionResult Reset(RestartPassword RpModel){
            var user = _clienteService.GetCorreo(RpModel.Email);
            if(user != null){
                if(user.codigo.Equals(RpModel.Code)){
                    if(_clienteService.changePassword(user, RpModel.ConfirmPassword)){
                        _clienteService.insertCodigo(user, "");
                        return Ok();
                    }
                }
                return NoContent();
            }
            return BadRequest();
        }
    }
}