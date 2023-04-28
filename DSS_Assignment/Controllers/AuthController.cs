using DSS_Assignment.Models;
using DSS_Assignment.Repositories.Interfaces;
using DSS_Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Assignment.Controllers
{
    public class AuthController : Controller
    {
        //private readonly AuthManager _authManager;
        private readonly IUserRepository _userRepository;

        //public AuthController(AuthManager authManager, IUserRepository userRepository)
        //{
        //    _authManager = authManager;
        //    _userRepository = userRepository;
        //}
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Login(User user)
        {
            if (user == null)
            {
                ViewBag.Error = "There is no user";
                return View(user);
            }
            User currentUser = _userRepository.GetUser(user.Name, user.Password);
            if (currentUser == null)
            {
                ViewBag.Error = "User not found";
                return View(user);
            }
            HttpContext.Session.SetInt32("ID", currentUser.Id); //CASE SENSITIVE
            
			return RedirectToAction("WriteArticle", "Article", new { area = "Controllers" });
			//return RedirectToAction("Index", "Home", new { area = "Controllers" });
		}

		public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            HttpContext.Session.SetInt32("id", user.Id);
            _userRepository.AddUser(user);
            ViewBag.CurrentUser = user;
			return RedirectToAction("Index", "Home", new { area = "Controllers" });
		}
	}
}
