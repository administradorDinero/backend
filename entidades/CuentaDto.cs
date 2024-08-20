using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CuentaDto
    {
        public int Id { get; set; }
        public double? Valor { get; set; }
        public string? Descripcion { get; set; }
        public List<TransaccionDto>? Transacciones  { get; set; }
    }
}
