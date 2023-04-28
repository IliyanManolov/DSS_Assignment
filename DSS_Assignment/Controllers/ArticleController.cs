using DSS_Assignment.Data;
using DSS_Assignment.Models;
using DSS_Assignment.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Assignment.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IArticleRepository _articleRepository;
        public ArticleController(ApplicationDBContext context,IArticleRepository articleRepository)
        {
            _dbContext = context;
            _articleRepository = articleRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            Article article = await _articleRepository.GetByIdAsync(id);
            return View(article);
        }

        public IActionResult WriteArticle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WriteArticle(Article article)
        {
            article.CommentsAmount = 0;

            article.UserId = (int)HttpContext.Session.GetInt32("ID");
            if (!ModelState.IsValid)
            {
                return View(article);
            }
            _articleRepository.AddArticle(article);
            return RedirectToAction("Index", "Home", new { area = "Controllers"});
        }
    }
}
