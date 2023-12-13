

using System.ComponentModel.DataAnnotations;

namespace Pustok.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
