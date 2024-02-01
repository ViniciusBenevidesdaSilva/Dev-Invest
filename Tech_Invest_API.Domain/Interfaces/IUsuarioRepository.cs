using Tech_Invest_API.Domain.Model;

namespace Tech_Invest_API.Domain.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario> GetByEmailAsync(string email);
}
