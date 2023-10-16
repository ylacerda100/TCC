using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TCC.Domain.Enums;

namespace TCC.Application.ViewModels
{
    public class ItemLojaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public string Descricao { get; set; }
        public int Preco { get; set; }
        public long Duracao { get; set; }
        public decimal Multiplicador { get; set; }
        public long QtdXp { get; set; }
        public TipoItemLoja TipoItem { get; set; }
    }
}
