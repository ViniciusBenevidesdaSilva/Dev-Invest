using Microsoft.EntityFrameworkCore;
using Tech_Invest_API.Domain.Interfaces;
using Tech_Invest_API.Domain.Model;
using Tech_Invest_API.Infrastructure.Data.Data;

namespace Tech_Invest_API.Infrastructure.Data.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly TechInvestDbContext _context;

    public UsuarioRepository(TechInvestDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Usuario> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());
    }

    public async Task<int> CreateAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        return usuario.Id;
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        if(usuario is null)
            throw new ArgumentNullException(nameof(usuario), "O objeto usuário estava vazio");

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await GetByIdAsync(id) ?? throw new Exception($"Usuário de Id {id} não encontrado");
        
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return true;
    }
}
