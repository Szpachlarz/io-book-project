using io_book_project.Data;
using io_book_project.Interfaces;
using io_book_project.Models;
using io_book_project.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace io_book_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
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

        public async Task <IActionResult> BooksPage(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            return View(book);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}