using DSS_Assignment.Data;
using DSS_Assignment.Models;
using DSS_Assignment.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Assignment.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ICommentRepository _commentRepository;
        private readonly IArticleRepository _articleRepository;
        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository, IArticleRepository articleRepository)
        {
            _dbContext = context;
            _commentRepository = commentRepository;
            _articleRepository = articleRepository;
        }
        public IActionResult WriteComment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WriteComment(Comment comment, int id)
        {
            //Database breaks unless you input an ID manually. Real ID is changed by the database itself
            comment.Id = 0;
            if (HttpContext.Session.GetInt32("ID") == null)
            {
                ViewBag.Error = "You must be logged in to comment!";
                return View(comment);
            }
            comment.UserId = (int)HttpContext.Session.GetInt32("ID");
            Article? article = await _articleRepository.GetByIdAsync(id);
            comment.ArticleId = article.Id;
            comment.Created = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "No text entered!";
                return View();
            }
            _articleRepository.AddArticleCommentById(id);
            _commentRepository.AddComment(comment);
            return RedirectToAction("Index", "Home", new { area = "Controllers" });
        }
    }
}
