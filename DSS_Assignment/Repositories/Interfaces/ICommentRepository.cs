using DSS_Assignment.Models;

namespace DSS_Assignment.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllByArticle(int id);
        public void AddComment(Comment newcomment);
        public void DeleteComment(Comment comment);
        public List<Comment> SortCommentsByCreation(List<Comment> list);
    }
}
