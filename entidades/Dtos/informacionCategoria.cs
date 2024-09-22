using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades.Dtos
{
    public class informacionCategoria
    {

        public int Id { get; set; }
        public string? CategoriaNo { get; set; }
        public string? color { get; set; }
    }
}
