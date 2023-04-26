using DSS_Assignment.Data;
using DSS_Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Assignment.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public ArticleController(ApplicationDBContext context)
        {
            _dbContext= context;
        }
        public IActionResult Index(int id)
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            Article article = _dbContext.Articles.FirstOrDefault(c => c.Id == id);
            return View(article);
        }

        public IActionResult WriteArticle()
        {
            return View();
        }
    }
}
