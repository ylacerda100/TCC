using NetDevPack.Data;
using TCC.Domain.Models;

namespace TCC.Domain.Interfaces
{
    public interface IPedidoLojaRepository : IRepository<PedidoLoja>
    {
        Task<bool> Add(PedidoLoja item);
    }
}
