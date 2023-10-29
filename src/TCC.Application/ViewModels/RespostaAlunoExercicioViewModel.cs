using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCC.Domain.Models;

namespace TCC.Application.ViewModels
{
    public class RespostaAlunoExercicioViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Usuario")]
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("ProgressoAula")]
        public Guid ProgressoAulaId { get; set; }
        public ProgressoAula ProgressoAula { get; set; }

        [ForeignKey("Exercicio")]
        public Guid ExercicioId { get; set; }
        public Exercicio Exercicio { get; set; }
        public string Resposta { get; set; }
    }
}
