using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC.Domain.Models;

namespace TCC.Infra.Data.Mappings
{
    public class RespostaAlunoExercicioMap : IEntityTypeConfiguration<RespostaAlunoExercicio>
    {
        public void Configure(EntityTypeBuilder<RespostaAlunoExercicio> builder)
        {
            builder.Property(c => c.Resposta)
               .HasColumnType("varchar(max)")
               .IsRequired();
        }
    }
}
