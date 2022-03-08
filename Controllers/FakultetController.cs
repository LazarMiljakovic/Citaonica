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
    public class FakultetController : ControllerBase
    {
        public CitaonicaContext Context { get; set; }

        public FakultetController(CitaonicaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiFakultete/{gradID}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(int gradID)
        {
            try
            {
                var fakulteti = await Context.Fakulteti.Where(p => p.Grad.ID == gradID).ToListAsync();
                return Ok(fakulteti);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajFakultet/{naziv}/{gradID}")]
        [HttpPost]        
        public async Task<ActionResult> DodajFakultet(string naziv, int gradID)
        {
            try
            {
                var fakultet = await Context.Fakulteti.Where(p=> p.Naziv == naziv&& p.Grad.ID == gradID).FirstOrDefaultAsync();
                
                
                if(fakultet == null)
                {
                    Fakultet fakultet1 = new Fakultet();
                    var Grad = await Context.Gradovi.Where(p=>p.ID == gradID).FirstOrDefaultAsync();

                    fakultet1.Naziv = naziv;
                    fakultet1.Grad = Grad;
                    
                    Context.Fakulteti.Add(fakultet1);
                    await Context.SaveChangesAsync();
                    return Ok("Dodat");
                
                }
                else
                {
                    return BadRequest("Vec postoji");
                }                
            }

            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [Route("IzmeniFakultet/{fakultetID}/{noviNaziv}")]
        [HttpPut]  
        public async Task<ActionResult> PromeniFakultet(int fakultetID,string noviNaziv)
        {
            try
            {
                var fakultet = await Context.Fakulteti.Where(p=> p.ID == fakultetID).FirstOrDefaultAsync();
                
                
                if(fakultet != null)
                {
                    fakultet.Naziv = noviNaziv;
                    
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

        [Route("IzbrisiFakultet/{fakultetID}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiFakultet(int fakultetID)
        {
            try
            {
                
                var fakultet = await Context.Fakulteti.FindAsync(fakultetID);
                if(fakultet!=null)
                {
                    Context.Fakulteti.Remove(fakultet);
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