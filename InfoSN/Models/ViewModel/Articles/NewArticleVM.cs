using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InfoSN.Models.ViewModel.Articles
{
	public class NewArticleVM
	{
		[Display(Name = "Titre"), Required(ErrorMessage = "Le titre est obligatoire")]
		public string Title { get; set; } = null!;

		[Display(Name = "Description"), Required(ErrorMessage = "La description est obligatoire")]
		public string Description { get; set; } = null!;

		[HiddenInput(DisplayValue = false), Required]
		public string IdUser { get; set; } = null!;
	}
}
