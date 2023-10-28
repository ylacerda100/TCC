using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using TCC.Application.Interfaces;
using TCC.Application.Services;
using TCC.Domain.Interfaces;
using TCC.Infra.Data.Context;
using TCC.Infra.Data.Repository;

namespace TCC.Infra.CrossCutting.IoC;

public static class NativeInjector
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Application
        services.AddScoped<ICursoAppService, CursoAppService>();
        services.AddScoped<IItemLojaAppService, ItemLojaAppService>();
        services.AddScoped<IUsuarioAppService, UsuarioAppService>();
        services.AddScoped<IAulaAppService, AulaAppService>();
        services.AddScoped<IExercicioAppService, ExercicioAppService>();
        services.AddScoped<IPedidoAppService, PedidoAppService>();
        services.AddScoped<IProgressoAppService, ProgressoAppService>();
        services.AddScoped<IRespostaAlunoExercicioAppService, RespostaAlunoAppService>();

        //services.AddTransient<IEmailSender, MailJetEmailSender>();

        // Infra - Data
        services.AddScoped<ICursoRepository, CursoRepository>();
        services.AddScoped<IItemLojaRepository, ItemLojaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IAulaRepository, AulaRepository>();
        services.AddScoped<IPedidoLojaRepository, PedidoLojaRepository>();
        services.AddScoped<IExercicioRepository, ExercicioRepository>();
        services.AddScoped<IProgressoAulaRepository, ProgressoAulaRepository>();
        services.AddScoped<IRespostaAlunoExercicioRepository, RespostaAlunoExercicioRepository>();

        services.AddScoped<AppDbContext>();
    }
}