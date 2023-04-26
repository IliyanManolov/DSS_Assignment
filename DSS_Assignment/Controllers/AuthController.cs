using DSS_Assignment.Repositories;
using DSS_Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Assignment.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthManager _authManager;
        private readonly IUserRepository _userRepository;

        public AuthController(AuthManager authManager, IUserRepository userRepository)
        {
            _authManager = authManager;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
