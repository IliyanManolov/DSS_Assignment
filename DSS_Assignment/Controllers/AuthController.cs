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

        //[HttpPost]
        //public async Task<IActionResult> Login(User user)
        //{
        //    if (user == null)
        //    {
        //        ViewBag.Error = "There is no user";
        //        return View(user);
        //    }
        //    User? user = _userRepository.
        //}
        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            _userRepository.AddUser(user);
			return RedirectToAction("Index", "Home", new { area = "Controllers" });
		}
	}
}
