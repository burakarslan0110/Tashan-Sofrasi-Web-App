using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.EntityLayer.Entities;
using TashanSofrasiWebApp.DTOs.IdentityDTOs;

namespace TashanSofrasiWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, false,false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View(loginDTO);
            }
        }
    }
}
