using AutoMapper;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Interfaces;

namespace TCC.Application.Services
{
    public class ExercicioAppService : IExercicioAppService
    {
        private readonly IExercicioRepository _exercicioRepo;
        private readonly IMapper _mapper;

        public ExercicioAppService(
            IExercicioRepository exercicioRepo,
            IMapper mapper
            )
        {
            _exercicioRepo = exercicioRepo;
            _mapper = mapper;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<ExercicioViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ExercicioViewModel>>(await _exercicioRepo.GetAll());
        }

        public async Task<ExercicioViewModel> GetById(Guid id)
        {
            return _mapper.Map<ExercicioViewModel>(await _exercicioRepo.GetById(id));
        }
    }
}
