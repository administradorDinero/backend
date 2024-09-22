using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Dtos
{
    public class informacionEspecificaTransaccionDto
    {
        public int Id { get; set; }
        public double Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public int idCategoria { get; set; }
        public int idCuenta { get; set; }
    }
}
