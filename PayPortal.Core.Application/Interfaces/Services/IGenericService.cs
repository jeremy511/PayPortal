

namespace PayPortal.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Entity>
    {
        Task<SaveViewModel> AddAsync(SaveViewModel saveView);
        Task DeleteAsync(int Id);
        Task<List<ViewModel>> GetAllViewModel();
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task<SaveViewModel> UpdateAsync(SaveViewModel saveView, int Id);
    }
}
