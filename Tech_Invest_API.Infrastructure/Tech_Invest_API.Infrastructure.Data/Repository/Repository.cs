using Microsoft.EntityFrameworkCore;
using Tech_Invest_API.Domain.Interfaces;
using Tech_Invest_API.Domain.Model;
using Tech_Invest_API.Infrastructure.Data.Data;

namespace Tech_Invest_API.Infrastructure.Data.Repository;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly TechInvestDbContext _context;
    private readonly DbSet<T> _set;

    public Repository(TechInvestDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _set = _context.Set<T>();

        if(_set is null)
            throw new InvalidOperationException($"O contexto não possui um conjunto (DbSet) para o tipo {typeof(T).Name}.");
    }

    public virtual async Task<IList<T>> GetAllAsync()
    {
        return await _set.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _set.FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task<int> CreateAsync(T entity)
    {
        await _set.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity), $"O objeto do tipo {typeof(T).Name} estava vazio");

        _set.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id) ?? throw new InvalidOperationException($"O objeto do tipo {typeof(T).Name} com Id {id} não foi encontrado para exclusão");

        _set.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }
}
