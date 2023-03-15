using PayPortal.Core.Application.Dtos.Account;
using PayPortal.Core.Application.ViewModels.Admin;

namespace PayPortal.Core.Application.Interfaces.Services.Account
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, int token);
        Task LogOut();
        Task<RegisterResponse> RegisterUser(RegisterRequest request);

        Task<List<AdminUserListViewModel>> GetAll();

        Task<List<GettingAllUsers>> GetAllUsers();
        Task<string> GetUserRoles(string id);

        Task Edit(RegisterRequest request, string role);

        Task<RegisterRequest> GetUserById(string id);
        Task<RegisterRequest> GetUserByName(string name);
        Task ChangePass(RegisterRequest request);
    }
}