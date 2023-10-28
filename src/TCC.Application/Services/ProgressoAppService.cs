using AutoMapper;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;

namespace TCC.Application.Services
{
    public class ProgressoAppService : IProgressoAppService
    {
        private readonly IProgressoAulaRepository _progressoRepo;
        private readonly IMapper _mapper;

        public ProgressoAppService(
            IProgressoAulaRepository progressoAulaRepository, 
            IMapper mapper
            )
        {
            _mapper = mapper;
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

        public async Task<ProgressoAulaViewModel> GetByAulaIdAndUserId(Guid aulaId, Guid userId)
        {
            return _mapper.Map<ProgressoAulaViewModel>(await _progressoRepo.GetByAulaIdAndUserId(aulaId, userId));
        }
    }
}
