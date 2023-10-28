using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Domain.Enums
{
    public enum TipoItemLoja
    {
        [Display(Name = "Boost")]
        Boost = 1,

        [Display(Name = "Pacote de XP")]
        PacoteXp = 2
    }
}
