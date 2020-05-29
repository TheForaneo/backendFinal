using apiweb.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;

namespace apiweb.Services{
    public class SolicitudService{
        private readonly IMongoCollection<Solicitud> _solicitud;

        public SolicitudService(ISolicitudstoreDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _solicitud = database.GetCollection<Solicitud>(settings.SolicitudCollectionName);
        }

        public List<Solicitud> Get() => _solicitud.Find<Solicitud>(solicitud => true ).ToList();

        public Solicitud GetS(string id) => _solicitud.Find<Solicitud>(solicitud => solicitud.Id.Equals(id)).FirstOrDefault();
        public Solicitud GetV(string placa) => _solicitud.Find<Solicitud>(solicitud => solicitud.placa.Equals(placa)).FirstOrDefault();
        public List<Solicitud> GetSolicitudesByCliente(string clienteid) => _solicitud.Find<Solicitud>(solicitud => solicitud.claveCliente.Equals(clienteid)).ToList();
        public List<Solicitud> GetSolicitudesByClienteRecientes(string clienteid){
            List<Solicitud> lista = this.GetSolicitudesByCliente(clienteid);
            List<Solicitud> lista2=null;
            for(int i=0; i<lista.Count; i++){
                if(!(lista.ElementAt(i).estado.Equals("Finalizado"))){
                    lista2.Add(lista.ElementAt(i));
                }
            }
            return lista2;
        }
        public List<Solicitud> GetSolicitudesByClienteFinalizadas(string clienteid){
            List<Solicitud> lista = this.GetSolicitudesByCliente(clienteid);
            List<Solicitud> lista2=null;
            for(int i=0; i<lista.Count; i++){
                if((lista.ElementAt(i).estado.Equals("Finalizado"))){
                    lista2.Add(lista.ElementAt(i));
                }
            }
            return lista2;
        }
        public Solicitud Create(Solicitud solicitud){
            _solicitud.InsertOne(solicitud);
            return solicitud;
        } 
        //public void Update(string id, Solicitud solicitudIn) => _solicitud.ReplaceOne(solicitud => solicitud.Id == id, solicitudIn);

        public void Update(string id, Solicitud solicitudIn){
            if(solicitudIn!=null){
                try{
                    if(!(solicitudIn.claveCliente.Equals(null))){
                        _solicitud.FindOneAndUpdate(solicitud => solicitud.Id == id, Builders<Solicitud>.Update.Set("claveCliente", solicitudIn.claveCliente));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(solicitudIn.placa.Equals(null))){
                        _solicitud.FindOneAndUpdate(solicitud => solicitud.Id == id, Builders<Solicitud>.Update.Set("placa", solicitudIn.placa));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(solicitudIn.descripcionProblema.Equals(null))){
                        _solicitud.FindOneAndUpdate(solicitud => solicitud.Id == id, Builders<Solicitud>.Update.Set("descripcionProblema", solicitudIn.descripcionProblema));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(solicitudIn.tiempoProblema.Equals(null))){
                        _solicitud.FindOneAndUpdate(solicitud => solicitud.Id == id, Builders<Solicitud>.Update.Set("tiempoProblema", solicitudIn.tiempoProblema));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(solicitudIn.entrada.Equals(null))){
                        _solicitud.FindOneAndUpdate(solicitud => solicitud.Id == id, Builders<Solicitud>.Update.Set("fechaProgramada", solicitudIn.entrada));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(solicitudIn.salida.Equals(null))){
                        _solicitud.FindOneAndUpdate(solicitud => solicitud.Id==id, Builders<Solicitud>.Update.Set("fechaSalida", solicitudIn.salida));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(solicitudIn.formaPago.Equals(null))){
                        _solicitud.FindOneAndUpdate(solicitud => solicitud.Id == id, Builders<Solicitud>.Update.Set("formaPago", solicitudIn.formaPago)); 
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(solicitudIn.montoEstimado==0)){
                        _solicitud.FindOneAndUpdate(solicitud => solicitud.Id == id, Builders<Solicitud>.Update.Set("montoEstimado", solicitudIn.montoEstimado)); 
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(solicitudIn.estado.Equals(null))){
                        _solicitud.FindOneAndUpdate(solicitud => solicitud.Id == id, Builders<Solicitud>.Update.Set("estado", solicitudIn.estado)); 
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(solicitudIn.tallerId.Equals(null))){
                        _solicitud.FindOneAndUpdate(solicitud => solicitud.Id == id, Builders<Solicitud>.Update.Set("tallerId", solicitudIn.tallerId)); 
                    }
                }catch(NullReferenceException ex){}
            }
        }

        public void Remove(Solicitud solicitudIn) => _solicitud.DeleteOne(solicitud => solicitud.Id== solicitudIn.Id);

        public void Remove(string id) => _solicitud.DeleteOne(solicitud => solicitud.Id == id);
    }

}