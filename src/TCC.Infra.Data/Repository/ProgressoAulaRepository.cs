using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;
using TCC.Infra.Data.Context;

namespace TCC.Infra.Data.Repository
{
    public class ProgressoAulaRepository : IProgressoAulaRepository
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<ProgressoAula> DbSet;

        public IUnitOfWork UnitOfWork => Db;

        public ProgressoAulaRepository(AppDbContext context)
        {
            Db = context;
            DbSet = Db.Set<ProgressoAula>();
        }

        public async Task<bool> Add(ProgressoAula progresso)
        {
            DbSet.Add(progresso);
            return await Db.Commit();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<IEnumerable<ProgressoAula>> GetByAulaId(Guid aulaId)
        {
            return await DbSet
                .Include(p => p.Aula)
                .Where(t => t.AulaId == aulaId)
                .ToListAsync();
        }

        public async Task<ProgressoAula> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Update(ProgressoAula progresso)
        {
            DbSet.Update(progresso);
            Db.Commit();
        }

        public async Task<ProgressoAula> GetByAulaIdAndUserId(Guid aulaId, Guid userId)
        {
            return await DbSet
                .Include(p => p.RespostasExercicios)
                .Include(p => p.Curso)
                .ThenInclude(c => c.Aulas)
                .Include(p => p.Aula)
                .ThenInclude(a => a.Exercicios)
                .FirstAsync(p => p.AulaId == aulaId && p.UsuarioId == userId);
        }

        public async Task<IEnumerable<ProgressoAula>> GetByCursoIdAndUserId(Guid cursoId, Guid userId)
        {
            return await DbSet
                .Include(p => p.Curso)
                .Include(p => p.Aula)
                .ThenInclude(a => a.Exercicios)
                .Where(p => p.CursoId == cursoId && p.UsuarioId == userId)
                .ToListAsync();
        }
    }
}
