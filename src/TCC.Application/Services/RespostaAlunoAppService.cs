using TCC.Application.Interfaces;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;

namespace TCC.Application.Services
{
    public class RespostaAlunoAppService : IRespostaAlunoExercicioAppService
    {
        private readonly IRespostaAlunoExercicioRepository _respostaRepo;

        public RespostaAlunoAppService(IRespostaAlunoExercicioRepository respostaAlunoExercicioRepository)
        {
            _respostaRepo = respostaAlunoExercicioRepository;
        }

        public async Task<bool> Add(RespostaAlunoExercicio resposta)
        {
            return await _respostaRepo.Add(resposta);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
