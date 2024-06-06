using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuthApp.Models;
using RoleBasedAuthApp.Services;
using System.Threading.Tasks;

namespace RoleBasedAuthApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserService _userService;

        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            await _userService.DeleteUserAsync(userId);
            return RedirectToAction("Index");
        }
    }
}
