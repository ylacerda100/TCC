using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TCC.Application.ViewModels;
using TCC.Domain.Models;

namespace TCC.Application.AutoMapper;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Curso, CursoViewModel>();
        CreateMap<Aula, AulaViewModel>();
        CreateMap<Exercicio, ExercicioViewModel>();
        CreateMap<ItemLoja, ItemLojaViewModel>();
        CreateMap<Usuario, UsuarioViewModel>();
        CreateMap<PedidoLoja, PedidoLojaViewModel>();
        CreateMap<ProgressoAula, ProgressoAulaViewModel>();
        CreateMap<RespostaAlunoExercicio, RespostaAlunoExercicioViewModel>();
    }
}