using PayPortal.Core.Application.Dtos.Account;
using PayPortal.Core.Application.ViewModels.Users;
using PayPortal.Core.Application.ViewModels.Admin;

namespace PayPortal.Core.Application.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string UserId, int action);
        Task<AuthenticationResponse> LogInAsyn(LoginViewModel vm);
        Task<RegisterResponse> Register(SaveUserViewModel vm);
        Task SignOut();
        Task<List<AdminUserListViewModel>> GetAll();
        Task<string> GetUserRoles(string id);
        Task Edit(EditUserViewModel vm, string role);

        Task<EditUserViewModel> GetUserById(string id);

        Task<EditUserViewModel> GetUserByName(string name);
        Task<PassUserViewModel> GetChengePassId(string id);
        Task<PassUserViewModel> ChangePass(PassUserViewModel vm);
    }
}