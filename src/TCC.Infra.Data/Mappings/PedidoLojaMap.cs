using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Domain.Models;

namespace TCC.Infra.Data.Mappings
{
    public class PedidoLojaMap : IEntityTypeConfiguration<PedidoLoja>
    {
        public void Configure(EntityTypeBuilder<PedidoLoja> builder)
        {
        }
    }
}
