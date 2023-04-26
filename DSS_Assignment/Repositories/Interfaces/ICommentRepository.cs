using DSS_Assignment.Models;

namespace DSS_Assignment.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllByArticle(int id);
        public void AddComment(Comment newcomment, User user);
        public void DeleteComment(Comment comment, User user);
        public List<Comment> GetArticleComments(Article article);
        public List<Comment> SortCommentsByCreation(List<Comment> list);
    }
}
