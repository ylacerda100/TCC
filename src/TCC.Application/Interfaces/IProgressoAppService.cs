using TCC.Application.ViewModels;
using TCC.Domain.Models;

namespace TCC.Application.Interfaces
{
    public interface IProgressoAppService : IDisposable
    {
        Task<bool> Add(ProgressoAula progresso);
        Task<ProgressoAulaViewModel> GetByAulaIdAndUserId(Guid aulaId, Guid userId);
    }
}
