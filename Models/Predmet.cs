using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Predmet")]

    public class Predmet
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Naziv { get; set; }

        public Fakultet Fakultet { get; set; }

        [Range(1,5)]
        [Required]
        public int Godina { get; set; }
    }
}