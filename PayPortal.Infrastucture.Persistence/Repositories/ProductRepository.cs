using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Domain.Entities;
using PayPortal.Infrastructure.Persistence.Contexts;


namespace PayPortal.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Products>, IProductRepository
    {
        private readonly ApplicationContext _DbContext;

        public ProductRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
