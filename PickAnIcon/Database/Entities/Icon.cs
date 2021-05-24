using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickAnIcon.Database.Entities
{
    [Table("Icons")]
    public class Icon
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public User Owner { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime LastEdited { get; set; }

        public List<IconPart> IconParts { get; set; }
    }
}
