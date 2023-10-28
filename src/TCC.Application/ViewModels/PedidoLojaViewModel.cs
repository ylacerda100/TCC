using System.ComponentModel.DataAnnotations;
using TCC.Domain.Models;

namespace TCC.Application.ViewModels
{
    public class PedidoLojaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public ItemLojaViewModel ItemComprado { get; set; }
        public Guid ItemCompradoId { get; set; }

        public Guid UsuarioId { get; set; }
    }
}
