using PayPortal.Core.Application.Dtos.Account;
using PayPortal.Core.Application.Helpers;
using PayPortal.Core.Application.ViewModels.Users;
using PayPortal.Core.Application.Interfaces.Services.Users;
using PayPortal.Core.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.PayPortal.Middlewares;
using PayPortal.Core.Application.Enums;
using PayPortal.Core.Application.Interfaces.Services;
 

namespace PayPortal.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISavingsAccountService _savings;
        private readonly IProductService _productService;
        private readonly ValidateUserSession _validateUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;


        public UserController(IUserService userService, ISavingsAccountService savings, IProductService productService, ValidateUserSession userSession, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _savings = savings;
            _productService = productService;
            _validateUser = userSession;
            _httpContextAccessor = httpContextAccessor;

            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");


        }
        public async Task<IActionResult> Index()
        {
            if (_validateUser.HasUser())
            {
                if (_userViewModel.Roles.Any(r => r == "Admin"))
                {
                    return RedirectToRoute(new { Controller = "Admin", Action = "Index" });
                }
                else
                {
                    return RedirectToRoute(new { Controller = "Client", Action = "Index" });

                }
            }


            var CheckForProducts = await _productService.GetAllViewModel();
            if (CheckForProducts.Count == 0)
            {


                await _productService.AddAsync();
                EditUserViewModel model = await _userService.GetUserByName("clientuser");
                SaveUserViewModel saveUser = new();
                saveUser.StartingAmount = 0;
                saveUser.LastName = model.LastName;
                saveUser.FirstName = model.FirstName;
                saveUser.UserName = model.UserName;
                await _savings.AddMainAsync(saveUser);
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {



            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LogInAsyn(vm);

            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                if (userVm.Roles.Count == 1)
                {
                    return RedirectToRoute(new { Controller = "Client", Action = "Index" });

                }

                return RedirectToRoute(new { Controller = "Admin", Action = "Index" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }


        }

        public async Task<IActionResult> LogOut()
        {

            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Index" });
            }

            await _userService.SignOut();
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]

        public IActionResult Register()
        {

            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Index" });
            }
            return View(new SaveUserViewModel());
        }

        [HttpPost]

        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {

            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.UserType = "Admin";
                return View(vm);
            }

            if (vm.StartingAmount == null)
            {
                vm.StartingAmount = 0;
            }



            RegisterResponse response = await _userService.Register(vm);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            if (!response.HasError && vm.UserType == Roles.Client.ToString())
            {
                await _savings.AddMainAsync(vm);
            }

            return RedirectToRoute(new { Controller = "Admin", Action = "UserAdministration", response.Error });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<IActionResult> Edit(string id, string messag)
        {


            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Index" });
            }
            ViewBag.HasMessage = false;

            if (id == _userViewModel.Id)
            {
                string message = "No puedes Editar tu perfil ya que estas logeado en el";
                return RedirectToRoute(new { Controller = "Admin", Action = "UserAdministration", message });

            }

            if (messag != null)
            {
                ViewBag.HasMessage = true;
                ViewBag.Message = messag;
            }

            ViewBag.UserType = await _userService.GetUserRoles(id);

            return View("Edit", await _userService.GetUserById(id));
        }

        [HttpPost]

        public async Task<IActionResult> Edit(EditUserViewModel vm)
        {

            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            ViewBag.HasMessage = false;


            await _userService.Edit(vm, vm.UserType);
            string message = "Datos Actualizados Correctamente!";
            return RedirectToRoute(new { Controller = "Admin", Action = "UserAdministration", message });

        }

        [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<IActionResult> ChangePass(string id)
        {

            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Index" });
            }
            return View(await _userService.GetChengePassId(id));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePass(PassUserViewModel vm)
        {

            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            PassUserViewModel passUser = await _userService.ChangePass(vm);
            string message = passUser.Error;
            return RedirectToRoute(new { Controller = "User", Action = "Edit", passUser.Id, message });

        }

        [Authorize]

        public IActionResult AccessDenied()
        {
            if (!_validateUser.HasUser())
            {
                return RedirectToRoute(new { Controller = "Admin", Action = "Index" });

            }

            if (_userViewModel.Roles.Any(a => a == Roles.Admin.ToString()))
            {
                ViewBag.Controller = "Admin";
            }
            else
            {
                ViewBag.Controller = "Client";
            }



            return View();
        }


    }
}
