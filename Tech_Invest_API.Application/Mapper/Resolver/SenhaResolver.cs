using AutoMapper;
using Tech_Invest_API.Application.ViewModel;
using Tech_Invest_API.Domain.Model;
using Tech_Invest_API.Domain.Utils;

namespace Tech_Invest_API.Application.Mapper.Resolver;

public class SenhaResolver : IValueResolver<UsuarioViewModel, Usuario, (byte[] Hash, byte[] Salt)>
{
    public (byte[] Hash, byte[] Salt) Resolve(UsuarioViewModel source, Usuario destination, (byte[] Hash, byte[] Salt) destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.Senha))
            return (destination.SenhaHash, destination.SenhaSalt);
        
        return SenhaUtils.GerarHashSenha(source.Senha);
    }
}
