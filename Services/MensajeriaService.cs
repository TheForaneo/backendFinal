using System.Collections.Generic;
using MongoDB.Driver;
using apiweb.Models;

namespace apiweb.Services{
    public class MensajeriaService{
        private readonly IMongoCollection<Mensajeria> _mensajeria;

        public MensajeriaService(IMensajeriastoreDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _mensajeria = database.GetCollection<Mensajeria>(settings.MensajeriaCollectionName);
        }

        public List<Mensajeria> GetMensajesRecibidosClienteTaller(string idCliente, string idTaller) => _mensajeria.Find<Mensajeria>(mensajeria => mensajeria.Clienteid == idCliente && mensajeria.Tallerid==idTaller).ToList();

        public List<Mensajeria> GetMensajesTaller(string idTaller) => _mensajeria.Find<Mensajeria>(mensajeria => mensajeria.Tallerid.Equals(idTaller)).ToList();
        public List<Mensajeria> GetMensajesCliente(string idCliente) => _mensajeria.Find<Mensajeria>(mensajeria => mensajeria.Clienteid.Equals(idCliente)).ToList();

        public Mensajeria EnviarMensaje(Mensajeria mensaje){
            _mensajeria.InsertOne(mensaje);
            return mensaje;
        }
    }
}