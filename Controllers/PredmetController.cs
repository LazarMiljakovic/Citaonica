using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Citaonica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredmetController : ControllerBase
    {
        public CitaonicaContext Context { get; set; }

        public PredmetController(CitaonicaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiPredmete/{fakultetID}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(int fakultetID)
        {
            try
            {
                var predmeti = await Context.Predmeti.Where(p => p.Fakultet.ID == fakultetID).ToListAsync();
                return Ok(predmeti);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            

        }

        [Route("DodajPredmet/{naziv}/{godina}/{faklutetID}")]
        [HttpPost] 
        public async Task<ActionResult> DodajPredmet(string naziv, int godina,int faklutetID)
        {
            try
            {
                var predmeti = await Context.Predmeti.Where(p=> p.Naziv == naziv&& p.Godina == godina&& p.Fakultet.ID == faklutetID).FirstOrDefaultAsync();
                
                
                if(predmeti == null)
                {
                    Predmet predmet = new Predmet();
                    
                    var fakultet = await Context.Fakulteti.Where(p=>p.ID == faklutetID).FirstOrDefaultAsync();
                    
                    predmet.Naziv = naziv;
                    predmet.Godina = godina;
                    predmet.Fakultet = fakultet;
                    
                    
                    Context.Predmeti.Add(predmet);
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

        [Route("IzmeniPredmet/{predmetID}/{noviNaziv}/{godina}")]
        [HttpPut]  
        public async Task<ActionResult> PromeniPredmet(int predmetID,string noviNaziv,int godina)
        {
            try
            {
                var predmet = await Context.Predmeti.Where(p=> p.ID == predmetID).FirstOrDefaultAsync();
                
                
                if(predmet != null)
                {
                    predmet.Naziv = noviNaziv;
                    predmet.Godina = godina;
                    
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

        [Route("IzbrisiPredmet/{predmetID}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiPredmet(int predmetID)
        {
            try
            {
                
                var predmet = await Context.Predmeti.FindAsync(predmetID);
                if(predmet!=null)
                {
                    Context.Predmeti.Remove(predmet);
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