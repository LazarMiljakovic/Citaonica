using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Profesor")]

    public class Profesor
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [JsonIgnore]
        public  Fakultet Fakultet { get; set; }

        [JsonIgnore]
        public  Predmet Predmet { get; set; }

        [Required]
        public string email { get; set; }
        
        [Required]
        public string kancelarija { get; set; }

    }
}