using TCC.Application.ViewModels;
using TCC.Domain.Models;

namespace TCC.Application.Interfaces
{
    public interface IProgressoAppService : IDisposable
    {
        Task<bool> Add(ProgressoAula progresso);
        Task<ProgressoAulaViewModel> GetByAulaIdAndUserId(Guid aulaId, Guid userId);
        Task<IEnumerable<ProgressoAulaViewModel>> GetByCursoIdAndUserId(Guid cursoId, Guid userId);
        Task<bool> IsCursoIniciado(Guid cursoId, Guid userId);
        void ConcluirProgresso(Guid progressoId);
    }
}
