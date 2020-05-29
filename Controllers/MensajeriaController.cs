using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using apiweb.Models;
using apiweb.Services;

namespace apiweb.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MensajeriaController : Controller{
        private readonly MensajeriaService _mensajeria;
        private readonly TallerService _taller;   
        private readonly ClienteService _cliente;      
        DateTime date = new DateTime();
        public MensajeriaController(MensajeriaService mensajeria, ClienteService cliente, TallerService taller){
            _mensajeria = mensajeria;
            _cliente=cliente;
            _taller=taller;
        }

        [HttpGet("{idCliente:length(24)}/{idTaller:length(24)}", Name="GetMensajesClienteTaller")]
        public ActionResult<List<Mensajeria>> GetMensajesClienteTaller(string idCliente, string idTaller){
            if(_mensajeria.GetMensajesRecibidosClienteTaller(idCliente, idTaller)==null){
                return NoContent();
            }
            List<Mensajeria> lista = _mensajeria.GetMensajesRecibidosClienteTaller(idCliente, idTaller);
            for(int i=0; i<lista.Count; i++){
                var taller = _taller.Get(idTaller);
                var cliente=_cliente.Get(idCliente);
                var mensaje = lista.ElementAt(i);
                mensaje.clienteNombre=cliente.Nombre;
                mensaje.tallerNombre=taller.nombreTaller;
            } 
            return lista;
        }
        [HttpGet("utlimo/{clienteid:length(24)}/{tallerid:length(24)}", Name="GetLast")]
        public ActionResult<Mensajeria> getLast(string clienteid, string tallerid){
            if(_mensajeria.GetMensajesRecibidosClienteTaller(clienteid, tallerid)==null){
                return NoContent();
            }
            List<Mensajeria> lista = _mensajeria.GetMensajesRecibidosClienteTaller(clienteid, tallerid);
            return lista.LastOrDefault();;
        }
        
        //[Route("[action]/")]
        [HttpGet("{id:length(24)}", Name="GetMensajes")]
        public ActionResult<List<Mensajeria>> GetMensajes(string id){
            List<Mensajeria> clientes= _mensajeria.GetMensajesCliente(id);
            List<Mensajeria> taller = _mensajeria.GetMensajesTaller(id);
            if(clientes.Count != 0){
                for(int i=0; i<clientes.Count; i++){
                    var mensaje = clientes.ElementAt(i);
                    var tallerin=_taller.Get(mensaje.Tallerid);
                    if(tallerin!=null){
                        mensaje.tallerNombre=tallerin.nombreTaller;
                    }
                    else{
                        mensaje.tallerNombre="Usuario eliminado";
                    }
                }
                return clientes;
            }
            if(taller.Count != 0){
                for(int i = 0; i<taller.Count; i++){
                    var mensaje = taller.ElementAt(i);
                    var clientein = _cliente.Get(mensaje.Clienteid);
                    if(clientein!= null){
                        mensaje.clienteNombre=clientein.Nombre;
                    }
                    else{
                        mensaje.clienteNombre="Usuario eliminado";
                    }
                }
                return taller;
            }
            return null;
        }
        public void EnviaraCliente(Mensajeria Mensaje){
            Mensaje.FechaEnvio=DateTime.Now;
            _mensajeria.EnviarMensaje(Mensaje);
        }
    }
}