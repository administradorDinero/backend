using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Dtos
{
    public class informacionCategoriaTransacciones :informacionCategoria
    {
        public List<InformacionTransaccionDto> Transacciones { get; set; } = new List<InformacionTransaccionDto>();
    }
}
