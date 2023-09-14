using System.ComponentModel.DataAnnotations;

namespace InfoSN.Models.ViewModel.Accounts
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Pseudo")]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation de mot de passe")]
        public string? ConfirmPassword { get; set; }
    }
}
