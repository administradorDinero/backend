using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Dtos
{
    public class CrearNuevaTransaccionDto
    {
        public int idCategoria { get; set; }
        public int idCuenta { get; set; }
        public int personaId { get; set; }

        public double cantidad { get; set; }
        public DateTime fecha { get; set; }
        public String? descripcion { get; set; }
    }
}
