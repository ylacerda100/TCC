using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;
using TCC.Infra.Data.Context;

namespace TCC.Infra.Data.Repository;

public class CursoRepository : ICursoRepository
{
    protected readonly AppDbContext Db;
    protected readonly DbSet<Curso> DbSet;
    public IUnitOfWork UnitOfWork => Db;
    public CursoRepository(AppDbContext context)
    {
        Db = context;
        DbSet = Db.Set<Curso>();
    }
    
    public async Task<Curso> GetById(Guid id)
    {
        return await DbSet
                .Include(t => t.Aulas)
                .ThenInclude(a => a.Exercicios)
                .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Curso>> GetAll()
    {
        return await DbSet
            .Include(t => t.Aulas)
            .ThenInclude(a => a.Exercicios)
            .ToListAsync();
    }
    
    public async Task<bool> Add(Curso curso)
    {
       DbSet.Add(curso);
       return await Db.Commit();
    }

    public void Update(Curso curso)
    {
        DbSet.Update(curso);
    }

    public async Task<bool> Remove(Curso curso)
    {
        DbSet.Remove(curso);
        return await Db.Commit();
    }

    public void Dispose()
    {
        Db.Dispose();
    }

    public async Task<Curso> GetByName(string name) => 
        DbSet
        .Include(t => t.Aulas)
        .ThenInclude(a => a.Exercicios)
        .FirstOrDefault(t => t.Nome.Contains(name));
}