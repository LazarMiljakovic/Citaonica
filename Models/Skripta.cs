using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Skripta")]

    public class Skripta
    {
        [Key]
        public int ID { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$")]
        [Required]
        public string Naziv { get; set; }

        public Fakultet Fakultet { get; set; }    

        public Predmet Predmet { get; set; }

    }
}
