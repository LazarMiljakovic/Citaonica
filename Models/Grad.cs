using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Grad")]

    public class Grad
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Naziv { get; set; }
    }
}
