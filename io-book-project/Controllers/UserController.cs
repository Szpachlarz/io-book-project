﻿using io_book_project.Data;
using io_book_project.Interfaces;
using io_book_project.Models;
using io_book_project.Repository;
using io_book_project.Utils;
using io_book_project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Security.Claims;
using static System.Reflection.Metadata.BlobBuilder;

namespace io_book_project.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly UserManager<User> _userManager;
        public UserController(ILogger<HomeController> logger, IBookRepository bookRepository, IUserRepository userRepository, IReviewRepository reviewRepository, UserManager<User> userManager)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _reviewRepository = reviewRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favourites = await _userRepository.GetAFewFavourites(userId);
            var reviews = await _reviewRepository.GetAFewReviews(userId);
            //foreach (var book in favourites)
            //{
            //    int id = book.Id;
            //    var bookAuthors = await _authorRepository.GetAuthorNames(id);
            //    authors.Add((List<Author>)bookAuthors);
            //}
            var userVM = new UserPageViewModel
            {
                FavouriteBooks = favourites,
                Reviews = reviews
            };
            return View(userVM);
        }
        [HttpGet]
        public async Task<IActionResult> BooksList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favourites = await _userRepository.GetAllFavourites(userId);
            var favouriteVM = new UserFavouriteViewModel
            {
                FavouriteBooks = favourites,
            };
            return View(favouriteVM);
        }
        [HttpGet]
        public async Task <IActionResult> ReviewsList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reviews = await _reviewRepository.GetAllReviews(userId);
            var reviewsVM = new UserReviewsViewModel
            {
                Reviews = reviews,
            };
            return View(reviewsVM);
        }
    }
}
