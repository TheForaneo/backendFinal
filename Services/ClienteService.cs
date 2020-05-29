using apiweb.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using MongoDB;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using MongoDB.Driver.Core;
using MongoDB.Driver;

namespace apiweb.Services{
    public class ClienteService{
        private readonly IMongoCollection<Cliente> _cliente;

        public ClienteService(IClientestoreDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _cliente = database.GetCollection<Cliente>(settings.ClienteCollectionName);
        }

        public List<Cliente> Get() => _cliente.Find<Cliente>(cliente => true).ToList();

        public Cliente Get(string id) => _cliente.Find<Cliente>(cliente => cliente.Id == id).FirstOrDefault();

        public Cliente GetCorreo(string email) => _cliente.Find<Cliente>(cliente => cliente.correo.Equals(email)).FirstOrDefault();

        public Cliente GetCelular(string celular) => _cliente.Find<Cliente>(cliente => cliente.celular == celular).FirstOrDefault();

        public List<Cliente> GetC(string correo) => _cliente.Find<Cliente>(cliente => cliente.correo.Equals(correo)).ToList();

        public List<Cliente> GetCel(string celular) => _cliente.Find<Cliente>(cliente => cliente.celular.Equals(celular)).ToList();

        public Cliente Create(Cliente cliente){
            _cliente.InsertOne(cliente);
            return cliente;
        } 
        public string GetId(string correo){
            var cliente = this.GetCorreo(correo);
            if(cliente != null){
                return cliente.Id.ToString();
            }   
            return null;
        }
        public Cliente iniciaSesionEmail(UserEmailLogin cli){
            var cliente = GetCorreo(cli.Email);
            if(cliente != null){
                if(cliente.contraseña.Equals(cli.Password)){
                    return cliente;
                }
            }
            return null;
        }
        public Cliente iniciaSesionCell(UserCellLogin cli){
            var cliente = GetCelular(cli.Cellphone);
            if(cliente != null){
                if(cliente.contraseña.Equals(cli.Password)){
                    return cliente;
                }
            }
            return null;
        }
        public Boolean insertCodigo(Cliente cli, string codigo){
            var user = GetCorreo(cli.correo);
            if(user!=null){
                if(!(codigo.Equals(null))){
                    _cliente.FindOneAndUpdate(cliente => cliente.correo.Equals(cli.correo), Builders<Cliente>.Update.Set("codigo", codigo));
                    return true;
                }
            }
            return false;
        }
        public Boolean changePassword(Cliente cli, string newPassword){
            if(cli != null){
                if(!(newPassword.Equals(null))){
                    _cliente.FindOneAndUpdate(cliente => cliente.correo.Equals(cli.correo), Builders<Cliente>.Update.Set("contraseña", newPassword));
                    return true;
                }
            }
            return false;
        }
        public Boolean correoExist(string correo){
            int cont = GetC(correo).Count();
            if(cont >= 1){
                return true;
            }
            return false;
        }
        public Boolean celularExist(string celular){
            int cont = GetC(celular).Count();
            if(cont >= 1){
                return true;
            }
            return false;
        }
        //public void Update(string id, Cliente clienteIn) => _cliente.UpdateOne(cliente=>cliente.Id==id, Builders<Cliente>.Update.Set("apellidop", clienteIn.apellidop).Set("apellidom",clienteIn.apellidom).Set("nombre",clienteIn.Nombre).Set("calle",clienteIn.calle).Set("numCasa",clienteIn.numCasa).Set("colonia",clienteIn.colonia).Set("celular", clienteIn.celular).Set("correo",clienteIn.correo));
        public void Update(String id, Cliente clienteIn){
            if(clienteIn!=null){
                try{
                    if(!(clienteIn.apellidop.Equals(null))){
                        _cliente.FindOneAndUpdate(cliente => cliente.Id == id, Builders<Cliente>.Update.Set("apellidop", clienteIn.apellidop));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!(clienteIn.apellidom.Equals(null))){
                        _cliente.FindOneAndUpdate(cliente => cliente.Id == id, Builders<Cliente>.Update.Set("apellidom",clienteIn.apellidom));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!clienteIn.Nombre.Equals(null)){
                        _cliente.FindOneAndUpdate(cliente => cliente.Id == id, Builders<Cliente>.Update.Set("nombre", clienteIn.Nombre));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!clienteIn.calle.Equals(null)){
                        _cliente.FindOneAndUpdate(cliente => cliente.Id == id, Builders<Cliente>.Update.Set("calle", clienteIn.calle));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!clienteIn.numCasa.Equals(null)){
                        _cliente.FindOneAndUpdate(cliente => cliente.Id == id, Builders<Cliente>.Update.Set("numCasa", clienteIn.numCasa));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!clienteIn.colonia.Equals(null)){
                        _cliente.FindOneAndUpdate(cliente => cliente.Id == id, Builders<Cliente>.Update.Set("colonia", clienteIn.colonia));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!clienteIn.celular.Equals(null)){
                        _cliente.FindOneAndUpdate(cliente => cliente.Id == id, Builders<Cliente>.Update.Set("celular", clienteIn.celular));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!clienteIn.correo.Equals(null)){
                        _cliente.FindOneAndUpdate(cliente => cliente.Id == id, Builders<Cliente>.Update.Set("correo", clienteIn.correo));
                    }
                }
                catch(NullReferenceException ex){}
            }
        }
        public void Remove(Cliente clienteIn) => _cliente.DeleteOne(cliente => cliente.Id== clienteIn.Id);

        public void Remove(string id) => _cliente.DeleteOne(cliente => cliente.Id == id);
    }
}