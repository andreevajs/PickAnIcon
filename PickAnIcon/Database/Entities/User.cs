using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickAnIcon.Database.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Username { get; set; }

        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(256)]
        public string Salt { get; set; }

        public List<Icon> Icons { get; set; }
    }
}
