using DSS_Assignment.Models;

namespace DSS_Assignment.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllByArticle(int id);
        public void AddComment(Comment newcomment);
        public bool DeleteComment(Comment comment, int userid);
        public Task<IEnumerable<Comment>> SortCommentsByCreation(IEnumerable<Comment> list);
        public Task<Comment> GetCommentById(int id);
    }
}
