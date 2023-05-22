using Microsoft.AspNetCore.Mvc;

namespace io_book_project.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
