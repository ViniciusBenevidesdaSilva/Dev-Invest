using Tech_Invest_API.Application.ViewModel;

namespace Tech_Invest_API.Application.Interfaces;

public interface IUsuarioService
{
    Task<IList<UsuarioViewModel>> GetUsuarioAsync();
    Task<UsuarioViewModel> GetUsuarioByIdAsync(int id);
    Task<UsuarioViewModel> GetUsuarioByEmailAsync(string email);
    Task<int> CreateAsync(UsuarioViewModel usuario);
    Task<UsuarioViewModel> UpdateAsync(UsuarioViewModel usuario, int id);
    Task<UsuarioViewModel?> AutenticarUsuario(UsuarioViewModel usuario);
}
