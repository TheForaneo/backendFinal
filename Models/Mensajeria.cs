using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace apiweb.Models{
    public class Mensajeria{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string id{get; set;}
        public string Clienteid{get; set;}
        public string clienteNombre{get; set;}
        public string Mensaje{get; set;}
        public string role{get; set;}
        public DateTime FechaEnvio{get;set;}
        public string Tallerid{get; set;}
        public string tallerNombre{get; set;}
    }
}