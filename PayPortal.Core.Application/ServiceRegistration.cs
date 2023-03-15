using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayPortal.Core.Application.Interfaces.Services.Users;
using PayPortal.Core.Application.Interfaces.Services;
using PayPortal.Core.Application.Services;
using System.Reflection;


namespace PayPortal.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddTransient<IUserService, UserService>();
            service.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<ISavingsAccountService, SavingsAccountService>();
            service.AddTransient<IBeneficiaryService, BeneficiaryService>();
            service.AddTransient<ICreditService, CreditService>();
            service.AddTransient<ILoanService, LoanService>();





        }
    }
}
