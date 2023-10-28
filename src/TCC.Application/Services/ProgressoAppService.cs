using TCC.Application.Interfaces;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;

namespace TCC.Application.Services
{
    public class ProgressoAppService : IProgressoAppService
    {
        private readonly IProgressoAulaRepository _progressoRepo;
        public ProgressoAppService(IProgressoAulaRepository progressoAulaRepository)
        {
            _progressoRepo = progressoAulaRepository;
        }

        public async Task<bool> Add(ProgressoAula progresso)
        {
            return await _progressoRepo.Add(progresso);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ProgressoAula> GetByAulaIdAndUserId(Guid aulaId, Guid userId)
        {
            return await _progressoRepo.GetByAulaIdAndUserId(aulaId, userId);
        }
    }
}
