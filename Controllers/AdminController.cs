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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace apiweb.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdminController:Controller{
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService){
            _adminService=adminService;
        }

        [HttpGet("{id:length(24)}", Name="GetAdmin")]
        public ActionResult<Admin> Get(string id){
            var admin = _adminService.Get(id);
            if(admin == null){
                return NotFound();
            }
            return admin;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Admin> Create(Admin admin){
            
            var cli = _adminService.GetCorreo(admin.correo);
            if(cli!=null){
                return BadRequest();
            }
            admin.role="Administrador";
            _adminService.Create(admin);
            return CreatedAtRoute("GetAdmin", new {id = admin.Id.ToString()}, admin);
        }
    }
}