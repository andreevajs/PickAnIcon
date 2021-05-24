using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickAnIcon.Database.Entities
{
    [Table("Parts")]
    public class Part
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(40)]
        public string FileName { get; set; }

        [Required]
        public bool IsFree { get; set; }
    }
}
