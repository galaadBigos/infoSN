using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace InfoSN.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

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
                    _userService.PostRegisterVM(model);
                    return RedirectToAction("Index", "Home");
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
