using System.ComponentModel.DataAnnotations;

namespace TCC.Application.ViewModels
{
    public class PedidoLojaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public ItemLojaViewModel ItemComprado { get; set; }

        public bool IsExpired()
        {
            var duration = TimeSpan.FromTicks(ItemComprado.Duracao);
            var validade = Timestamp + duration;

            return validade < DateTime.Now;
        }
    }
}
