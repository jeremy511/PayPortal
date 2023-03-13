using AutoMapper;
using PayPortal.Core.Application.Interfaces.Repositories;
using PayPortal.Core.Application.Interfaces.Services;

namespace PayPortal.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity>
        where ViewModel : class
        where Entity : class
        where SaveViewModel : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<SaveViewModel> AddAsync(SaveViewModel saveView)
        {
            Entity entity = _mapper.Map<Entity>(saveView);
            await _repository.AddAsync(entity);

            return saveView;
        }

        public virtual async Task<SaveViewModel> UpdateAsync(SaveViewModel saveView, int Id)
        {
            Entity entity = _mapper.Map<Entity>(saveView);
            await _repository.UpdateAsyn(entity, Id);

            return saveView;
        }

        public virtual async Task DeleteAsync(int Id)
        {
            Entity entity = await _repository.GetViewModelById(Id);
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
            var EntityList = await _repository.GetAllViewModel();
            return _mapper.Map<List<ViewModel>>(EntityList);
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(int id)
        {
            Entity entity = await _repository.GetViewModelById(id);
            SaveViewModel saveView = _mapper.Map<SaveViewModel>(entity);
            return saveView;
        }

    }
}
