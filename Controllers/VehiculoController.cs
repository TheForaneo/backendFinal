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
using System.Web.Http.Cors;

namespace apiweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     
    //[Authorize]
    public class VehiculoController : Controller{
        private readonly VehiculoService _vehiculoService;
        private readonly SolicitudService _solicitudService;
        public VehiculoController(VehiculoService vehiculoService, SolicitudService solicitudService){
            _vehiculoService=vehiculoService;
            _solicitudService=solicitudService;
        }
        
        [HttpGet]
        public ActionResult<List<Vehiculo>> Get() => _vehiculoService.Get();
        
        [HttpGet("porCliente/{cid}", Name="GetByCliente")]
        public ActionResult<List<Vehiculo>> GetByCliente(string cid) => _vehiculoService.GetByCliente(cid);

        [HttpGet("getVehiculo/{rid:length(24)}", Name="GetVehiculo")]
        public ActionResult<Vehiculo> GetVehiculo(string rid) => _vehiculoService.GetVehiculo(rid);

        [HttpPost]
        public ActionResult<Vehiculo> Create(Vehiculo vehiculo){
            _vehiculoService.Create(vehiculo);
            return CreatedAtRoute("GetVehiculo", new {rid = vehiculo.Id.ToString()}, vehiculo);
        }

        [HttpPut]
        public IActionResult Update(string id, Vehiculo vehiculoIn){
            var vehiculo = _vehiculoService.GetVehiculo(id);
            if(vehiculo == null){
                return NotFound();
            }
            _vehiculoService.Update(id, vehiculoIn);
            vehiculo = _vehiculoService.GetVehiculo(id);
            return Ok(vehiculo);
        }
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id){
            var vehiculo = _vehiculoService.GetVehiculo(id);
            var soli = _solicitudService.GetV(vehiculo.placa);
            if(vehiculo == null){
                return NotFound();
            }
            _vehiculoService.Remove(vehiculo.Id);
            if(soli != null){
                _solicitudService.Remove(soli.Id);
            }
            return Ok();
        }
    }
}