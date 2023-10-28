using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCC.Domain.Enums;
using TCC.Domain.Models;

namespace TCC.Application.ViewModels
{
    public class ProgressoAulaViewModel
    {
        [Key]
        public Guid Id { get; set; }

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

        public List<RespostaAlunoExercicioViewModel> RespostasExercicios { get; set; }
    }
}
