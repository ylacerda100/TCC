using NetDevPack.Data;
using TCC.Domain.Models;

namespace TCC.Domain.Interfaces
{
    public interface IRespostaAlunoExercicioRepository : IRepository<RespostaAlunoExercicio>
    {
        Task<bool> Add(RespostaAlunoExercicio resposta);
    }
}
