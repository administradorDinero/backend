﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Dtos
{
    public class InformacionTransaccionDto
    {
        public int Id { get; set; }
        public double Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public Categoria? categoria { get; set; }
        public int CuentaId { get; set; }
        public string? ColorCuenta { get; set; } 
    }
}
