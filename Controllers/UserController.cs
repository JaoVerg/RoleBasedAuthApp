using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuthApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
