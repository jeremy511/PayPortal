using PayPortal.Core.Application.Dtos.Account;
using PayPortal.Core.Application.Interfaces.Services.Account;
using PayPortal.Core.Application.ViewModels.Users;
using AutoMapper;
using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Application.Interfaces.Services.Users;
using PayPortal.Core.Application.ViewModels.Admin;
using PayPortal.Core.Application.ViewModels.SavingsAccount;
using PayPortal.Core.Domain.Entities;
using PayPortal.Core.Application.Enums;
using PayPortal.Core.Application.Interfaces.Services;

namespace PayPortal.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IProductService _productService;
        private readonly ISavingsAccountRepository _savings;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper, IProductService productService, ISavingsAccountRepository savings)
        {
            _accountService = accountService;
            _mapper = mapper;
            _productService = productService;   
            _savings = savings;
        }


        public async Task<AuthenticationResponse> LogInAsyn(LoginViewModel vm)
        {
            AuthenticationRequest request = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse response = await _accountService.AuthenticateAsync(request);
            return response;
        }

        public async Task<RegisterResponse> Register(SaveUserViewModel vm)
        {
            RegisterRequest request = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterUser(request);
        }

        public async Task<string> ConfirmEmailAsync(string UserId, int action)
        {
            return await _accountService.ConfirmAccountAsync(UserId, action);
        }

        public async Task SignOut()
        {
            await _accountService.LogOut();
        }


        public async Task<List<AdminUserListViewModel>> GetAll()
        {
            return await _accountService.GetAll();
        }

        public async Task<string> GetUserRoles(string id)
        {
          return  await _accountService.GetUserRoles(id);
        }

        public async Task Edit(EditUserViewModel vm, string role)
        {
            RegisterRequest request = _mapper.Map<RegisterRequest>(vm);
           
            if (role == Roles.Client.ToString())
            {
                var SavingsList = await _savings.GetAllViewModel();
                var MySaving = SavingsList.Where(acc => acc.OwnerUserName == vm.UserName).Select(acc => new SaveSavingsViewModel
                {
                    Id = acc.Id,
                }).FirstOrDefault();

                SavingsAccount accountService = await _savings.GetViewModelById(MySaving.Id);
                if (vm.AditionalAmount != 0 || vm.AditionalAmount == null) {
                    accountService.Amount = (double)(accountService.Amount + vm.AditionalAmount);
                    accountService.OwnerName = vm.FirstName +" "+ vm.LastName;
                    await _savings.UpdateAsyn(accountService, accountService.Id); 
                }
            }
            
            await _accountService.Edit(request, role);
        }

        public async Task<EditUserViewModel> GetUserById(string id)
        {
            RegisterRequest user = await _accountService.GetUserById(id);
            EditUserViewModel vm = _mapper.Map<EditUserViewModel>(user);
            return vm;
        }

        public async Task<EditUserViewModel> GetUserByName(string name)
        {
            RegisterRequest user = await _accountService.GetUserByName(name);
            EditUserViewModel vm = _mapper.Map<EditUserViewModel>(user);
            return vm;
        }

        public async Task<PassUserViewModel> ChangePass(PassUserViewModel vm)
        {
            RegisterRequest request = new();
            request.Password = vm.Password;
            request.Id = vm.Id;
            await _accountService.ChangePass(request);
            vm.HasError = false;
            vm.Error = "Contraseña Actualizada Correctamente";
            vm.Id = vm.Id;
            return vm;
        }

        public async Task<PassUserViewModel> GetChengePassId(string id)
        {
            RegisterRequest user = await _accountService.GetUserById(id);
            PassUserViewModel vm = new();

            vm.HasError = false;
            vm.Id = user.Id;
            return vm;
        }


    }
}
