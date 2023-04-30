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
        public async Task<IActionResult> ShowArticleComments(int id)
        {
            IEnumerable<Comment> comments = await _commentRepository.GetAllByArticle(id);
            comments = await _commentRepository.SortCommentsByCreation(comments);
            return View(comments);
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            Comment? comment = await _commentRepository.GetCommentById(id);
            var sessionID = HttpContext.Session.GetInt32("ID");
            if (sessionID == null)
            {
                ViewBag.DeleteStatus = "You must be logged in to delete a comment!";
                return View(comment);
            }
            ViewBag.Session = true;
            if (!_commentRepository.DeleteComment(comment, (int)sessionID))
            {
                ViewBag.DeleteStatus = "You cannot delete someone else's article!";
                return View(comment);
            }
            else
            {
                _articleRepository.RemoveArticleCommentById(comment.ArticleId);
                ViewBag.DeleteStatus = "Your comment has been successfully deleted!";
                return View(comment);
            }
        }
    }
}
