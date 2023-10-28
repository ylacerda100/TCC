using NetDevPack.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCC.Domain.Models
{
    public class RespostaAlunoExercicio : Entity, IAggregateRoot
    {
        public RespostaAlunoExercicio()
        {
            
        }

        [ForeignKey("Usuario")]
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }


        [ForeignKey("Exercicio")]
        public Guid ExercicioId { get; set; }
        public Exercicio Exercicio { get; set; }
        public string Resposta { get; set; }
    }
}
