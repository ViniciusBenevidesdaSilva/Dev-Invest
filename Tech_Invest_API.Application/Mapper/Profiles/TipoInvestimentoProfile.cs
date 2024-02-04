using AutoMapper;
using Tech_Invest_API.Application.ViewModel;
using Tech_Invest_API.Domain.Model;

namespace Tech_Invest_API.Application.Mapper.Profiles;

public class TipoInvestimentoProfile : Profile
{
    public TipoInvestimentoProfile()
    {
        CreateMap<TipoInvestimento, TipoInvestimentoViewModel>()
            .ForMember(x => x.NomeUsuarioCriacao, opt => opt.MapFrom(src => (src.UsuarioCriacao == null) ? null : src.UsuarioCriacao.Nome))
            .ForMember(x => x.NomeUsuarioAlteracao, opt => opt.MapFrom(src => (src.UsuarioAlteracao == null) ? null : src.UsuarioAlteracao.Nome));
        CreateMap<TipoInvestimentoViewModel, TipoInvestimento>();
    }
}
