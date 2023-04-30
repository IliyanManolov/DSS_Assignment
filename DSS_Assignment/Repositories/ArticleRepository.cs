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

        //Incrases the amount of comments that the article has in the database
        public void AddArticleCommentById(int id)
        {
            Article? article = _dbContext.Articles.FirstOrDefault(x => x.Id == id);
            article.CommentsAmount++;
            //_dbContext.Articles.Update(article);
            _dbContext.SaveChanges();
            return;
        }

        public bool DeleteArticle(Article article, int userID)
        {
            if (userID == article.UserId)
            {
                _dbContext.Articles.Remove(article);
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _dbContext.Articles.ToListAsync();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _dbContext.Articles.FirstOrDefaultAsync(i => i.Id == id);
        }

        //Decreases the amount of comments that the article has in the database
        public void RemoveArticleCommentById(int id)
        {
            Article? article = _dbContext.Articles.FirstOrDefault(x => x.Id == id);
            article.CommentsAmount--;
            //_dbContext.Articles.Update(article);
            _dbContext.SaveChanges();
            return;
        }
    }
}
