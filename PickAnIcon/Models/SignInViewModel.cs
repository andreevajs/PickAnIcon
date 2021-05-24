using System.ComponentModel.DataAnnotations;

namespace PickAnIcon.Models
{
    public class SignInViewModel
    {
        [Display(Name = "Username")]
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length: 50")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length: 50")]
        public string Password { get; set; }
    }
}
