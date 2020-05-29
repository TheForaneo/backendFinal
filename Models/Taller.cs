using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace apiweb.Models{
    public class Taller{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id{get; set;}
        public string nombreTaller{get; set;}
        public string correo{get;set;}
        public string telefonoFijo{get;set;}
        public string celular{get;set;}
        public string nombreDueño{get;set;}
        public string calle{get;set;}
        public string numExt{get;set;}
        public string numInt{get;set;}
        public string colonia{get;set;}
        public string latitud{get; set;}
        public string longitud{get; set;}
        public string tipoTaller1{get; set;}
        public string tipoTaller2{get; set;}
        public string tipoTaller3{get; set;}
        public string role{get; set;}
        public string contraseña{get;set;}
        public string codigo{get;set;}
    }
}