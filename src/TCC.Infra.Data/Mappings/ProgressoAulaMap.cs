using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC.Domain.Models;

namespace TCC.Infra.Data.Mappings
{
    public class ProgressoAulaMap : IEntityTypeConfiguration<ProgressoAula>
    {
        public void Configure(EntityTypeBuilder<ProgressoAula> builder)
        {
        }
    }
}
