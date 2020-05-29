using System.Collections.Generic;
using System.Linq;
using System;
using apiweb.Models;
using apiweb.Services;
using MongoDB.Driver;
using MongoDB;

namespace apiweb.Services{
    public class ComentarioService{
        private readonly IMongoCollection<Comentario> _comentario;
        
        public ComentarioService (IComentariostoreDatabaseSettings settings){
            var taller = new MongoClient(settings.ConnectionString);
            var database = taller.GetDatabase(settings.DatabaseName);

            _comentario = database.GetCollection<Comentario>(settings.ComentariosCollectionName);
        }

        public List<Comentario> GetporTaller(string idT) => _comentario.Find<Comentario>(comentario => comentario.idTaller.Equals(idT)).ToList();
        public List<Comentario> GetporCliente(string idC) => _comentario.Find<Comentario>(comentario => comentario.idCliente==idC).ToList();
        public Comentario GetComentario(string id) => _comentario.Find<Comentario>(comentario => comentario.Id == id).FirstOrDefault(); 
        public Comentario GetBySolicitud(string sid) => _comentario.Find<Comentario>(comentario => comentario.idSolicitud.Equals(sid)).FirstOrDefault();
        public Comentario Create(Comentario comentario){
            _comentario.InsertOne(comentario);
            return comentario;
        }
        public void Update(string id, Comentario comentarioIn){
            if(comentarioIn != null){
                try{
                    if(!(comentarioIn.comentario.Equals(null))){
                        _comentario.FindOneAndUpdate(comentario => comentario.Id == id, Builders<Comentario>.Update.Set("Comentario", comentarioIn.comentario));
                    }
                }
                catch(NullReferenceException ex){}
            }
        }   
        public void Remove(Comentario comentarioIn) => _comentario.DeleteOne(comentario => comentario.Id==comentarioIn.Id);
        public void Remove(string id) => _comentario.DeleteOne(comentario => comentario.Id==id);
    }
}