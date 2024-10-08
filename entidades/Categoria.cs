﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Entidades
{
    public class Categoria
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CategoriaNo { get; set; }
        [JsonIgnore]
        public int? PersonaId { get; set; }
        [JsonIgnore]
        public Persona Persona { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }

         public string? color { get; set; }
    }
}
