using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Domain.Entities;
using PayPortal.Infrastructure.Persistence.Contexts;

namespace PayPortal.Infrastructure.Persistence.Repositories
{

    public class PaymentRepository : GenericRepository<Payments>, IPaymentRepository
    {
        private readonly ApplicationContext _DbContext;

        public PaymentRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
