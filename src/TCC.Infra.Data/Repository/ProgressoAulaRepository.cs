﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<ProgressoAula> GetByAulaId(Guid aulaId)
        {
            return await DbSet
                .Include(p => p.Aula)
                .FirstAsync(t => t.AulaId == aulaId);
        }

        public async Task<ProgressoAula> GetById(Guid id)
        {
            return await DbSet
                .Include(p => p.Aula)
                .FirstAsync(t => t.Id == id);
        }

        public void Update(ProgressoAula progresso)
        {
            DbSet.Update(progresso);
        }
    }
}
