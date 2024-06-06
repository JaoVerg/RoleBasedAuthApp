using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuthApp.Models;
using RoleBasedAuthApp.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoleBasedAuthApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userService.GetUserByUsernameAndPasswordAsync(username, password);

            if (user != null)
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (user.Role == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (user.Role == "user")
                {
                    return RedirectToAction("Index", "User");
                }
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string role)
        {
            var user = new User
            {
                Username = username,
                Password = password,  // Ensure to hash the password in a real application
                Role = role
            };

            await _userService.CreateUserAsync(user);

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
