using DSS_Assignment.Models;

namespace DSS_Assignment.Repositories.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAll();
        Task<Article> GetByIdAsync(int id);
        public void AddArticle(Article newarticle);
        public bool DeleteArticle(Article article, int userID);
    }
}
