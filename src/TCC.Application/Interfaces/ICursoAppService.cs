using TCC.Application.ViewModels;
using TCC.Domain.Models;

namespace TCC.Application.Interfaces;

public interface ICursoAppService : IDisposable
{
    Task<IEnumerable<CursoViewModel>> GetAll();
    Task<CursoViewModel> GetById(Guid id);

    Task<CursoViewModel> GetByName(string name);

    Task<bool> Add(Curso curso);

    Task<bool> Remove(Guid cursoId);
}