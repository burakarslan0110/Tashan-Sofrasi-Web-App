using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.EntityLayer.Entities;
using TashanSofrasiWebApp.DTOs.IdentityDTOs;

namespace TashanSofrasiWebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterDTO registerDTO)
        {
            // Model doğrulama başarısız ise tekrar formu göster
            if (!ModelState.IsValid)
            {
                return View(registerDTO);
            }


            // Şifreler eşleşiyor mu kontrol et
            if (registerDTO.Password != registerDTO.PasswordConfirm)
            {
                ModelState.AddModelError("", "Şifreler eşleşmedi");
                return View(registerDTO);
            }

            // Yeni bir kullanıcı oluştur
            var user = new AppUser
            {
                Name = registerDTO.Name,
                Surname = registerDTO.Surname,
                Email = registerDTO.Email,
                UserName = registerDTO.Username
            };

            // Kullanıcı oluşturma işlemi
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                // Başarılı kayıt sonrası giriş sayfasına yönlendir
                return RedirectToAction("Index", "Login");
            }

            // Hata mesajlarını modele ekle
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            // Hatalar varsa formu tekrar göster
            return View(registerDTO);
        }

    }
}
