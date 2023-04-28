using DSS_Assignment.Data;
using DSS_Assignment.Models;

namespace DSS_Assignment.Services
{
    public class AuthManager
    {
        private static readonly string ID = "id";
        private readonly IHttpContextAccessor _httpContext;
        private readonly ApplicationDBContext _dbContext;
        public AuthManager(ApplicationDBContext dbContext, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _dbContext = dbContext;
        }

        public void SetSession(User user)
        {
            _httpContext.HttpContext.Session.SetInt32(AuthManager.ID, user.Id);
        }

        public bool IsAuthenticated()
        {
            var httpContext = _httpContext.HttpContext;
            if (httpContext == null)
                return false;
            var id = httpContext.Session.GetInt32(AuthManager.ID);
            if (id == null)
                return false;
            //GET THE user?

            return true;
        }
    }
}
