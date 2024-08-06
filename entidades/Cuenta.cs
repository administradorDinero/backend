using Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace entidades
{
    public class Cuenta
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }

        public double valor { get; set; }

        public string? descripcion { get; set; }
        [JsonIgnore]
        public Persona? persona { get; set; }
        public virtual ICollection<Transaccion>? Transaccions { get; set; }

    }
}
