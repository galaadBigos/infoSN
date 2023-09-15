using System.ComponentModel.DataAnnotations;

namespace InfoSN.Models.ViewModel.Accounts
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Le champ Email est requis")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Le champ Pseudo est requis")]
        [Display(Name = "Pseudo")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Le champ Mot de passe est requis")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Le champ Confirmation de mot de passe est requis")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation de mot de passe")]
        public string? ConfirmPassword { get; set; }
    }
}
