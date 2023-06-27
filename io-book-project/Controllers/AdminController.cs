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
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;
using System.Linq;

namespace io_book_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        public AdminController(AppDbContext context, IBookRepository bookRepository, IPublisherRepository publisherRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }

            var bookCount = await _bookRepository.GetCountAsync();
            var authorCount = await _authorRepository.GetCountAsync();
            var publisherCount = await _publisherRepository.GetCountAsync();
            var categoryCount = await _categoryRepository.GetCountAsync();
            var userCount = await _userRepository.GetCountAsync();

            var adminIndex = new AdminIndexViewModel
            {
                BookCount = bookCount,
                AuthorCount = authorCount,
                PublisherCount = publisherCount,
                CategoryCount = categoryCount,
                UserCount = userCount
            };

            return View(adminIndex);
        }

        [HttpGet]
        public IActionResult AddAuthor()
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthorViewModel model)
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

                return RedirectToAction("AddAuthor", "Admin");
            }

            return View("AddAuthor", model);
        }

        public async Task<IActionResult> AuthorList(int pg=1)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var authors = await _authorRepository.GetAll();

                const int pageSize = 10;
                if (pg < 1)
                    pg = 1;
                int recsCount = authors.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = authors.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;

                //return View(authors);
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        public async Task<IActionResult> AuthorDetails(int id)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                return View(author);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AuthorEdit(int id)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return View("Error");
        
            var model = new AddAuthorViewModel()
            {
                Names = author.Names,
                Surname = author.Surname,
                Nationality = author.Nationality,
                DateOfBirth = author.DateOfBirth,
                DateOfDeath = author.DateOfDeath
            };

            return View(model);            
        }

        [HttpPost]
        public async Task<IActionResult> AuthorEdit(int id, AddAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Nie udało się zedytować");
                return View(model);
            }
            var author = await _authorRepository.GetByIdAsyncNoTracking(id);

            if (author == null)
            {
                return View("Error");
            }

            var author2 = new Author
            {
                Id = id,
                Names = model.Names,
                Surname = model.Surname,
                Nationality = model.Nationality,
                DateOfBirth = model.DateOfBirth,
                DateOfDeath = model.DateOfDeath,
            };

            _authorRepository.Update(author2);
            return RedirectToAction("AuthorList");
        }
        public async Task<IActionResult> AuthorDelete(int id)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                _authorRepository.Delete(author);
                _authorRepository.Save();
                return RedirectToAction("AuthorList", "Admin");


            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var publishers = await _publisherRepository.GetAll();
            ViewData["Publishers"] = new SelectList((System.Collections.IEnumerable)publishers, "Id", "Name");

            var authors = await _authorRepository.GetAll();
            var authorList = authors.Select(a => new { Id = a.Id, FullName = $"{a.Names} {a.Surname}" }).ToList();
            ViewData["Authors"] = new MultiSelectList((System.Collections.IEnumerable)authorList, "Id", "FullName");

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
                    BookAuthors = new List<BookAuthor>(),
                    BookCategories = new List<BookCategory>()
                };

                foreach (var authorId in model.AuthorId)
                {
                    var bookAuthor = new BookAuthor
                    {
                        BookId = book.Id,
                        AuthorId = authorId
                    };
                    book.BookAuthors.Add(bookAuthor);
                }
                foreach (var categoryId in model.CategoryId)
                {
                    var bookCategory = new BookCategory
                    {
                        BookId = book.Id,
                        CategoryId = categoryId
                    };
                    book.BookCategories.Add(bookCategory);
                }
                _bookRepository.Add(book);
                _bookRepository.Save();

                return RedirectToAction("BookList", "Admin");
            }

            return View("AddBook", model);
        }
        [HttpGet]
        public async Task<IActionResult> BookList(int pg = 1)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var books = await _bookRepository.GetAll();
                //const int pageSize = 5;
                //if (pg < 1)
                //    pg = 1;
                //int recsCount = books.Count();
                //var pager = new Pager(recsCount, pg, pageSize);
                //int recSkip = (pg - 1) * pageSize;
                //var data = books.Skip(recSkip).Take(pager.PageSize).ToList();
                //this.ViewBag.Pager = pager;
                List<List<Author>> authors = new List<List<Author>>();
                foreach (var book in books) 
                {
                    int id = book.Id;
                    var bookAuthors = await _authorRepository.GetAuthorNames(id);
                    authors.Add((List<Author>)bookAuthors);
                }

                var bookListVM = new BookListViewModel
                {
                    Books = books,
                    Authors = authors,
                    //Data = data,
                };
                return View(bookListVM);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        public async Task<IActionResult> BookDetails(int id)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookRepository.GetByIdAsync(id);
            var bookauthor = await _authorRepository.GetByIdAsync(id);
            return View(book);
            
        }
        [HttpGet]
        public async Task<IActionResult> BookEdit(int id)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var publishers = await _publisherRepository.GetAll();
            ViewData["Publishers"] = new SelectList((System.Collections.IEnumerable)publishers, "Id", "Name");

            var authors = await _authorRepository.GetAll();
            var authorList = authors.Select(a => new { Id = a.Id, FullName = $"{a.Names} {a.Surname}" }).ToList();
            ViewData["Authors"] = new MultiSelectList((System.Collections.IEnumerable)authorList, "Id", "FullName");

            var categories = await _categoryRepository.GetAll();
            ViewData["Categories"] = new SelectList((System.Collections.IEnumerable)categories, "Id", "Name");

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return View("Error");

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> BookEdit(int id, AddBookViewModel model)
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
                    BookAuthors = new List<BookAuthor>(),
                    BookCategories = new List<BookCategory>()
                };

                foreach (var authorId in model.AuthorId)
                {
                    var bookAuthor = new BookAuthor
                    {
                        BookId = book.Id,
                        AuthorId = authorId
                    };
                    book.BookAuthors.Add(bookAuthor);
                }
                foreach (var categoryId in model.CategoryId)
                {
                    var bookCategory = new BookCategory
                    {
                        BookId = book.Id,
                        CategoryId = categoryId
                    };
                    book.BookCategories.Add(bookCategory);
                }
                _bookRepository.Update(book);

                return RedirectToAction("BookList");
            }
            return View("BookEdit", model);
        }

        public async Task<IActionResult> BookDelete(int id)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                _bookRepository.Delete(book);
                _bookRepository.Save();
                return RedirectToAction("BookList", "Admin");

                
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                };

                _categoryRepository.Add(category);
                _categoryRepository.Save();

                return RedirectToAction("AddCategory", "Admin");
            }

            return View("AddCategory", model);

        }

        public async Task<IActionResult> CategoryList(int pg = 1)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var categories = await _categoryRepository.GetAll();
                const int pageSize = 10;
                if (pg < 1)
                    pg = 1;
                int recsCount = categories.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = categories.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                //return View(categories);
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CategoryEdit(int id)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return View("Error");

            var model = new AddCategoryViewModel()
            {
                Name = category.Name,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryEdit(int id, AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Nie udało się zedytować");
                return View(model);
            }
            var category = await _categoryRepository.GetByIdAsyncNoTracking(id);

            if (category == null)
            {
                return View("Error");
            }

            var category2 = new Category
            {
                Id = id,
                Name = model.Name,
            };

            _categoryRepository.Update(category2);
            return RedirectToAction("CategoryList");
        }

        public async Task<IActionResult> CategoryDelete(int id)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                _categoryRepository.Delete(category);
                _categoryRepository.Save();
                return RedirectToAction("CategoryList", "Admin");
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddPublishingHouse()
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPublishingHouse(AddPublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var publisher = new Publisher
                {
                    Name = model.Name,
                    Country = model.Country,
                    City = model.City,

                };

                _publisherRepository.Add(publisher);
                _publisherRepository.Save();

                return RedirectToAction("AddPublishingHouse", "Admin");
            }

            return View("AddPublishingHouse", model);
        }

        public async Task<IActionResult> PublishingHouseList(int pg = 1)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var publishers = await _publisherRepository.GetAll();
                const int pageSize = 10;
                if (pg < 1)
                    pg = 1;
                int recsCount = publishers.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = publishers.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                //return View(publishers);
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PublishingHouseEdit(int id)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher == null) return View("Error");

            var model = new AddPublisherViewModel()
            {
                Name = publisher.Name,
                Country = publisher.Country,
                City = publisher.City,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> PublishingHouseEdit(int id, AddPublisherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Nie udało się zedytować");
                return View(model);
            }
            var publisher = await _publisherRepository.GetByIdAsyncNoTracking(id);

            if (publisher == null)
            {
                return View("Error");
            }

            var publisher2 = new Publisher
            {
                Id = id,
                Name = model.Name,
                Country = model.Country,
                City = model.City
            };

            _publisherRepository.Update(publisher2);
            return RedirectToAction("PublishingHouseList");
        }
        public async Task<IActionResult> PublishingHouseDelete(int id)
        {
            if (HttpContext.Session.GetString(Const.USER_ROLE) != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var publisher = await _publisherRepository.GetByIdAsync(id);
                _publisherRepository.Delete(publisher);
                _publisherRepository.Save();
                return RedirectToAction("PublishingHouseList", "Admin");
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UserList(int pg=1)
        {
            var users = await _userRepository.GetAllUsers();
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = users.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = users.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            //return View(authors);
            return View(data);
        }
        [HttpPost]
        public IActionResult UserBan(string id)
        {
            _userRepository.UserBan(id);
            return RedirectToAction("UserList", "Admin");
        }
        [HttpPost]
        public IActionResult UserUnban(string id)
        {
            _userRepository.UserUnban(id);
            return RedirectToAction("UserList", "Admin");
        }
    }
}