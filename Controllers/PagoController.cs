using System;
using Microsoft.AspNetCore.Mvc;
using apiweb.Models;
namespace apiweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : Controller{
        [HttpPost]
        public ActionResult proceder(Pago pago){
            if(pago != null){
                if(pago.numeroTar!= null){
                    if(pago.numSeguridad!=null){
                        if(pago.mesVencimiento != null && pago.añoVencimiento!=null){
                            DateTime dateOnly = DateTime.Now;
                            int año=dateOnly.Year;
                            int mes=dateOnly.Month;
                            int añoñ=Int32.Parse(pago.añoVencimiento)+2000;
                            int meñ=Int32.Parse(pago.mesVencimiento);
                            if(año == añoñ){
                                if(meñ<mes){
                                    return Ok();
                                }
                                else{
                                    return BadRequest();
                                }
                            }
                            if(año>añoñ){
                                return Ok();
                            }
                            else{
                                return BadRequest();
                            }
                        }
                        else{
                            return BadRequest();
                        }
                    }
                    else{
                        return BadRequest();
                    }
                }
                else{
                    return BadRequest();
                }
            }
            return BadRequest();
        }
    }
       
}