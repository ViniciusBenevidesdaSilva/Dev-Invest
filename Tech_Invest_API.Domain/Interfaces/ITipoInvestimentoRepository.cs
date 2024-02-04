using Tech_Invest_API.Domain.Model;

namespace Tech_Invest_API.Domain.Interfaces;

public interface ITipoInvestimentoRepository : IRepository<TipoInvestimento>
{
    Task<TipoInvestimento> GetByNomeAsync(string nome);
}
