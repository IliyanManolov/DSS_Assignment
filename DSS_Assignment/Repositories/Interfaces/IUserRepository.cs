using DSS_Assignment.Models;

namespace DSS_Assignment.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public void AddUser(User user);
        public User? GetUser(string name, string password);
    }
}
