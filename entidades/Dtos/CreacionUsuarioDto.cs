using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entidades.Dtos
{

    public class CreacionUsuarioDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string clave { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
    }
}