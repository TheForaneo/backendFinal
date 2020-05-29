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
    public class ClienteController:Controller{
        private readonly ClienteService _clienteService;
        private readonly VehiculoService _vehiculoService;
        private readonly TallerService _tallerService;

        public ClienteController(ClienteService clienteService, TallerService tallerService, VehiculoService vehiculoService){
            _clienteService=clienteService;
            _tallerService = tallerService;
            _vehiculoService=vehiculoService;
        }
        
        [HttpGet]
        public ActionResult<List<Cliente>> Get() => _clienteService.Get();
        
        
        [HttpGet("{id:length(24)}", Name="GetCliente")]
        public ActionResult<Cliente> Get(string id){
            var cliente = _clienteService.Get(id);
            if(cliente == null){
                return NotFound();
            }
            return cliente;
        }
        [HttpGet("{correo}", Name="GetID")]
        public ActionResult GetID(string correo){
            string correos =  correo;
            if(!(correos.Equals(null))){
                return Ok(new { id = _clienteService.GetId(correos)});
            }
            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Cliente> Create(Cliente cliente){
            string correo= cliente.correo.ToString();
            var cli = _clienteService.GetCorreo(correo);
            if(cli!=null){
                return BadRequest();
            }
            cliente.role="Cliente";
            _clienteService.Create(cliente);
            return CreatedAtRoute("GetCliente", new {id = cliente.Id.ToString()}, cliente);
        }

        [HttpPut]
        public IActionResult Update(string id, Cliente clienteIn){
            var cliente = _clienteService.Get(id);
            if(cliente == null){
                return NotFound();
            }
            _clienteService.Update(id, clienteIn);
            cliente = _clienteService.Get(id);
            return Ok(cliente);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id){
            var cliente = _clienteService.Get(id);
            if(cliente==null){
                return NotFound();
            }
            List<Vehiculo> lista = _vehiculoService.GetByCliente(cliente.Id);
            _clienteService.Remove(cliente.Id);
            if(lista != null){
                for(int i=0; i<lista.Count; i++){
                _vehiculoService.Remove(lista.ElementAt(i).Id);
            }    
            }
            return NoContent();
        }
    }
    
}