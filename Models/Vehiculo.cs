using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace apiweb.Models{
    public class Vehiculo{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string placa { get; set;}
        public string modelo { get; set; }
        public int año { get; set; }
        public string marca { get; set; }
        public string cliente { get; set; }
    }
}