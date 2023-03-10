
using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Infrastructure.Persistence.Contexts;
using PayPortal.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PayPortal.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {
            #region "Contexts"

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("InMemoryDB"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m=> m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }

            #endregion

            #region "Services"

            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISavingsAccountRepository, SavingsAccountRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
            services.AddTransient<IBeneficiaryRepository, BeneficieryRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();





            #endregion

        }
    }
}
