using TCC.Application.ViewModels;

namespace TCC.Application.Interfaces
{
    public interface IExercicioAppService : IDisposable
    {
        Task<IEnumerable<ExercicioViewModel>> GetAll();
        Task<ExercicioViewModel> GetById(Guid id);
    }
}
