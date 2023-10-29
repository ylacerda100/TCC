using AutoMapper;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;

namespace TCC.Application.Services;

public class CursoAppService : ICursoAppService
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IMapper _mapper;
    
    public CursoAppService(
        ICursoRepository cursoRepository,
        IMapper mapper
        )
    {
        _cursoRepository = cursoRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<CursoViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<CursoViewModel>>(await _cursoRepository.GetAll());
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<CursoViewModel> GetById(Guid id)
    {
        return _mapper.Map<CursoViewModel>(await _cursoRepository.GetById(id));
    }

    public async Task<CursoViewModel> GetByName(string name)
    {
        return _mapper.Map<CursoViewModel>(await _cursoRepository.GetByName(name));
    }

    public async Task<bool> Add(Curso curso)
    {
        return await _cursoRepository.Add(curso);
    }

    public async Task<bool> Remove(Guid cursoId)
    {
        var curso = await _cursoRepository.GetById(cursoId);
        return await _cursoRepository.Remove(curso);
    }
}