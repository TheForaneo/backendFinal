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
    public class ComentariosController:Controller{
        private readonly ComentarioService _comentarios;
        private readonly TallerService _taller;
        private readonly ClienteService _cliente;
        private readonly SolicitudService _solicitud;

        public ComentariosController(ComentarioService comentarioService, TallerService taller, ClienteService cliente, SolicitudService solicitud){
            _comentarios=comentarioService;
            _taller=taller;
            _cliente=cliente;
            _solicitud=solicitud;
        }
        
        [HttpGet("{id}", Name="GetCT")]
        public ActionResult<List<Comentario>> GetCT(string id){
            return _comentarios.GetporTaller(id);
        }
        [HttpGet("comentarioTaller/{idTaller}", Name="comentariosPorTaller")]
        public ActionResult<List<Comentario>> getPorTaller(string idTaller){
            return _comentarios.GetporTaller(idTaller);
        }
        [HttpGet("Comentario/{idComentario}", Name="GetComentario")]
        public ActionResult<Comentario> GetComentario(string idComentario){
            var comentario = _comentarios.GetComentario(idComentario);
            var solicitud = _solicitud.GetS(comentario.idSolicitud);
            if(solicitud == null){
                return Ok(new {comentario});
            }
            return Ok((new {comentario, solicitud}));
        } 
        
        [HttpPost]
        public ActionResult<Comentario> Create(Comentario comentario){
            DateTime dateOnly = DateTime.Now;
            var date1=dateOnly.Date;
            comentario.fecha = date1.ToString("d");
            var taller= _taller.Get(comentario.idTaller);
            var cliente = _cliente.Get(comentario.idCliente);
            comentario.nombreTaller=taller.nombreTaller;
            comentario.nombreCliente=cliente.Nombre+" "+cliente.apellidop;
            _comentarios.Create(comentario);
             return CreatedAtRoute("GetComentario", new {idComentario = comentario.Id.ToString()}, comentario);
        }

        [HttpPut]
        public IActionResult Update(string id, Comentario comentario){
            var exist = _comentarios.GetComentario(id);
            if(exist == null){
                return NotFound();
            }
            _comentarios.Update(id, comentario);
            return Ok();
        }
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id){
            var exist = _comentarios.GetComentario(id);
            if(exist==null){
                return NotFound();
            }
            _comentarios.Remove(exist.Id);
            return NoContent();
        }
    }
    
}