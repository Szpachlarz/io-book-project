using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using io_book_project.Utils;
using io_book_project.ViewModels;
using io_book_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using io_book_project.Data;


namespace io_book_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }
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

        [HttpGet]
        public IActionResult AddBook()
        {
            //przekopiować z indexu to zabezpieczenie admina
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = model.Title,
                };

                _context.Books.Add(book);
                _context.SaveChanges();

                return RedirectToAction("Index", "Admin");
            }

            return View("AddBook", model);
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
