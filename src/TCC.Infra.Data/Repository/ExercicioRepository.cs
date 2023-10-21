using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;
using TCC.Infra.Data.Context;

namespace TCC.Infra.Data.Repository
{
    public class ExercicioRepository : IExercicioRepository
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<Exercicio> DbSet;
        public IUnitOfWork UnitOfWork => Db;

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<IEnumerable<Exercicio>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Exercicio> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}
