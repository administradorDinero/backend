using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades
{
    public class Persona
    {

        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? clave { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual ICollection<Cuenta>? Cuentas { get; set;} = new HashSet<Cuenta>();
        [JsonIgnore]
        public virtual ICollection<Categoria>? Categorias{ get; set; }

    }
}
