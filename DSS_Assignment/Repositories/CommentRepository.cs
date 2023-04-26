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

        public void AddComment(Comment newcomment, User user)
        {
            _dbContext.Comments.Add(newcomment);
            _dbContext.SaveChanges();
        }

        public void DeleteComment(Comment comment, User user)
        {
            if (user.Id != comment.UserId)
            {
                return;
            }
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Comment>> GetAllByArticle(int id)
        {
            return await _dbContext.Comments.Where(i => i.ArticleId == id).ToListAsync();
        }

        public List<Comment> SortCommentsByCreation(List<Comment> list)
        {
            //should work?
            list = (List<Comment>)list.OrderBy(i => i.Created);
            return list;
        }
    }
}
