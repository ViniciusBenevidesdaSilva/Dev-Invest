namespace Tech_Invest_API.Domain.Model;

public class TipoInvestimento : BaseEntity
{
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public int? IdUsuarioCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public int? IdUsuarioAlteracao { get; set; }

    public Usuario? UsuarioCriacao { get; set; }
    public Usuario? UsuarioAlteracao { get; set; }


    public override void UpdateFrom(BaseEntity updatedEntity)
    {
        base.UpdateFrom(updatedEntity);

        if (updatedEntity is TipoInvestimento tipoInvestimentoAtualizado)
        {
            this.Nome = tipoInvestimentoAtualizado.Nome;
            this.Descricao = tipoInvestimentoAtualizado.Descricao;
            
            // Não é permitido realizar o update nas informações de criação
            /*
            this.DataCriacao = tipoInvestimentoAtualizado.DataCriacao;
            this.IdUsuarioCriacao = tipoInvestimentoAtualizado.IdUsuarioCriacao;
            */

            this.DataAlteracao = tipoInvestimentoAtualizado.DataAlteracao;
            this.IdUsuarioAlteracao = tipoInvestimentoAtualizado.IdUsuarioAlteracao;
        }
    }
}
