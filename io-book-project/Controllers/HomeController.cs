using io_book_project.Data;
using io_book_project.Interfaces;
using io_book_project.Models;
using io_book_project.Repository;
using io_book_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Dynamic;

namespace io_book_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly ICategoryRepository _categoryRepository;
        public HomeController(ILogger<HomeController> logger, AppDbContext context, IBookRepository bookRepository, IAuthorRepository authorRepository, IPublisherRepository publisherRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> books = await _bookRepository.GetAll();
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> BookPage(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return View("Error");
            var authors = await _authorRepository.GetAuthorNames(id);
            var categories = await _categoryRepository.GetCategoryNames(id);
            var publisher = await _publisherRepository.GetByBookId(id);
            var bookVM = new BookPageViewModel
            {
                Title = book.Title,
                CoverImagePath = book.CoverImagePath,
                Authors = authors,
                Publisher = publisher.Name,
                Language = book.Language,
                OriginalLanguage = book.OriginalLanguage,
                Translation = book.Translation,
                Description = book.Description,
                Series = book.Series,
                PublicationYear = book.PublicationYear,
                FirstPublicationYear = book.FirstPublicationYear,
                ISBN=book.ISBN,
                PageCount=book.PageCount,
                Categories = categories,
            };
            return View(bookVM);
        }

        public async Task<IActionResult> AuthorsPage(int id)
        {
            
            return View();
        }

        public async Task<IActionResult> PublishersPage(int id)
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}