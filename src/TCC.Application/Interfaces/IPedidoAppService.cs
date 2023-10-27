using TCC.Application.ViewModels;

namespace TCC.Application.Interfaces
{
    public interface IPedidoAppService
    {
        Task<IEnumerable<PedidoLojaViewModel>> GetAllPedidosFromUser(UsuarioViewModel user);
        Task<bool> AddPedido(PedidoLojaViewModel pedido);
    }
}
