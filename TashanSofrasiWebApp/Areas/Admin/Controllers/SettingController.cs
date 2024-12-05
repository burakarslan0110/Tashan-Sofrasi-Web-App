using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.EntityLayer.Entities;
using TashanSofrasiWebApp.DTOs.IdentityDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public SettingController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditDTO userEditDTO = new UserEditDTO
            {
                Email = values.Email,
                Name = values.Name,
                Surname = values.Surname,
                Username = values.UserName
            };
            return View(userEditDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditDTO userEditDTO)
        {
            if (userEditDTO.Password == userEditDTO.ConfirmPassword)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
				user.Email = userEditDTO.Email;
				user.Name = userEditDTO.Name;
				user.Surname = userEditDTO.Surname;
				user.UserName = userEditDTO.Username;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userEditDTO.Password);
				var result = await _userManager.UpdateAsync(user);
				if (result.Succeeded)
				{
                    return RedirectToAction("Index","Category");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
					return View(userEditDTO);
				}
			}
			else
			{
				ModelState.AddModelError("", "Şifreler uyuşmuyor.");
				return View(userEditDTO);
			}
        }

	}
}
