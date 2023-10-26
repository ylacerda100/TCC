using NetDevPack.Domain;

namespace TCC.Domain.Models
{
    public class PedidoLoja : Entity, IAggregateRoot
    {
        public PedidoLoja()
        {

        }
        public DateTime Timestamp { get; set; }
        public ItemLoja ItemComprado { get; set; }

        //navigation
        public ItemLoja ItemLoja { get; set; }
        public Guid ItemLojaId { get; set; }
        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public bool IsExpired()
        {
            var duration = TimeSpan.FromTicks(ItemComprado.Duracao);
            var validade = Timestamp + duration;

            return validade < DateTime.Now;
        }
    }
}
