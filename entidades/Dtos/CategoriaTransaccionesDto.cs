using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Dtos
{
    public class CategoriaTransaccionesDto
    {
        public int Id { get; set; }
        public string CategoriaNo { get; set; }
        public List<TransaccionDto> Transacciones { get; set; } = new List<TransaccionDto>();
        public string? color { get; set; }

    }
}
