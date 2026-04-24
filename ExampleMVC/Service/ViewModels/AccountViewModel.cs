using System;
using System.ComponentModel.DataAnnotations;

namespace Service.ViewModels
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        public string Role { get; set; }
    }
}
