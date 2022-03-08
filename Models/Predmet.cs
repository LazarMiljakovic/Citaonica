using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Predmet")]

    public class Predmet
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Naziv { get; set; }

        [JsonIgnore]
        public Fakultet Fakultet { get; set; }

        [Range(1,5)]
        [Required]
        public int Godina { get; set; }



    }
}