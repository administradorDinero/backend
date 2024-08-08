using entidades;
using FluentValidation;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.validaciones
{
    internal class CuentaValidacion:AbstractValidator<Cuenta>
    {

        public CuentaValidacion()
        {
            RuleFor(cuenta => cuenta.descripcion).NotNull().NotEmpty().MaximumLength(20).Must(ContentEmpty).WithMessage("No puede contener espacios en blanco");
           // RuleFor(cuenta => cuenta.valor).Must(MustBeCero).WithMessage("El valor deber ser igual a cero").Unless(cuenta =>cuenta.Id !=0); 
        }
        public bool MustBeCero(double valor)
        {
            return valor.Equals(0);
        }
        public bool ContentEmpty(string cadena)
        {
            return !cadena.Contains(" ");
        }
    }
}
