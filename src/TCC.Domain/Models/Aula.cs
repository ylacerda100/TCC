using NetDevPack.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCC.Domain.Models
{
    public class Aula : Entity, IAggregateRoot
    {
        public Aula() 
        { 

        }

        public int Number { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public string ContentUrl { get; set; }

        public IEnumerable<Exercicio> Exercicios { get; set; }

        [ForeignKey("Curso")]
        public Guid CursoId { get; set; }

        public Curso Curso { get; set; }

        public int Xp { get; set; }
        public int QtdMoedas { get; set; }
    }
}
