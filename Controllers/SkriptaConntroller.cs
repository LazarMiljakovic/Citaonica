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
    public class SkriptaController : ControllerBase
    {
        public CitaonicaContext Context { get; set; }

        public SkriptaController(CitaonicaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiSkripte/{predmetID}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(int predmetID)
        {
            try
            {
                var skripte = await Context.Skripte.Where(p => p.Predmet.ID == predmetID).ToListAsync();
                return Ok(skripte);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajSkriptu/{predmetID}")]
        [HttpPost]        
        public async Task<ActionResult> DodajSkriptu(int predmetID,IFormFile file)
        {
            try
            {                
                var skripte = await Context.Skripte.Where(p=> p.Naziv == file.FileName&& p.Predmet.ID == predmetID).FirstOrDefaultAsync();
                if(skripte == null)
                {
                    var predmet = await Context.Predmeti.Where(p=>p.ID == predmetID).FirstOrDefaultAsync();
                    if(CheckIfPDFFile(file))
                    {
                        await WriteFile(file,predmet.ID.ToString());
                    }
                    else{
                        return BadRequest(new { message = "Dokument nije pdf" });
                    }
                
                    Skripta skripta = new Skripta();
                    skripta.Naziv = file.FileName;
                    skripta.Predmet = predmet;
                    skripta.File = file;
                        
                    Context.Skripte.Add(skripta);
                    await Context.SaveChangesAsync();
                    return Ok("Dodat");
                }     
                else{
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
        private async Task<bool> WriteFile(IFormFile file,string IdPredmeta)
        {
            bool isSaveSuccess = false;
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = file.FileName; //Create a new Name for the file due to security reasons.

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "UploadS\\"+IdPredmeta);

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadS\\"+IdPredmeta,
                   fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                Content(e.Message);
            }

            return isSaveSuccess;
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
                var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadS\\"+Idpredmeta,imeFajla);
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

        [Route("IzbrisatiSkriptu/{skriptaID}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiSkriptu(int skriptaID)
        {
            try
            {
                
                var skripta = await Context.Skripte.FindAsync(skriptaID);
                if(skripta!=null)
                {
                    Context.Skripte.Remove(skripta);
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