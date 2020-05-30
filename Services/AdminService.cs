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
    public class AdminService{
        private readonly IMongoCollection<Admin> _admin;

        public AdminService(IAdminstoreDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _admin = database.GetCollection<Admin>(settings.AdminCollectionName);
        }

        public List<Admin> Get() => _admin.Find<Admin>(admin => true).ToList();

        public Admin Get(string id) => _admin.Find<Admin>(admin => admin.Id == id).FirstOrDefault();

        public Admin GetCorreo(string email) => _admin.Find<Admin>(admin => admin.correo.Equals(email)).FirstOrDefault();

        public Admin GetCelular(string celular) => _admin.Find<Admin>(admin => admin.celular == celular).FirstOrDefault();

        public List<Admin> GetC(string correo) => _admin.Find<Admin>(admin => admin.correo.Equals(correo)).ToList();

        public List<Admin> GetCel(string celular) => _admin.Find<Admin>(admin => admin.celular.Equals(celular)).ToList();

        public Admin Create(Admin admin){
            _admin.InsertOne(admin);
            return admin;
        } 
        public string GetId(string correo){
            var admin = this.GetCorreo(correo);
            if(admin != null){
                return admin.Id.ToString();
            }   
            return null;
        }
        public Admin iniciaSesionEmail(UserEmailLogin cli){
            var admin = GetCorreo(cli.Email);
            if(admin != null){
                if(admin.contraseña.Equals(cli.Password)){
                    return admin;
                }
            }
            return null;
        }
        public Admin iniciaSesionCell(UserCellLogin cli){
            var admin = GetCelular(cli.Cellphone);
            if(admin != null){
                if(admin.contraseña.Equals(cli.Password)){
                    return admin;
                }
            }
            return null;
        }
        public Boolean insertCodigo(Admin cli, string codigo){
            var user = GetCorreo(cli.correo);
            if(user!=null){
                if(!(codigo.Equals(null))){
                    _admin.FindOneAndUpdate(admin => admin.correo.Equals(cli.correo), Builders<Admin>.Update.Set("codigo", codigo));
                    return true;
                }
            }
            return false;
        }
        public Boolean changePassword(Admin cli, string newPassword){
            if(cli != null){
                if(!(newPassword.Equals(null))){
                    _admin.FindOneAndUpdate(admin => admin.correo.Equals(cli.correo), Builders<Admin>.Update.Set("contraseña", newPassword));
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
        //public void Update(string id, Admin adminIn) => _admin.UpdateOne(admin=>admin.Id==id, Builders<Admin>.Update.Set("apellidop", adminIn.apellidop).Set("apellidom",adminIn.apellidom).Set("nombre",adminIn.Nombre).Set("calle",adminIn.calle).Set("numCasa",adminIn.numCasa).Set("colonia",adminIn.colonia).Set("celular", adminIn.celular).Set("correo",adminIn.correo));
        public void Update(String id, Admin adminIn){
            if(adminIn!=null){
                try{
                    if(!(adminIn.apellidop.Equals(null))){
                        _admin.FindOneAndUpdate(admin => admin.Id == id, Builders<Admin>.Update.Set("apellidop", adminIn.apellidop));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!(adminIn.apellidom.Equals(null))){
                        _admin.FindOneAndUpdate(admin => admin.Id == id, Builders<Admin>.Update.Set("apellidom",adminIn.apellidom));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!adminIn.Nombre.Equals(null)){
                        _admin.FindOneAndUpdate(admin => admin.Id == id, Builders<Admin>.Update.Set("nombre", adminIn.Nombre));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!adminIn.calle.Equals(null)){
                        _admin.FindOneAndUpdate(admin => admin.Id == id, Builders<Admin>.Update.Set("calle", adminIn.calle));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!adminIn.numCasa.Equals(null)){
                        _admin.FindOneAndUpdate(admin => admin.Id == id, Builders<Admin>.Update.Set("numCasa", adminIn.numCasa));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!adminIn.colonia.Equals(null)){
                        _admin.FindOneAndUpdate(admin => admin.Id == id, Builders<Admin>.Update.Set("colonia", adminIn.colonia));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!adminIn.celular.Equals(null)){
                        _admin.FindOneAndUpdate(admin => admin.Id == id, Builders<Admin>.Update.Set("celular", adminIn.celular));
                    }
                }
                catch(NullReferenceException ex){}
                try{
                    if(!adminIn.correo.Equals(null)){
                        _admin.FindOneAndUpdate(admin => admin.Id == id, Builders<Admin>.Update.Set("correo", adminIn.correo));
                    }
                }
                catch(NullReferenceException ex){}
            }
        }
        public void Remove(Admin adminIn) => _admin.DeleteOne(admin => admin.Id== adminIn.Id);

        public void Remove(string id) => _admin.DeleteOne(admin => admin.Id == id);
    }
}