using DSS_Assignment.Data;
using DSS_Assignment.Models;
using DSS_Assignment.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DSS_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _dbContext;
        private readonly IArticleRepository _articleRepository;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext context, IArticleRepository articleRepository)
        {
            _logger = logger;
            _dbContext = context;
            _articleRepository = articleRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Article> articles = await _articleRepository.GetAll();
            return View(articles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}