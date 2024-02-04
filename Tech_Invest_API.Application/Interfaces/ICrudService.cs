using Tech_Invest_API.Application.ViewModel;

namespace Tech_Invest_API.Application.Interfaces;

public interface ICrudService<T> where T : BaseViewModel
{
    Task<IList<T>> GetAsync();
    Task<T> GetByIdAsync(int id);
    Task<int> CreateAsync(T viewModel);
    Task<T> UpdateAsync(T viewModel, int id);
    Task<bool> DeleteAsync(int id);
}
