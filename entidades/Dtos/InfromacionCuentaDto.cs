﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Dtos
{
    public class InformacionCuentaDto
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public string? Descripcion { get; set; }
        public List<InformacionTransaccionDto>? Transacciones { get; set; }
        public string? color { get; set; }
    }
}
