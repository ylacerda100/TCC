using AutoMapper;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Enums;
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
            var result = await _progressoRepo.GetByAulaIdAndUserId(aulaId, userId);
            return _mapper.Map<ProgressoAulaViewModel>(result);
        }

        public async Task<IEnumerable<ProgressoAulaViewModel>> GetByCursoIdAndUserId(Guid cursoId, Guid userId)
        {
            return _mapper.Map<IEnumerable<ProgressoAulaViewModel>>(await _progressoRepo.GetByCursoIdAndUserId(cursoId, userId));
        }

        public async Task<bool> IsCursoIniciado(Guid cursoId, Guid userId)
        {
            var cursoProgresso = await GetByCursoIdAndUserId(cursoId, userId);

            return cursoProgresso != null && cursoProgresso.Any(c => c.Status == StatusProgresso.EmAndamento);
        }

        public async void ConcluirProgresso(Guid progressoId)
        {
            var progressoDomain = await _progressoRepo.GetById(progressoId);

            progressoDomain.DataConclusao = DateTime.Now;
            progressoDomain.Status = Domain.Enums.StatusProgresso.Concluido;

            _progressoRepo.Update(progressoDomain);
        }

        public async Task<bool> IsCursoConcluido(Guid cursoId, Guid userId)
        {
            var cursoProgresso = await GetByCursoIdAndUserId(cursoId, userId);

            return cursoProgresso.Any() && cursoProgresso.All(c => c.Status == StatusProgresso.Concluido);
        }
    }
}
