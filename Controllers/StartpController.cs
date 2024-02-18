using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using TheaterClubProject.Models;

namespace TheaterClubProject.Controllers
{
	public class StartpController : Controller
	{
        private readonly TheaterClubContext _context;


        public StartpController(TheaterClubContext context)
        {
            _context = context;

        }
        public IActionResult Index()
		{
			return View();
		}

        public IActionResult Login()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            if (claimuser.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "TblGosteriTakips");
            }
            else
            {
                return View();
            }

        }


        [HttpPost]

        public async Task<IActionResult> Login(Loginc loginc)
        {
            bool userExist = _context.Logincs.Any(x => x.Email == loginc.Email && x.Password ==loginc.Password);
            if (userExist)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, loginc.Email!)

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties prop = new AuthenticationProperties()
                {
                    AllowRefresh = true,

                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), prop);
                return RedirectToAction("Index", "TblGosteriTakips");

            }

            ViewData["OnayMesajı"] = "Email veya Şifre Yanlış";
            return View();
        }

    }
}
