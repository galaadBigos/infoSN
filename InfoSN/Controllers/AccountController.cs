using InfoSN.Models.ViewModel.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace InfoSN.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            RegisterVM model = new RegisterVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.ConfirmPassword)
                {



                }
                else
                {
                    ModelState.AddModelError("Password", "Le mot de passe et la confirmation ne coresspondent pas");
                }
            }

            model.Password = "";
            model.ConfirmPassword = "";

            return View(model);
        }
    }
}
