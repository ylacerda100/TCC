using AutoMapper;
using TCC.Application.ViewModels;
using TCC.Domain.Models;

namespace TCC.Application.AutoMapper;

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<CursoViewModel, Curso>();
        CreateMap<AulaViewModel, Aula>();
        CreateMap<ExercicioViewModel, Exercicio>();
        CreateMap<ItemLojaViewModel, ItemLoja>();
        CreateMap<UsuarioViewModel, Usuario>();
        CreateMap<PedidoLojaViewModel, PedidoLoja>();
    }
}