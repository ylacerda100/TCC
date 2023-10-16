using TCC.Domain.Models;

namespace TCC.Application.ViewModels
{
    public class PedidoLojaViewModel
    {
        public DateTime Timestamp { get; set; }
        public ItemLoja ItemComprado { get; set; }

        public bool IsExpired()
        {
            var duration = TimeSpan.FromTicks(ItemComprado.Duracao);
            var validade = Timestamp + duration;

            return validade < DateTime.Now;
        }
    }
}
