using PayPortal.Core.Application.Helpers;
using PayPortal.Core.Application.ViewModels.Users;

namespace WebApp.PayPortal.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            if (userViewModel == null)
            {
                return false;
            }
            return true;
        }
    }
}
