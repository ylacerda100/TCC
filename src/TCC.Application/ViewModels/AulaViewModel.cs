using System.ComponentModel.DataAnnotations;
using TCC.Domain.Models;

namespace TCC.Application.ViewModels
{
    public class AulaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public int Number { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public string ContentUrl { get; set; }

        public IEnumerable<Exercicio> Exercicios { get; set; }

        public Guid CursoId { get; set; }
        public int Xp { get; set; }
        public int QtdMoedas { get; set; }
    }
}
