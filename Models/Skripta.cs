using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Models
{
    [Table("Skripta")]

    public class Skripta
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Naziv { get; set; }
   
        [JsonIgnore]
        public  Predmet Predmet { get; set; }

        [Required]
        [NotMapped]
        public IFormFile File { get; set; } 

    }
}
