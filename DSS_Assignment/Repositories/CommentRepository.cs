using DSS_Assignment.Data;
using DSS_Assignment.Models;
using DSS_Assignment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DSS_Assignment.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CommentRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void AddComment(Comment newcomment)
        {
            _dbContext.Comments.Add(newcomment);
            _dbContext.SaveChanges();
        }

        public bool DeleteComment(Comment comment, int userid)
        {
            if (comment.UserId == userid)
            {
                _dbContext.Comments.Remove(comment);
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public async Task<IEnumerable<Comment>> GetAllByArticle(int id)
        {
            return await _dbContext.Comments.Where(i => i.ArticleId == id).ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _dbContext.Comments.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Comment>> SortCommentsByCreation(IEnumerable<Comment> commentslist)
        {
            commentslist = commentslist.OrderBy(i => i.Created).Reverse<Comment>();
            return commentslist;
        }
    }
}
