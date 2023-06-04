using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using io_book_project.Utils;
using io_book_project.ViewModels;
using io_book_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using io_book_project.Data;
using static System.Net.WebRequestMethods;


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
                    OriginalTitle = model.OriginalTitle,
                    ISBN = model.ISBN,
                    PublicationYear = model.PublicationYear,
                    FirstPublicationYear = model.FirstPublicationYear,
                    Language = model.Language,
                    OriginalLanguage = model.OriginalLanguage,
                    Translation = model.Translation,
                    PageCount = model.PageCount,
                    Series = model.Series,
                    Description = model.Description,
                    //CoverImagePath = "https://upload.wikimedia.org/wikipedia/commons/e/e0/JPEG_example_JPG_RIP_050.jpg",
                    PublisherId = 1,
                };

                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

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
