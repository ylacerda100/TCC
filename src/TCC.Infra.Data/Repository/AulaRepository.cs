using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;
using TCC.Infra.Data.Context;

namespace TCC.Infra.Data.Repository
{
    public class AulaRepository : IAulaRepository
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<Aula> DbSet;

        public IUnitOfWork UnitOfWork => Db;

        public AulaRepository(AppDbContext context)
        {
            Db = context;
            DbSet = Db.Set<Aula>();
        }

        public async Task<bool> Add(Aula aula)
        {
            DbSet.Add(aula);
            return await Db.Commit();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<IEnumerable<Aula>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Aula> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<Aula> GetByName(string name)
        {
            return DbSet
                .Include(a => a.Exercicios)
                .FirstOrDefault(t => t.Nome.Contains(name));
        }

        public void Remove(Aula aula)
        {
            DbSet.Remove(aula);
        }

        public void Update(Aula aula)
        {
            DbSet.Update(aula);
        }
    }
}
