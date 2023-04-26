using DSS_Assignment.Data;
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
        public IActionResult Index()
        {
            return View();
        }
    }
}
