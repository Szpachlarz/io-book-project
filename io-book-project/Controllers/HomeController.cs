using io_book_project.Data;
using io_book_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Dynamic;

namespace io_book_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            return books;
        }
        public List<BookAuthor> GetBookAuthors()
        {
            List<BookAuthor> bookAuthors = new List<BookAuthor>();
            return bookAuthors;
        }

        public IActionResult BooksPage(int id)
        {
             Book book = _context.Books.Include(a => a.Publisher).FirstOrDefault(x => x.Id == id);

             return View(book);

           // dynamic mymodel = new ExpandoObject();
           // mymodel.Book = GetBooks().FirstOrDefault(x => x.Id == id);
           // mymodel.BookAuthor = GetBookAuthors().FirstOrDefault(x => x.Id == id);
          //  return View(mymodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}