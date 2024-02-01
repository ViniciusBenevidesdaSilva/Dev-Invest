using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech_Invest_API.Domain.Model;

namespace Tech_Invest_API.Infrastructure.Data.Data.Map;

internal class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome).HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.SenhaHash).IsRequired();
        builder.Property(x => x.SenhaSalt).IsRequired();
        builder.Property(x => x.UserRole).IsRequired();

        builder.Ignore(x => x.Senha);
    }
}
