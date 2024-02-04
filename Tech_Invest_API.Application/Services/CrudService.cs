using AutoMapper;
using Tech_Invest_API.Application.Interfaces;
using Tech_Invest_API.Application.ViewModel;
using Tech_Invest_API.Domain.Interfaces;
using Tech_Invest_API.Domain.Model;

namespace Tech_Invest_API.Application.Services;

public abstract class CrudService<TEntity, TViewModel> : ICrudService<TViewModel> 
    where TEntity : BaseEntity
    where TViewModel : BaseViewModel
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public CrudService(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public virtual async Task<IList<TViewModel>> GetAsync()
    {
        var entidade = await _repository.GetAllAsync();
        return _mapper.Map<IList<TEntity>, IList<TViewModel>>(entidade);
    }

    public virtual async Task<TViewModel> GetByIdAsync(int id)
    {
        var entidade = await _repository.GetByIdAsync(id);
        return _mapper.Map<TEntity, TViewModel>(entidade);
    }

    public virtual async Task<int> CreateAsync(TViewModel viewModel)
    {
        viewModel.Id = 0;
        var log = await ValidaViewModel(viewModel);

        if (log.Count > 0)
            throw new Exception($"{typeof(TViewModel).Name} inválido: {String.Join("; ", log)}");

        var model = _mapper.Map<TViewModel, TEntity>(viewModel);
        viewModel.Id = await _repository.CreateAsync(model);

        return viewModel.Id;
    }

    public virtual async Task<TViewModel> UpdateAsync(TViewModel viewModel, int id)
    {
        viewModel.Id = id;
        var log = await ValidaViewModel(viewModel);

        if (log.Count > 0)
            throw new Exception($"{typeof(TViewModel).Name} inválido: {String.Join("; ", log)}");

        var model = _mapper.Map<TViewModel, TEntity>(viewModel);
        var entity = await _repository.GetByIdAsync(viewModel.Id);

        if (entity is null)
            throw new Exception($"{typeof(TEntity).Name} de id {id} não encontrado");

        entity.UpdateFrom(model);

        var retorno = await _repository.UpdateAsync(entity);

        return _mapper.Map<TEntity, TViewModel>(retorno);
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
            throw new Exception($"{typeof(TEntity).Name} de id {id} não encontrado");

        return await _repository.DeleteAsync(id);
    }

    internal abstract Task<IList<string>> ValidaViewModel(TViewModel usuario);
}
