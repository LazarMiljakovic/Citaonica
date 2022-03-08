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
    public class ProfesorController : ControllerBase
    {
        public CitaonicaContext Context { get; set; }

        public ProfesorController(CitaonicaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiProfesore/{fakultetID}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(int fakultetID)
        {
            try
            {
                var profesori = await Context.Profesori.Where(p => p.Fakultet.ID == fakultetID).ToListAsync();
                return Ok(profesori);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajProfesora/{ime}/{prezime}/{fakultetID}/{predmetID}/{email}/{kancelarija}")]
        [HttpPost]        
        public async Task<ActionResult> DodajProfesora(string ime, string prezime,int fakultetID,int predmetID,string email,string kancelarija)
        {
            try
            {
                Profesor profesor = new Profesor();
                var fakultet = await Context.Fakulteti.Where(p=>p.ID == fakultetID).FirstOrDefaultAsync();
                var predmet = await Context.Predmeti.Where(p=>p.ID == predmetID).FirstOrDefaultAsync();

                profesor.Ime = ime;
                profesor.Prezime = prezime;
                profesor.Fakultet = fakultet;
                profesor.Predmet = predmet;
                profesor.email = email;
                profesor.kancelarija = kancelarija;
                    
                Context.Profesori.Add(profesor);
                await Context.SaveChangesAsync();
                return Ok("Dodat");
                              
            }

            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("IzmeniProfesora/{profesorID}/{email}/{kancelarija}")]
        [HttpPut]  
        public async Task<ActionResult> IzmeniProfesora(int profesorID,string email,string kancelarija)
        {
            try
            {
                var profesor = await Context.Profesori.Where(p=> p.ID == profesorID).FirstOrDefaultAsync();
                
                
                if(profesor != null)
                {
                    profesor.email = email;
                    profesor.kancelarija = kancelarija;
                    
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

        [Route("IzbrisiProfesora/{profesorID}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiProfesora(int profesorID)
        {
            try
            {
                
                var profesor = await Context.Profesori.FindAsync(profesorID);
                if(profesor!=null)
                {
                    Context.Profesori.Remove(profesor);
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