using AutoMapper;
using Tech_Invest_API.Application.Interfaces;
using Tech_Invest_API.Application.ViewModel;
using Tech_Invest_API.Domain.Interfaces;
using Tech_Invest_API.Domain.Model;
using Tech_Invest_API.Domain.Utils;

namespace Tech_Invest_API.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<UsuarioViewModel> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        return _mapper.Map<Usuario, UsuarioViewModel>(usuario);
    }

    public async Task<UsuarioViewModel> GetUsuarioByEmailAsync(string email)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(email);
        return _mapper.Map<Usuario, UsuarioViewModel>(usuario);
    }

    public async Task<int> CreateAsync(UsuarioViewModel usuario)
    {
        usuario.Id = 0;
        var log = await ValidaUsuario(usuario);

        if(log.Count > 0)
            throw new Exception("Usuário inválido: " + String.Join("; ", log));

        var usuarioModel = _mapper.Map<UsuarioViewModel, Usuario>(usuario);

        usuario.Senha = string.Empty;

        return await _usuarioRepository.CreateAsync(usuarioModel);
    }

    public async Task<UsuarioViewModel?> AutenticarUsuario(UsuarioViewModel usuario)
    {
        if (usuario is null)
            throw new ArgumentNullException(nameof(usuario), "Usuário não pode ser nulo");

        if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
            return null;

        var usuarioBanco = await _usuarioRepository.GetByEmailAsync(usuario.Email);

        if (usuarioBanco is null || !SenhaUtils.ValidarSenha(usuario.Senha, usuarioBanco.Senha))
            return null;

        return usuario;
    }

    private async Task<IList<string>> ValidaUsuario(UsuarioViewModel usuario)
    {
        var retorno = new List<string>();

        if(usuario is null)
        {
            retorno.Add("Usuário não pode ser nulo");
            return retorno;
        }

        if (usuario.Id < 0)
            retorno.Add("O Id do usuário deve ser maior que 0");

        if(!RegexUtils.ValidaFormatoEmail(usuario.Email))
            retorno.Add("Email não está no formato válido");

        var usuarioEmailBanco = await GetUsuarioByEmailAsync(usuario.Email);

        if(usuarioEmailBanco is not null)
        {
            if (usuario.Id == 0 || usuario.Id != usuarioEmailBanco.Id)
                retorno.Add("Email já cadastrado");
        }

        if (usuario.Senha.Length < 3)
            retorno.Add("A senha deve possuir ao menos 3 caracteres");

        return retorno;
    }
}
