using NetDevPack.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using TCC.Domain.Enums;

namespace TCC.Domain.Models
{
    public class ProgressoAula : Entity, IAggregateRoot
    {
        [ForeignKey("Usuario")]
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Aula")]
        public Guid AulaId { get; set; }

        public Aula Aula { get; set; }

        [ForeignKey("Curso")]
        public Guid CursoId { get; set; }

        public Curso Curso { get; set; }

        public StatusProgresso Status { get; set; }

        public DateTime DataConclusao { get; set; }

        public List<RespostaAlunoExercicio> RespostasExercicios { get; set; }

        public ProgressoAula()
        {
        }
    }
}
