using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;
using TCC.Infra.Data.Context;

namespace TCC.Infra.Data.Repository
{
    public class PedidoLojaRepository : IPedidoLojaRepository
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<PedidoLoja> DbSet;
        public IUnitOfWork UnitOfWork => Db;

        public PedidoLojaRepository(AppDbContext context)
        {
            Db = context;
            DbSet = Db.Set<PedidoLoja>();
        }

        public async Task<bool> Add(PedidoLoja item)
        {
            DbSet.Add(item);
            return await Db.Commit();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
