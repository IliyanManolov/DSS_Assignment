using DSS_Assignment.Data;
using DSS_Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Assignment.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDBContext _context;
        public ArticleController(ApplicationDBContext context)
        {
            _context= context;
        }
        public IActionResult Index(int id)
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            Article article = _context.Articles.FirstOrDefault(c => c.Id == id);
            return View(article);
        }

        public IActionResult WriteArticle()
        {
            return View();
        }
    }
}
