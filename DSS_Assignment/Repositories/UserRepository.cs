using DSS_Assignment.Data;
using DSS_Assignment.Models;
using DSS_Assignment.Repositories.Interfaces;

namespace DSS_Assignment.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

		public User? GetUser(string name, string password)
		{
            return _dbContext.Users.FirstOrDefault(user => user.Name == name && user.Password == password);
		}

		public User? GetUserByID(int id)
		{
            User? user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            return user;
		}
	}
}
