using System.ComponentModel.DataAnnotations;

namespace Service.ViewModels
{
    public class AuthViewModel
    {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
    }
}