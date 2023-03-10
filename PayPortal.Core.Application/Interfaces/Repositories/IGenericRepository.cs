

namespace PayPortal.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity>
    {
        Task<Entity> AddAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllViewModel();
        Task<List<Entity>> GetAllWithIncludes(List<string> properties);
        Task<Entity> GetViewModelById(int id);
        Task<Entity> UpdateAsyn(Entity entity, int id);
    }
}
