using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Fakultet")]

    public class Fakultet
    {
        [Key]
        public int ID { get; set; } 

        [RegularExpression(@"^[a-zA-Z]+$")]
        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$")]
        [Required]
        [MaxLength(50)]
        public string Grad { get; set; }
        
        public List<Predmet> listaPredmeta { get; set; }
    }
}