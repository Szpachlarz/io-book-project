using Microsoft.AspNetCore.Mvc;

namespace io_book_project.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BooksList()
        {
            return View();
        }
        public IActionResult ReviewsList()
        {
            return View();
        }
    }
}
