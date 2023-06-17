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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace io_book_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        public AdminController(AppDbContext context, IBookRepository bookRepository, IPublisherRepository publisherRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddAuthor()
        {
            // może kiedyś coś tu będzie
            return View();
        }

        [HttpPost]
        public IActionResult AddAuthor(AddAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = new Author
                {
                    Names = model.Names,
                    Surname = model.Surname,
                    Nationality = model.Nationality,
                    DateOfBirth = model.DateOfBirth,
                    DateOfDeath = model.DateOfDeath,
                };

                _authorRepository.Add(author);
                _authorRepository.Save();

                return RedirectToAction("Index", "Admin");
            }

            return View("AddAuthor", model);
        }

        public IActionResult AuthorList()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            //przekopiować z indexu to zabezpieczenie admina

            var publishers = await _publisherRepository.GetAll();
            ViewData["Publishers"] = new SelectList((System.Collections.IEnumerable)publishers, "Id", "Name");

            var authors = await _authorRepository.GetAll();
            var authorList = authors.Select(a => new { Id = a.Id, FullName = $"{a.Names} {a.Surname}" }).ToList();
            ViewData["Authors"] = new SelectList((System.Collections.IEnumerable)authorList, "Id", "FullName");
            
            var categories = await _categoryRepository.GetAll();
            ViewData["Categories"] = new SelectList((System.Collections.IEnumerable)categories, "Id", "Name");

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
                    CoverImagePath = model.CoverImagePath,
                    PublisherId = model.PublisherId,
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