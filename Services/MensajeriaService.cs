using System.Collections.Generic;
using MongoDB.Driver;
using apiweb.Models;
using System.Linq;

namespace apiweb.Services{
    public class MensajeriaService{
        private readonly IMongoCollection<Mensajeria> _mensajeria;
        private readonly IMongoCollection<Taller> _taller;
        private readonly IMongoCollection<Cliente> _cliente;

        public MensajeriaService(IClientestoreDatabaseSettings csettings,IMensajeriastoreDatabaseSettings settings, ITallerstoreDatabaseSettings tsettings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _mensajeria = database.GetCollection<Mensajeria>(settings.MensajeriaCollectionName);
            _taller = database.GetCollection<Taller>(tsettings.TallerCollectionName);
            _cliente=database.GetCollection<Cliente>(csettings.ClienteCollectionName);
        }

        public List<Mensajeria> GetMensajesRecibidosClienteTaller(string idCliente, string idTaller) => _mensajeria.Find<Mensajeria>(mensajeria => mensajeria.Clienteid == idCliente && mensajeria.Tallerid==idTaller).ToList();
        public List<Mensajeria> GetMensajesTaller(string idTaller) => _mensajeria.Find<Mensajeria>(mensajeria => mensajeria.Tallerid.Equals(idTaller)).ToList();
        public List<Mensajeria> GetMensajesCliente(string idCliente) => _mensajeria.Find<Mensajeria>(mensajeria => mensajeria.Clienteid.Equals(idCliente)).ToList();

        public List<Taller> tallerHablado(string idCliente){
            List<Taller> contactos=new List<Taller>();
            var mensajes = GetMensajesCliente(idCliente);
            for(int i=0; i<mensajes.Count; i++){
                var contacto = mensajes.ElementAt(i);
                var tallerin=_taller.Find<Taller>(taller => taller.Id.Equals(contacto.Tallerid)).FirstOrDefault();
                if(tallerin!=null){
                    contactos.Add(tallerin);
                }
            }
            return contactos;
        }

        public List<Cliente> clienteHablado(string idTaller){
            List<Cliente> contactos=new List<Cliente>();
            var mensajes = GetMensajesTaller(idTaller);
            for(int i=0; i<mensajes.Count; i++){
                var contacto = mensajes.ElementAt(i);
                var clientein=_cliente.Find<Cliente>(cliente => cliente.Id.Equals(contacto.Clienteid)).FirstOrDefault();
                if(clientein!=null){
                    contactos.Add(clientein);
                }
            }
            return contactos;
        }
        public Mensajeria EnviarMensaje(Mensajeria mensaje){
            _mensajeria.InsertOne(mensaje);
            return mensaje;
        }
    }
}