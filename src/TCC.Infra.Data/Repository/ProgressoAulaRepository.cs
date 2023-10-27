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

        public Task<bool> Add(ProgressoAula progresso)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<ProgressoAula> GetByAulaId(Guid aulaId)
        {
            throw new NotImplementedException();
        }

        public Task<ProgressoAula> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(ProgressoAula progresso)
        {
            throw new NotImplementedException();
        }
    }
}
