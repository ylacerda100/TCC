using TCC.Application.ViewModels;
using TCC.Domain.Models;

namespace TCC.Application.Interfaces
{
    public interface IPedidoAppService
    {
        Task<IEnumerable<PedidoLojaViewModel>> GetAllPedidosFromUser(UsuarioViewModel user);
        Task<bool> AddPedido(PedidoLojaViewModel pedido, Usuario user);
    }
}
