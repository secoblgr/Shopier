using Microsoft.AspNetCore.Mvc;
using Shopier.Application.Dtos.AccountDtos;
using Shopier.Application.Interfaces;
using Shopier.Application.UseCasess.AccountServices;

namespace Shopier.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _accountService.Register(dto);
            return RedirectToAction("Login","Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _accountService.Login(dto);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Home");
        }

    }
}
