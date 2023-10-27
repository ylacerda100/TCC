using NetDevPack.Data;
using TCC.Domain.Models;

namespace TCC.Domain.Interfaces
{
    public interface IProgressoAulaRepository : IRepository<ProgressoAula>
    {
        Task<ProgressoAula> GetById(Guid id);
        Task<bool> Add(ProgressoAula progresso);
        void Update(ProgressoAula progresso);
        Task<ProgressoAula> GetByAulaId(Guid aulaId);
    }
}
