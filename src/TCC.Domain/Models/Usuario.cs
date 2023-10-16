using Microsoft.AspNetCore.Identity;
using NetDevPack.Domain;

namespace TCC.Domain.Models
{
    public class Usuario : IdentityUser, IAggregateRoot
    {
        public string Nome { get; set; }
        public long Xp { get; set; }
        public int QtdMoedas { get; set; }

        public decimal MultiplicadorXp { get; set; }
        public List<PedidoLoja> Pedidos { get; set; }
    }
}
