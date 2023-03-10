using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Domain.Entities;
using PayPortal.Infrastructure.Persistence.Contexts;

namespace PayPortal.Infrastructure.Persistence.Repositories
{
    public class BeneficieryRepository : GenericRepository<Beneficiary>, IBeneficiaryRepository
    {
        private readonly ApplicationContext _DbContext;

        public BeneficieryRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
