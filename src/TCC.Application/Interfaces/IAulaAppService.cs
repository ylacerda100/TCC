using TCC.Application.ViewModels;

namespace TCC.Application.Interfaces
{
    public interface IAulaAppService : IDisposable
    {
        Task<AulaViewModel> GetByName(string name);
        Task<AulaViewModel> GetById(Guid id);
    }
}
