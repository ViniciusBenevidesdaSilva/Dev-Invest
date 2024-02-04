using Tech_Invest_API.Application.ViewModel;

namespace Tech_Invest_API.Application.Interfaces;

public interface ITipoInvestimentoService : ICrudService<TipoInvestimentoViewModel>
{
    Task<TipoInvestimentoViewModel> GetByNomelAsync(string nome);
}
