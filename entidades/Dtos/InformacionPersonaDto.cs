using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Dtos
{
    public class InformacionPersonaDto
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public string correo { get; set; } = string.Empty;

        public virtual ICollection<Cuenta>? Cuentas { get; set; } = new HashSet<Cuenta>();
        public virtual ICollection<Categoria>? Categorias { get; set; } = new HashSet<Categoria>();

    }
}
