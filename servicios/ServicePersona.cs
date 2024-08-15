using apiCuentas.Validations;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio;
using Servicios.seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ServicePersona
    {


        public async Task<Persona> CreatePersona(Persona persona)
        {

            PersonaValidation validationRules = new PersonaValidation();

            var result =await validationRules.ValidateAsync(persona);
            if (!result.IsValid)
            {
                result.Errors.ForEach(x =>
                {
                    Console.WriteLine(x.ToString());
                }
                 );
                return persona;
            }
            Console.WriteLine(persona.correo);
            using (var db = new PostgresContext())
            {
                    persona.clave = Encryptacion.HashPassword(persona.clave);
                    var nueva= db.PersonaContext.Add(persona);
                    await db.SaveChangesAsync();
                    return nueva.Entity;
            }
        }
        public async Task<Persona> login(Persona persona)
        {

            using (var db = new PostgresContext())
            {
                var personaValidation = await db.PersonaContext.FirstOrDefaultAsync( x => x.correo ==persona.correo);
                if (personaValidation == null)
                {
                    return persona;
                }
                
                if(Encryptacion.VerifyPassword(persona.clave,personaValidation.clave))
                {
                    return personaValidation;
                }
                return persona;
            }

        }
        public async Task<PersonaDto> informacion(int id)
        {
            
            using(var db = new PostgresContext())
            {

                 return await db.PersonaContext
            .Where(x => x.Id == id)
            .Select(x => new PersonaDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                correo = x.correo,
                Cuentas =x.Cuentas
            })
            .FirstOrDefaultAsync();  
            }
        }
        public async Task<String> DeletePersona( int id)
        {
            using (var db = new PostgresContext())
            {
                try
                {
                    var find =db.PersonaContext.Find(id); 
                    if(find ==null)return "Not found";
                    db.PersonaContext.Remove(find);
                    await db.SaveChangesAsync();
                    return "Eliminado Con exito";
                }
                catch
                {
                    return "No se elimino este ID";
                }
            }
        }
    }
}
