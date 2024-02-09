using System.ComponentModel.DataAnnotations;

namespace FCManager.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required to login")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required to login")]
        public string Password { get; set; }
    }
}
