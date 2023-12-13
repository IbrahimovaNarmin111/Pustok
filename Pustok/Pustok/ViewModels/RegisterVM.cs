
using System.ComponentModel.DataAnnotations;

namespace Pustok.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        [MinLength(7)]
        [MaxLength(25)]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
