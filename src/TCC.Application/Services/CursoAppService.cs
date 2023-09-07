﻿using AutoMapper;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Enums;
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
        return _mapper.Map<IEnumerable<CursoViewModel>>(_cursoRepository.GetAll());
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}