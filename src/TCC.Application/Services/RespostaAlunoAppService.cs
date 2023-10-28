using TCC.Application.Interfaces;

namespace TCC.Application.Services
{
    public class RespostaAlunoAppService : IRespostaAlunoExercicioAppService
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
