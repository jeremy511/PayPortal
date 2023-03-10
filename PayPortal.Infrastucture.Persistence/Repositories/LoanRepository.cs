using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Domain.Entities;
using PayPortal.Infrastructure.Persistence.Contexts;

namespace PayPortal.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        private readonly ApplicationContext _DbContext;

        public LoanRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
