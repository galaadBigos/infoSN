using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InfoSN.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _userService;

        public AccountController(IAccountService userService)
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
                    try
                    {
                        _userService.PostRegisterVM(model);
                    }
                    catch (SqlException ex)
                    {
                        return View();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("The insert query did not work");
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Le mot de passe et la confirmation ne coresspondent pas");
                }
            }

            return View(model);
        }
    }
}
