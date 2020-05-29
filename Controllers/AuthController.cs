using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;  
using MongoDB.Bson;  
using apiweb.Models;
using apiweb.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace apiweb.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller{
        private readonly IConfiguration _configuration;
        private readonly TallerService _tallerService;
        private readonly ClienteService _clienteService;

        public AuthController(IConfiguration configuration, ClienteService clienteService, TallerService tallerService){
            _configuration = configuration;
            _clienteService = clienteService;
            _tallerService = tallerService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult LoginEmail(UserEmailLogin obj){

            IActionResult response = Unauthorized();  
            var userC = _clienteService.iniciaSesionEmail(obj);
            var userT = _tallerService.iniciaSesionEmail(obj);
            if(userC != null){
                var tokenString = GenerateJSONWebToken(userC);
                response = Ok(new {tokenString});
            }
            if(userT != null){
                var tokenString = GenerateJSONWebToken(userT);
                response = Ok(new  {tokenString});
            }
            return response;
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult LoginCellphone(UserCellLogin obj){

            IActionResult response = Unauthorized();  
            var userC = _clienteService.iniciaSesionCell(obj);
            var userT = _tallerService.iniciaSesionCell(obj);
            if(userC != null ){
                var tokenString = GenerateJSONWebToken(userC);
                response = Ok(new {tokenString});
            }
            if(userT != null){
                var tokenString = GenerateJSONWebToken(userT);
                response = Ok(new {tokenString});
            }
            return response;
        }

        public string GenerateJSONWebToken(Cliente userC){
            var SecretKey = _configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(ClaimTypes.NameIdentifier, userC.Id.ToString()),
                        new Claim(ClaimTypes.Email, userC.correo),
                        new Claim(ClaimTypes.Role, userC.role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokerHandler = new JwtSecurityTokenHandler();
                var createdToken = tokerHandler.CreateToken(tokenDescriptor);
                return tokerHandler.WriteToken(createdToken);
        }
        public string GenerateJSONWebToken(Taller userT){
            var SecretKey = _configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(ClaimTypes.NameIdentifier, userT.Id.ToString()),
                        new Claim(ClaimTypes.Email, userT.correo),
                        new Claim(ClaimTypes.Role, userT.role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokerHandler = new JwtSecurityTokenHandler();
                var createdToken = tokerHandler.CreateToken(tokenDescriptor);
                return tokerHandler.WriteToken(createdToken);
        }
    }
}