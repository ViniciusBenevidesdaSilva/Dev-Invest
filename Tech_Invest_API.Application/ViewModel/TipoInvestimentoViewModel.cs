namespace Tech_Invest_API.Application.ViewModel;

public class TipoInvestimentoViewModel : BaseViewModel
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataCriacao { get; set; }
    public int? IdUsuarioCriacao { get; set; }
    public string? NomeUsuarioCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public int? IdUsuarioAlteracao { get; set; }
    public string? NomeUsuarioAlteracao { get; set; }
}
