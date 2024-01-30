using Microsoft.EntityFrameworkCore;
using Tech_Invest_API.Domain.Model;
using Tech_Invest_API.Infrastructure.Data.Data.Map;

namespace Tech_Invest_API.Infrastructure.Data.Data;

public class TechInvestDbContext : DbContext
{
    public TechInvestDbContext(DbContextOptions<TechInvestDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMap());

        base.OnModelCreating(modelBuilder);
    }
}
