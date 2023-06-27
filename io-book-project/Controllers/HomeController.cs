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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using io_book_project.Utils;

namespace io_book_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        public HomeController(AppDbContext context, ILogger<HomeController> logger, AppDbContext dbContext, IBookRepository bookRepository, IAuthorRepository authorRepository, IPublisherRepository publisherRepository, ICategoryRepository categoryRepository, IReviewRepository reviewRepository, IUserRepository userRepository)
        {
            _context = context;
            _logger = logger;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index (string id, int pg=1)
        {
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            //ViewData["CurrentFilter"] = searchString;

            //IEnumerable<Book> books = await _bookRepository.GetAll();

            var books = from m in _context.Books
                         select m;

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    books = await _bookRepository.BookSearch(searchString);
            //}

            if (!string.IsNullOrEmpty(id))
            {
                //var catId = await _categoryRepository.GetIdByName(id);
                //books = await _bookRepository.GetByCategoryId(catId);
                books = books.Include(i => i.BookCategories).ThenInclude(i => i.Category).Where(i => i.BookCategories.Any(ba => ba.Category.Name == id));
            }

            //var books = from s in await _bookRepository.GetAll()
            //            select s;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    books = books.Where(s => s.Title.Contains(searchString));
            //}

            var books2 = await books.ToListAsync();

            const int pageSize = 3;
            if(pg < 1)
                pg = 1;
            int recsCount = books2.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = books2.Skip(recSkip).Take(pager.PageSize).ToArray();
            this.ViewBag.Pager = pager;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> NewBooks()
        {
            var newbooks = await _bookRepository.GetNew();
            return View(newbooks);
        }

        [HttpGet]
        public async Task <IActionResult> BookPage(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return View("Error");
            var authors = await _authorRepository.GetAuthorNames(id);
            var categories = await _categoryRepository.GetCategoryNames(id);
            var publisher = await _publisherRepository.GetByBookId(id);
            var reviews = await _reviewRepository.GetAllForThisBook(id);
            var userReviews = await _userRepository.GetAllForThisBook(id);
            bool alreadyFavourite = false;
            
            if (HttpContext.Session.GetString(Const.LOGGED_USER) != null)
            {
                var userId = HttpContext.Session.GetString(Const.USER_ID);
                alreadyFavourite = _userRepository.CheckIfItIsAlreadyFavourite(userId, book.Id);

            }
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
                Reviews = reviews,
                UserReviews = userReviews,
                AlreadyFavourite = alreadyFavourite,
            };
            return View(bookVM);
        }
        [HttpGet]
        public async Task<IActionResult> AuthorsPage(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return View("Error");
            var books = await _bookRepository.GetByAuthorId(id);
            
            var authorVM = new AuthorsPageViewModel
            {
                Names = author.Names,
                Surname = author.Surname,
                Nationality = author.Nationality,
                DateOfBirth = author.DateOfBirth,
                DateOfDeath = author.DateOfDeath,
                Books = books,
            };
            return View(authorVM);
        }

        public async Task<IActionResult> PublishersPage(int id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher == null) return View("Error");
            var books = await _bookRepository.GetByPublisherId(id);

            var publisherVM = new PublishersPageViewModel
            {
                Name = publisher.Name,
                Country = publisher.Country,
                City = publisher.City,
                Books = books,

            };
            return View(publisherVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> AddReview(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return View("Error");
            ViewBag.bookTitle=book.Title;
            ViewBag.bookId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewViewModel model, int id)
        {
            if(ModelState.IsValid)
            {
                string loggedUser = HttpContext.Session.GetString(Const.LOGGED_USER);
                var user = await _userRepository.GetUserByName(loggedUser);
                var review = new Review
                {
                    Text = model.Text,
                    UserId = user.Id,
                    BookId = id,
                    Rating = model.Rating,
                    CreatedAt = DateTime.Now
                };

                _reviewRepository.Add(review);
                _reviewRepository.Save();
                return RedirectToAction($"BookPage", "Home", new {id});
            }
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return View("Error");
            ViewBag.Title = book.Title;
            ViewBag.BookId = book.Id;
            return View();
        }
        public async Task<IActionResult> AddToFavourites(int id)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = HttpContext.Session.GetString(Const.USER_ID);
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return View("Error");

            var favourite = new UserFavourite
            {
                UserId = userId,
                BookId = book.Id
            };

            _userRepository.AddFavourite(favourite);
            _userRepository.Save();

            return RedirectToAction($"BookPage", "Home", new {id});
        }
        public async Task<IActionResult> RemoveFromFavourites(int id)
        {
            var userId = HttpContext.Session.GetString(Const.USER_ID);
            var book = await _bookRepository.GetByIdAsync(id);

            _userRepository.RemoveFavourite(userId, book.Id);
            _userRepository.Save();

            return RedirectToAction($"BookPage", "Home", new { id });
        }
    }
}