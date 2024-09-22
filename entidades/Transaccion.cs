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
    public  class Transaccion
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  
        public double cantidad {  get; set; }
        public DateTime fecha { get; set; }
        public String? descripcion { get; set; }
        public int CuentaId { get; set; }
        [JsonIgnore]
        public Cuenta? cuenta { get; set; } = null;
        public Categoria? categoria { get; set; }
        [JsonIgnore]
        public Tipo? tipo { get; set; }

    }
}
