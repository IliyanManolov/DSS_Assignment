using DSS_Assignment.Data;
using DSS_Assignment.Models;

namespace DSS_Assignment.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void StoreUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
