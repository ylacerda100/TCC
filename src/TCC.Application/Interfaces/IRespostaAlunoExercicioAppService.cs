using TCC.Domain.Models;

namespace TCC.Application.Interfaces
{
    public interface IRespostaAlunoExercicioAppService : IDisposable
    {
        Task<bool> Add(RespostaAlunoExercicio resposta);
    }
}
