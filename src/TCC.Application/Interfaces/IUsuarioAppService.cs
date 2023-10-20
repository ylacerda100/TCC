using Microsoft.AspNetCore.Identity;
using TCC.Application.ViewModels;
using TCC.Domain.Models;

namespace TCC.Application.Interfaces
{
    public interface IUsuarioAppService : IDisposable
    {
        Task<IEnumerable<UsuarioViewModel>> GetAll();

        Task<Usuario> GetCurrentUser();
        Task<IdentityResult> UpdatePedidoUser(Usuario user, PedidoLoja pedido);
    }
}
