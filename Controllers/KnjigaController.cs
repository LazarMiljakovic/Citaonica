using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using System.IO;
using System.Collections.Generic;

namespace Citaonica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KnjigaController : ControllerBase
    {
        public CitaonicaContext Context { get; set; }

        public KnjigaController(CitaonicaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiKnjige/{predmetID}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(int predmetID)
        {
            try
            {
                var Knjige = await Context.Knjige.Where(p => p.Predmet.ID == predmetID).ToListAsync();
                return Ok(Knjige);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajKnjigu/{predmetID}")]
        [HttpPost]        
        public async Task<ActionResult> DodajKnjigu(int predmetID,IFormFile file)
        {
            
            try
            {
                var knjige = await Context.Knjige.Where(p=> p.Naziv == file.FileName&& p.Predmet.ID == predmetID).FirstOrDefaultAsync();
                if(knjige == null)
                {
                    var predmet = await Context.Predmeti.Where(p=>p.ID == predmetID).FirstOrDefaultAsync();
                    if(CheckIfPDFFile(file))
                    {
                        await UpisiFajl(file,predmet.ID.ToString());
                    }
                    else{
                        return BadRequest(new { message = "Dokument nije pdf" });
                    }
                    Knjiga knjiga = new Knjiga();
                    

                    knjiga.Predmet = predmet;
                    knjiga.File = file;
                    knjiga.Naziv = file.FileName;
                    
                    
                        
                    Context.Knjige.Add(knjiga);
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

        private bool CheckIfPDFFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".pdf" || extension == ".pdf"); 
        }
        private async Task<bool> UpisiFajl(IFormFile file,string IdPredmeta)
        {
            bool UspesnoSacuvano = false;
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = file.FileName; 

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\"+IdPredmeta);

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\"+IdPredmeta,
                   fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                UspesnoSacuvano = true;
            }
            catch (Exception e)
            {
                Content(e.Message);
            }

            return UspesnoSacuvano;
        }

        [Route("PreuzmiKnjigu/{Idpredmeta}/{imeFajla}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(string Idpredmeta,string imeFajla)
        {
            try
            {
                if(imeFajla==null)
                {
                    return Content("Nema imena");
                }
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\"+Idpredmeta,imeFajla);
                var memory = new MemoryStream();
                using(var stream = new FileStream(path,FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position=0;
                try{
                    return File(memory,GetContentType(path),Path.GetFileName(path));
                }
                catch
                {
                    return Ok("Ne postoji");

                }
            

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        private string GetContentType(string path)  
        {  
            var types = GetMimeTypes();  
            var ext = Path.GetExtension(path).ToLowerInvariant();  
            return types[ext];  
        }  
   
        private Dictionary<string, string> GetMimeTypes()  
        {  
            return new Dictionary<string, string>  
            {  
                {".txt", "text/plain"},  
                {".pdf", "application/pdf"},  
                {".doc", "application/vnd.ms-word"},  
                {".docx", "application/vnd.ms-word"},  
                {".xls", "application/vnd.ms-excel"},  
                {".xlsx", "application/vnd.openxmlformats  officedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},  
                {".jpg", "image/jpeg"},  
                {".jpeg", "image/jpeg"},  
                {".gif", "image/gif"},  
                {".csv", "text/csv"}  
            };  
        } 


        [Route("IzbrisatiKnjigu/{knjigaID}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiKnjigu(int knjigaID)
        {
            try
            {
                
                var knjiga = await Context.Knjige.FindAsync(knjigaID);
                if(knjiga!=null)
                {
                    Context.Knjige.Remove(knjiga);
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