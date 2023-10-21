using NetDevPack.Data;
using TCC.Domain.Models;

namespace TCC.Domain.Interfaces
{
    public interface IExercicioRepository : IRepository<Exercicio>
    {
        Task<Exercicio> GetById(Guid id);
        Task<IEnumerable<Exercicio>> GetAll();
    }
}
