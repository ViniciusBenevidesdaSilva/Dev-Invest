using AutoMapper;
using Tech_Invest_API.Application.Interfaces;
using Tech_Invest_API.Application.ViewModel;
using Tech_Invest_API.Domain.Interfaces;
using Tech_Invest_API.Domain.Model;
using Tech_Invest_API.Domain.Utils;
using Tech_Invest_API.Domain.Utils.Enums;

namespace Tech_Invest_API.Application.Services;

public class UsuarioService : CrudService<Usuario, UsuarioViewModel>, IUsuarioService
{
    private readonly IUsuarioRepository _repository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UsuarioViewModel> GetByEmailAsync(string? email)
    {
        var usuario = await _repository.GetByEmailAsync(email);
        return _mapper.Map<Usuario, UsuarioViewModel>(usuario);
    }

    public override async Task<int> CreateAsync(UsuarioViewModel usuario)
    {
        var retorno = await base.CreateAsync(usuario);

        usuario.Senha = string.Empty;

        return retorno;
    }

    public override async Task<UsuarioViewModel> UpdateAsync(UsuarioViewModel usuario, int id)
    {
        var retorno = await base.UpdateAsync(usuario, id);

        usuario.Senha = string.Empty;

        return retorno;
    }

    public async Task<UsuarioViewModel?> AutenticarUsuario(UsuarioViewModel usuario)
    {
        if (usuario is null)
            throw new ArgumentNullException(nameof(usuario), "Usuário não pode ser nulo");

        if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
            return null;

        var usuarioBanco = await _repository.GetByEmailAsync(usuario.Email);

        if (usuarioBanco is null || !SenhaUtils.ValidarSenha(usuario.Senha, usuarioBanco.Senha))
            return null;

        usuario.Id = usuarioBanco.Id;
        usuario.Nome = usuarioBanco.Nome;
        usuario.UserRole = usuarioBanco.UserRole;

        return usuario;
    }

    internal override async Task<IList<string>> ValidaViewModel(UsuarioViewModel usuario)
    {
        var retorno = new List<string>();

        if(usuario is null)
        {
            retorno.Add("Usuário não pode ser nulo");
            return retorno;
        }

        if (usuario!.Id < 0)
            retorno.Add("O Id do usuário deve ser maior que 0");

        if (!RegexUtils.ValidaFormatoEmail(usuario!.Email))
            retorno.Add("Email não está no formato válido");
        else
        {
            var usuarioEmailBanco = await GetByEmailAsync(usuario!.Email);

            if (usuarioEmailBanco is not null)
            {
                if (usuario!.Id == 0 || usuario!.Id != usuarioEmailBanco!.Id)
                    retorno.Add("Email já cadastrado");
            }
        }

        if (string.IsNullOrEmpty(usuario!.Senha) || usuario!.Senha?.Length < 3)
            retorno.Add("A senha deve possuir ao menos 3 caracteres");

        if (!usuario.UserRole.HasValue || !Enum.IsDefined(typeof(EnumUserRole), usuario.UserRole))
            retorno.Add("User Role inválido.");

        return retorno;
    }
}
