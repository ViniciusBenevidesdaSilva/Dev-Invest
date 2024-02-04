using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tech_Invest_API.Domain.Interfaces;
using Tech_Invest_API.Domain.Model;
using Tech_Invest_API.Infrastructure.Data.Data;

namespace Tech_Invest_API.Infrastructure.Data.Repository;

public class TipoInvestimentoRepository : Repository<TipoInvestimento>, ITipoInvestimentoRepository
{
    private readonly TechInvestDbContext _context;

    public TipoInvestimentoRepository(TechInvestDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IList<TipoInvestimento>> GetAllAsync()
    {
        return await _context.TiposInvestimento
            .Include(x => x.UsuarioCriacao)
            .Include(x => x.UsuarioAlteracao)
            .ToListAsync();
    }

    public override async Task<TipoInvestimento> GetByIdAsync(int id)
    {
        return await _context.TiposInvestimento
            .Include(x => x.UsuarioCriacao)
            .Include(x => x.UsuarioAlteracao)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TipoInvestimento> GetByNomeAsync(string nome)
    {
        return await _context.TiposInvestimento.FirstOrDefaultAsync(x => x.Nome.ToUpper() == nome.ToUpper());
    }
}
