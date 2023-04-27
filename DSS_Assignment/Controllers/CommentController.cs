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
        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository)
        {
            _dbContext = context;
            _commentRepository = commentRepository;
        }
        public IActionResult WriteComment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WriteComment(Comment comment, Article article, User user)
        {
            comment.ArticleId = article.Id;
            comment.UserId = user.Id;
            if (!ModelState.IsValid)
            {
                return View();
            }
            _commentRepository.AddComment(comment);
            return RedirectToAction("Index", "Home", new { area = "Controllers" });
        }
    }
}
