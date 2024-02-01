using Tech_Invest_API.Domain.Utils.Enums;

namespace Tech_Invest_API.Application.ViewModel;

public class UsuarioViewModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public UserRole? UserRole { get; set; }
}
