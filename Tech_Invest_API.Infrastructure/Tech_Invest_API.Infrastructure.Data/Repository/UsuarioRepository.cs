using Microsoft.EntityFrameworkCore;
using Tech_Invest_API.Domain.Interfaces;
using Tech_Invest_API.Domain.Model;
using Tech_Invest_API.Infrastructure.Data.Data;

namespace Tech_Invest_API.Infrastructure.Data.Repository;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    private readonly TechInvestDbContext _context;

    public UsuarioRepository(TechInvestDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Usuario> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());
    }
}
