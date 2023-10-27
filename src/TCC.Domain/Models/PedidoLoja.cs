using NetDevPack.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCC.Domain.Models
{
    public class PedidoLoja : Entity, IAggregateRoot
    {
        public PedidoLoja()
        {

        }

        public DateTime Timestamp { get; set; }


        [ForeignKey("Usuario")]
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }


        [ForeignKey("ItemComprado")]
        public Guid ItemCompradoId { get; set; }

        public ItemLoja ItemComprado { get; set; }

        public bool IsExpired()
        {
            var duration = TimeSpan.FromTicks(ItemComprado.Duracao);
            var validade = Timestamp + duration;

            return validade < DateTime.Now;
        }
    }
}
