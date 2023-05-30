using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using io_book_project.Utils;

namespace io_book_project.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString(Const.USER_ROLE)!="admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult AddAuthor()
        {
            return View();
        }

        public IActionResult AuthorList()
        {
            return View();
        }

        public IActionResult AddBook()
        {
            return View();
        }

        public IActionResult BookList()
        {
            return View();
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        public IActionResult CategoryList()
        {
            return View();
        }

        public IActionResult AddPublishingHouse()
        {
            return View();
        }

        public IActionResult PublishingHouseList()
        {
            return View();
        }
    }
}
