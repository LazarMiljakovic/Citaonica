using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;

namespace Citaonica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradController : ControllerBase
    {
        public CitaonicaContext Context { get; set; }

        public GradController(CitaonicaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiGradove")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiGradove()
        {
            try
            {
                return Ok(await Context.Gradovi.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajGrad/{naziv}")]
        [HttpPost]        
        public async Task<ActionResult> DodajFakultet(string naziv)
        {
            try
            {
                var gradovi = await Context.Gradovi.Where(p=> p.Naziv == naziv).FirstOrDefaultAsync();
                
                
                if(gradovi == null)
                {
                    Grad grad = new Grad();
                    
                    grad.Naziv = naziv;
                    
                    
                    Context.Gradovi.Add(grad);
                    await Context.SaveChangesAsync();
                    return Ok("Dodat");
                
                }
                else
                {
                    return BadRequest("vec postoji");
                }                
            }

            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("IzmeniGrad/{gradID}/{noviNaziv}")]
        [HttpPut]  
        public async Task<ActionResult> PromeniGrad(int gradID,string noviNaziv)
        {
            try
            {
                var grad = await Context.Gradovi.Where(p=> p.ID == gradID).FirstOrDefaultAsync();
                
                
                if(grad != null)
                {
                    grad.Naziv = noviNaziv;
                    
                    await Context.SaveChangesAsync();
                    return Ok("Promenjen");
                
                }
                else
                {
                    return BadRequest("Nije pronadjen");
                }                
            }

            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisiGrad/{gradID}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiGrad(int gradID)
        {
            try
            {
                
                var grad = await Context.Gradovi.FindAsync(gradID);
                if(grad!=null)
                {
                    Context.Gradovi.Remove(grad);
                    await Context.SaveChangesAsync();
                    return Ok("Izbrisan");   
                }
                else
                {
                    return BadRequest("Ne postoji");

                }
                
            }

            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        
    }
}






