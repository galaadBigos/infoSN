using InfoSN.Managers.Abstractions;
using InfoSN.Models.ViewModel.Accounts;
using InfoSN.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InfoSN.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly IAccountManager _accountManager;
		private readonly ICookieAuthenticationManager _cookieManager;

		public AccountController(IAccountService accountService, IAccountManager accountManager, ICookieAuthenticationManager cookieManager)
		{
			_accountService = accountService;
			_accountManager = accountManager;
			_cookieManager = cookieManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View(new RegisterVM());
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
						_accountService.PostRegisterVM(model);
					}
					catch (Exception ex)
					{
						ViewBag.ExceptionMessage = ex.Message;
						return View();
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

		[HttpGet]
		public IActionResult Login()
		{
			return View(new LoginVM());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginVM model)
		{
			if (ModelState.IsValid && _accountManager.IsRightIdentifier(model))
			{
				List<Claim> claims = _cookieManager.CreateLoginClaims(model);
				ClaimsIdentity claimsIdentity = _cookieManager.CreateLoginIdentity(claims);
				AuthenticationProperties authProperties = _cookieManager.CreateLoginAuthenticationProperties(model.IsPersistent);

				await HttpContext.SignInAsync(
					"LoginCookie",
					new ClaimsPrincipal(claimsIdentity),
					authProperties);

				return RedirectToAction("Index", "Home");
			}

			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync("LoginCookie");

			return RedirectToAction("Index", "Home");
		}
	}
}
