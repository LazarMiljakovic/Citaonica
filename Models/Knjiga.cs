using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Knjiga")]

    public class Knjiga
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Naziv { get; set; }

        [Range(1,5)]
        [Required]
        public int Godina { get; set; }

        public Fakultet Fakultet { get; set; }

        public Predmet Predmet { get; set; }
    }
}