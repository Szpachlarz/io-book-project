using io_book_project.Data;
using io_book_project.Models;
using io_book_project.ViewModels;
using io_book_project.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace io_book_project.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _context;
        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null) 
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        var role = await _userManager.GetRolesAsync(user);
                        HttpContext.Session.SetString(Utils.Const.LOGGED_USER, user.UserName);
                        HttpContext.Session.SetString(Utils.Const.USER_ROLE, role[0]);
                        if (role[0]=="admin")
                        {
                            return RedirectToAction("Index", "Admin");                            
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                TempData["Error"] = "Błędne dane logowania";
                return View(loginViewModel);
            }
            TempData["Error"] = "Błędne dane logowania";
            return View(loginViewModel);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
