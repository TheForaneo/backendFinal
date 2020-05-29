using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace apiweb.Models{
    public class Solicitud{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id{get; set;}
        public string claveCliente{get;set;}
        public string placa{get;set;}
        public string modeloVehiculo{get; set;}
        public string descripcionProblema{get;set;}
        public string tiempoProblema{get;set;}

        [BsonElement("fechaRealizacion")]
        public string creacionSolicitid{get; set;}

        [BsonElement("fechaProgramada")]
        public string entrada{get; set;}

        [BsonElement("fechaSalida")]
        public string salida{get; set;}
        public string formaPago{get; set;}
        public Double montoEstimado{get; set;}
        public string estado{get; set;}
        public string nombreTaller{get; set;}
        public Comentario comentario{get; set;}
        public string tallerId{get;set;}
    }
}