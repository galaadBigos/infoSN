using System.ComponentModel.DataAnnotations;

namespace InfoSN.Models.ViewModel.Accounts
{
	public class LoginVM
	{
		[Required(ErrorMessage = "Le champ Email est requis")]
		[EmailAddress]
		[Display(Name = "Email")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Le champ Mot de passe est requis")]
		[DataType(DataType.Password)]
		[Display(Name = "Mot de passe")]
		public string? Password { get; set; }
		public bool IsPersistent { get; set; }
	}
}
