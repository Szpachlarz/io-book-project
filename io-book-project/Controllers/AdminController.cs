using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using io_book_project.Utils;
using io_book_project.ViewModels;
using io_book_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using io_book_project.Data;
using static System.Net.WebRequestMethods;
using io_book_project.Interfaces;

namespace io_book_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookRepository _bookRepository;
        public AdminController(AppDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
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

                _bookRepository.Add(book);
                _bookRepository.Save();

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