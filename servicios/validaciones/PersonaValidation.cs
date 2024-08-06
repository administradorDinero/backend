using Entidades;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repositorio;

namespace apiCuentas.Validations
{
    internal class PersonaValidation:AbstractValidator<Persona>
    {

        public PersonaValidation() { 
           RuleFor(persona=> persona.correo).NotNull().NotEmpty().EmailAddress().MustAsync(BeUniqueEmail).WithMessage("Correo ya existe.");
           //RuleFor(persona => persona.Id).;
            RuleFor(persona => persona.clave).NotNull().NotEmpty().MinimumLength(6).MaximumLength(6);
        }
        private async Task<bool> BeUniqueEmail(string correo, CancellationToken cancellationToken)
        {
            using (var db = new PostgresContext()) {
                return !await db.PersonaContext.AnyAsync(p => p.correo == correo, cancellationToken);
            }

        }
    }
}
