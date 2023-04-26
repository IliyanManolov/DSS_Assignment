using DSS_Assignment.Models;

namespace DSS_Assignment.Services
{
    public class AuthManager
    {
        private static readonly string ID = "id";
        private IHttpContextAccessor _contextAccessor;
        public AuthManager(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void SetSession(User user)
        {
            _contextAccessor.HttpContext.Session.SetInt32(AuthManager.ID, user.Id);
        }

        public bool IsAuthenticated()
        {
            var httpContext = _contextAccessor.HttpContext;
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
