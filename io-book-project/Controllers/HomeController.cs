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
using System.Security.Policy;

namespace io_book_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext, IBookRepository bookRepository, IAuthorRepository authorRepository, IPublisherRepository publisherRepository, ICategoryRepository categoryRepository, IReviewRepository reviewRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int pg=1)
        {
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            //ViewData["CurrentFilter"] = searchString;

            IEnumerable<Book> books = await _bookRepository.GetAll();

<<<<<<< HEAD
            const int pageSize = 9;
=======
            //var books = from s in await _bookRepository.GetAll()
            //            select s;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    books = books.Where(s => s.Title.Contains(searchString));
            //}

                const int pageSize = 3;
>>>>>>> 681c17abf0340495942e293c89aa8b236a10b32b
            if(pg < 1)
                pg = 1;
            int recsCount = books.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = books.Skip(recSkip).Take(pager.PageSize).ToArray();
            this.ViewBag.Pager = pager;

            //return View(books);
            return View(data);
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
<<<<<<< HEAD
            //var publisher = await _publisherRepository.GetByBookId(id);
=======
            var reviews = await _reviewRepository.GetAllForThisBook(id);
>>>>>>> 681c17abf0340495942e293c89aa8b236a10b32b
            var bookVM = new BookPageViewModel
            {
                Id = book.Id,
                Title = book.Title,
                CoverImagePath = book.CoverImagePath,
                Authors = authors,
                Publisher = publisher,
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
                Reviews = reviews
            };
            return View(bookVM);
        }
        [HttpGet]
        public async Task<IActionResult> AuthorsPage(int id)
        {

            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return View("Error");
            
            var authorVM = new AuthorsPageViewModel
            {
                Names = author.Names,
                Surname = author.Surname,
                Nationality = author.Nationality,
                DateOfBirth = author.DateOfBirth,
                DateOfDeath = author.DateOfDeath,
                
            };
            return View(authorVM);
        }

        public async Task<IActionResult> PublishersPage(int id)
        {

            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher == null) return View("Error");

            var publisherVM = new PublishersPageViewModel
            {
                Name = publisher.Name,
                Country=publisher.Country,
                City=publisher.City,   

            };
            return View(publisherVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}