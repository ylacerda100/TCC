using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC.Domain.Models;

namespace TCC.Infra.Data.Mappings
{
    public class ExercicioMap : IEntityTypeConfiguration<Exercicio>
    {
        public void Configure(EntityTypeBuilder<Exercicio> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Enunciado)
               .HasColumnType("varchar(max)")
               .IsRequired();

            builder.Property(c => c.AlternativaA)
              .HasColumnType("varchar(max)")
              .IsRequired();

            builder.Property(c => c.AlternativaB)
              .HasColumnType("varchar(max)")
              .IsRequired();

            builder.Property(c => c.AlternativaC)
              .HasColumnType("varchar(max)")
              .IsRequired();

            builder.Property(c => c.AlternativaD)
              .HasColumnType("varchar(max)")
              .IsRequired();

            builder.Property(c => c.Resposta)
              .HasColumnType("varchar(max)")
              .IsRequired();
        }
    }
}
