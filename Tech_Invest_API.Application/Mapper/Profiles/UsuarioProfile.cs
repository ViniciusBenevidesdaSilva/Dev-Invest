using AutoMapper;
using Tech_Invest_API.Application.Mapper.Resolver;
using Tech_Invest_API.Application.ViewModel;
using Tech_Invest_API.Domain.Model;

namespace Tech_Invest_API.Application.Mapper.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioViewModel>();
        CreateMap<UsuarioViewModel, Usuario>()
            .ForMember(dest => dest.Senha, opt => opt.MapFrom<SenhaResolver>());
    }
}
