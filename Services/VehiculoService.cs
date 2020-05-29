using apiweb.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;

namespace apiweb.Services{

    public class VehiculoService{
        private readonly IMongoCollection<Vehiculo> _vehiculo;
        private readonly IMongoCollection<Solicitud> _solicitud;
        
        public VehiculoService(IVehiculostoreDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _vehiculo=database.GetCollection<Vehiculo>(settings.VehiculoCollectionName);
        }

        public List<Vehiculo> Get() => _vehiculo.Find<Vehiculo>(vehiculo => true).ToList();

        public List<Vehiculo> GetByCliente(string id) => _vehiculo.Find<Vehiculo>(vehiculo => vehiculo.cliente.Equals(id)).ToList();

        public Vehiculo GetVehiculo(string id) => _vehiculo.Find<Vehiculo>(vehiculo => vehiculo.Id.Equals(id)).FirstOrDefault();

        public Vehiculo GetV(string placa) => _vehiculo.Find<Vehiculo>(vehiculo => vehiculo.placa.Equals(placa)).FirstOrDefault();

        public Vehiculo Create(Vehiculo vehiculo){
            _vehiculo.InsertOne(vehiculo);
            return vehiculo;
        }

        public Vehiculo checkV(string placa) {
            var cont = GetV(placa);
            return cont;
        }
        
        //public void Update(string id, Vehiculo vehiculoIn) => _vehiculo.ReplaceOne(vehiculo => vehiculo.Id.Equals(id), vehiculoIn);
        public void Update(string id, Vehiculo vehiculoIn){
            if(vehiculoIn != null){
                try{
                    if(!(vehiculoIn.placa.Equals(null))){
                        if(!(checkV(vehiculoIn.placa)==null)){
                            _vehiculo.FindOneAndUpdate(vehiculo => vehiculo.Id == id, Builders<Vehiculo>.Update.Set("placa", vehiculoIn.placa));
                        }
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(vehiculoIn.modelo.Equals(null))){
                        _vehiculo.FindOneAndUpdate(vehiculo => vehiculo.Id == id, Builders<Vehiculo>.Update.Set("modelo", vehiculoIn.modelo));
                    }
                }catch(NullReferenceException ex){}
                try{
                    Console.WriteLine(vehiculoIn.año);
                    if(!(vehiculoIn.año==0)){
                        _vehiculo.FindOneAndUpdate(vehiculo => vehiculo.Id == id, Builders<Vehiculo>.Update.Set("año", vehiculoIn.año));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(vehiculoIn.marca.Equals(null))){
                        _vehiculo.FindOneAndUpdate(vehiculo => vehiculo.Id==id, Builders<Vehiculo>.Update.Set("marca", vehiculoIn.marca));
                    }
                }catch(NullReferenceException ex){}
                try{
                    if(!(vehiculoIn.cliente.Equals(null))){
                        _vehiculo.FindOneAndUpdate(vehiculo => vehiculo.Id==id, Builders<Vehiculo>.Update.Set("cliente", vehiculoIn.cliente));
                    }
                }catch(NullReferenceException ex){}
            }
        }
        public void Remove(Vehiculo vehiculoIn) => _vehiculo.DeleteOne(vehiculo => vehiculo.Id == vehiculoIn.Id);

        public void Remove(string id){
            var vehiculo = this.GetVehiculo(id);
            _vehiculo.DeleteOne(vehiculo=> vehiculo.Id==id);
        } 
    }
    


}
