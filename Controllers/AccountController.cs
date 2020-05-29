using Microsoft.AspNetCore.Mvc;
using apiweb.Models;
using apiweb.Services;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using System;
using MimeKit.Utils;
using System.Web.Http.Cors;

namespace apiweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller{
        private readonly ClienteService _clienteService;
        private readonly TallerService _tallerService;
        public AccountController(ClienteService clienteService, TallerService tallerService){
            _clienteService=clienteService;
            _tallerService=tallerService;
        }
        [HttpPost]
        public ActionResult news(ForgotPassword mod){
            var userC = _clienteService.GetCorreo(mod.Email.ToString());
            var userT = _tallerService.GetCorreo(mod.Email.ToString());
            if(userC != null){
                this.SendCorreoCliente(userC);
                return Ok();
            }
            if(userT != null){
                this.SendCorreoTaller(userT);
                return Ok();
            }
            return BadRequest();
        }
        private void SendCorreoCliente(Cliente user){
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[10];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            if(_clienteService.insertCodigo(user, finalString)){
                var message = new MimeMessage();
                var bodyBuilder = new BodyBuilder();

                message.From.Add(new MailboxAddress("MechaFinder Support", "MechaFinderSupp@gmail.com"));

                message.To.Add(new MailboxAddress("To: ", user.correo));

                message.ReplyTo.Add(new MailboxAddress("reply_to", "MechaFinderSupp@gmail.com"));

                message.Subject = "Solictud de Reinicio de Contraseña";
                //bodyBuilder.HtmlBody= "Querido usuario: "+user.Nombre+" "+user.apellidop+" "+user.apellidom+"."+Environment.NewLine+"Tu codigo para reiniciar tu contraseña es: "+finalString;
                //message.Body = bodyBuilder.ToMessageBody();
                message.Body = new TextPart("plain"){
                    Text = @"Querido Usuario: "+user.Nombre+" "+user.apellidop+" "+user.apellidom+"."+"\n\n"
                    +"Su código para el reinicio de su contraseña es: "+finalString+".\n\n"
                    +"Gracias por preferir el uso de nuestra aplicación." 
                };
                

                var client = new SmtpClient();

                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate("MechaFinderSupp@gmail.com", "mechafinder123");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        private void SendCorreoTaller(Taller user){
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[10];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            if(_tallerService.insertCodigo(user, finalString)){
                var message = new MimeMessage();
                var bodyBuilder = new BodyBuilder();

                message.From.Add(new MailboxAddress("MechaFinder Support", "MechaFinderSupp@gmail.com"));

                message.To.Add(new MailboxAddress("To: "+user.nombreTaller, user.correo));

                message.ReplyTo.Add(new MailboxAddress("reply_to", "MechaFinderSupp@gmail.com"));

                message.Subject = "Solictud de Reinicio de Contraseña";
                bodyBuilder.HtmlBody="Querido usuario: "+user.nombreDueño;
                bodyBuilder.HtmlBody= "Tu codigo para reiniciar tu contraseña es: "+finalString;
                message.Body = bodyBuilder.ToMessageBody();

                var client = new SmtpClient();

                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate("MechaFinderSupp@gmail.com", "mechafinder123");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}