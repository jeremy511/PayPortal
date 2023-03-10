using PayPortal.Infrastructure.Persistence.Repositories;
using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Domain.Entities;
using PayPortal.Infrastructure.Persistence.Contexts;

namespace PayPortal.Infrastructure.Persistence.Repositories
{

    public class SavingsAccountRepository : GenericRepository<SavingsAccount>, ISavingsAccountRepository
    {
        private readonly ApplicationContext _DbContext;

        public SavingsAccountRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
