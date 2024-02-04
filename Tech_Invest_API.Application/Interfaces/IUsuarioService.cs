using Tech_Invest_API.Application.ViewModel;

namespace Tech_Invest_API.Application.Interfaces;

public interface IUsuarioService : ICrudService<UsuarioViewModel>
{
    Task<UsuarioViewModel> GetByEmailAsync(string email);
    Task<UsuarioViewModel?> AutenticarUsuario(UsuarioViewModel usuario);
}
