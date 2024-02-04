using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech_Invest_API.Domain.Model;

namespace Tech_Invest_API.Infrastructure.Data.Data.Map;

internal class TipoInvestimentoMap : IEntityTypeConfiguration<TipoInvestimento>
{
    public void Configure(EntityTypeBuilder<TipoInvestimento> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Descricao);
        builder.Property(x => x.DataCriacao).IsRequired();
        builder.Property(x => x.IdUsuarioCriacao);
        builder.Property(x => x.DataAlteracao);
        builder.Property(x => x.IdUsuarioAlteracao);

        builder.HasOne(x => x.UsuarioCriacao)
            .WithMany()
            .HasForeignKey(x => x.IdUsuarioCriacao)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.UsuarioAlteracao)
            .WithMany()
            .HasForeignKey(x => x.IdUsuarioAlteracao)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
