using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Fakultet")]

    public class Fakultet
    {
        [Key]
        public int ID { get; set; } 

        
        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }

        [JsonIgnore]
        public Grad Grad { get; set; }
        
        [JsonIgnore]
        public  List<Predmet> listaPredmeta { get; set; }
    }
}