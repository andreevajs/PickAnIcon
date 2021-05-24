using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickAnIcon.Database.Entities
{
    [Table("IconParts")]
    public class IconPart
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Icon Icon { get; set; }
        [Required]
        public Part Part { get; set; }

        [MaxLength(8)]
        public string ColorHEX { get; set; }

        [Required]
        public int Layer { get; set; }
    }
}
