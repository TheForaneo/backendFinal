using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace apiweb.Models{
    public class Comentario{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id{get; private set;}
        public string idTaller{get; set;}
        public string nombreTaller{get; set;}
        [BsonElement("Comentario")]
        public string comentario{get; set;}
        public int calificacion{get; set;}
        public string fecha{get; set;}
        public string idCliente{get; set;}
        public string nombreCliente{get; set;}
        public string idSolicitud{get; set;}
    }
}