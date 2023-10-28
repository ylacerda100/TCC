using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;
using TCC.Infra.Data.Context;

namespace TCC.Infra.Data.Repository
{
    public class RespostaAlunoExercicioRepository : IRespostaAlunoExercicioRepository
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<RespostaAlunoExercicio> DbSet;
        public IUnitOfWork UnitOfWork => Db;

        public RespostaAlunoExercicioRepository(AppDbContext context)
        {
            Db = context;
            DbSet = Db.Set<RespostaAlunoExercicio>();
        }


        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<bool> Add(RespostaAlunoExercicio resposta)
        {
            DbSet.Add(resposta);
            return await Db.Commit();
        }
    }
}
