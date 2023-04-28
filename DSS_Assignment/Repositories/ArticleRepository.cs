using DSS_Assignment.Data;
using DSS_Assignment.Models;
using DSS_Assignment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DSS_Assignment.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ArticleRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void AddArticle(Article newarticle)
        {
            _dbContext.Articles.Add(newarticle);
            _dbContext.SaveChanges();            
        }

        public void DeleteArticle(Article article, int userID)
        {
            if (userID == article.UserId)
            { 
                _dbContext.Articles.Remove(article);
                _dbContext.SaveChanges();
                return;
            }
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _dbContext.Articles.ToListAsync();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _dbContext.Articles.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
