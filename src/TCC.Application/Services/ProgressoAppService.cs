using TCC.Application.Interfaces;
using TCC.Domain.Interfaces;

namespace TCC.Application.Services
{
    public class ProgressoAppService : IProgressoAppService
    {
        private readonly IProgressoAulaRepository _progressoRepo;
        public ProgressoAppService(IProgressoAulaRepository progressoAulaRepository)
        {
            _progressoRepo = progressoAulaRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
