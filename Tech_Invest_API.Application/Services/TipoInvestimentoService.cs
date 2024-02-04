using AutoMapper;
using Tech_Invest_API.Application.Interfaces;
using Tech_Invest_API.Application.ViewModel;
using Tech_Invest_API.Domain.Interfaces;
using Tech_Invest_API.Domain.Model;

namespace Tech_Invest_API.Application.Services;

public class TipoInvestimentoService : CrudService<TipoInvestimento, TipoInvestimentoViewModel>, ITipoInvestimentoService
{
    private readonly ITipoInvestimentoRepository _repository;
    private readonly IMapper _mapper;

    public TipoInvestimentoService(ITipoInvestimentoRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TipoInvestimentoViewModel> GetByNomelAsync(string nome)
    {
        var tipoInvestimento = await _repository.GetByNomeAsync(nome);
        return _mapper.Map<TipoInvestimento, TipoInvestimentoViewModel>(tipoInvestimento);
    }

    public override Task<int> CreateAsync(TipoInvestimentoViewModel viewModel)
    {
        viewModel.DataCriacao = DateTime.Now;
        return base.CreateAsync(viewModel);
    }

    public override Task<TipoInvestimentoViewModel> UpdateAsync(TipoInvestimentoViewModel viewModel, int id)
    {
        viewModel.DataAlteracao = DateTime.Now;
        return base.UpdateAsync(viewModel, id);
    }

    internal async override Task<IList<string>> ValidaViewModel(TipoInvestimentoViewModel tipoInvestimento)
    {
        var retorno = new List<string>();

        if (tipoInvestimento is null)
        {
            retorno.Add("Tipo De Investimento não pode ser nulo");
            return retorno;
        }

        if (tipoInvestimento!.Id < 0)
            retorno.Add("O Id do Tipo de Investimento deve ser maior que 0");

        if (string.IsNullOrEmpty(tipoInvestimento!.Nome))
            retorno.Add("Nome deve estar preenchido");
        else
        {
            var tipoInvestimentoBanco = await GetByNomelAsync(tipoInvestimento!.Nome!);

            if (tipoInvestimentoBanco is not null)
            {
                if (tipoInvestimento!.Id == 0 || tipoInvestimento!.Id != tipoInvestimentoBanco!.Id)
                    retorno.Add("Nome já cadastrado");
            }
        }

        if (!tipoInvestimento!.DataCriacao.HasValue)
            retorno.Add("A Data de Criação não pode ser nula");

        else if (tipoInvestimento.DataAlteracao.HasValue && tipoInvestimento.DataCriacao > tipoInvestimento.DataAlteracao)
            retorno.Add("A Data de Criação não pode posterior à data de Alteração");


        return retorno;
    }
}
