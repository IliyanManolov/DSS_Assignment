using DSS_Assignment.Models;
using DSS_Assignment.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Assignment.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

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
            HttpContext.Session.SetInt32("ID", currentUser.Id);
            
			return RedirectToAction("Index", "Home", new { area = "Controllers" });
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
                if (user.Name == null)
                    ViewBag.RegisterError = "No name given";
                else if (user.Password == null)
                    ViewBag.RegisterError = "No password given";
                return View(user);
            }
            HttpContext.Session.SetInt32("id", user.Id);
            _userRepository.AddUser(user);
            ViewBag.CurrentUser = user;
			return RedirectToAction("Index", "Home", new { area = "Controllers" });
		}
	}
}
