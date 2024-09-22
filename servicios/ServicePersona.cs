using apiCuentas.Validations;
using Entidades;
using Entidades.Dtos;
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


        public async Task<Persona> CreatePersona(CreacionUsuarioDto persona)
        {

            PersonaValidation validationRules = new PersonaValidation();

            var result =await validationRules.ValidateAsync(new Persona { clave=persona.clave,correo=persona.correo,Nombre=persona.Nombre});
            if (!result.IsValid)
            {
                result.Errors.ForEach(x =>
                {
                    Console.WriteLine(x.ToString());
                }
                 );
                return null;
            }
            using (var db = new PostgresContext())
            {
                    persona.clave = Encryptacion.HashPassword(persona.clave);
                    var nueva= db.PersonaContext.Add(new Persona { correo=persona.correo,clave=persona.clave,Nombre=persona.Nombre});
                    await db.SaveChangesAsync();
                    return nueva.Entity;
            }
        }
        public async Task<Persona> login(CredencialesUsuarioDto persona)
        {

            using (var db = new PostgresContext())
            {
                var personaValidation = await db.PersonaContext.FirstOrDefaultAsync( x => x.correo ==persona.correo);
                if (personaValidation == null)
                {
                    return null;
                }
                
                if(Encryptacion.VerifyPassword(persona.clave,personaValidation.clave))
                {
                    return personaValidation;
                }
                return null;
            }

        }
        public async Task<InformacionPersonaDto> informacion(int id)
        {
            
            using(var db = new PostgresContext())
            {
                return await db.PersonaContext
    .Where(x => x.Id == id)
    .Select(x => new InformacionPersonaDto
    {
        Id = x.Id,
        Nombre = x.Nombre,
        correo = x.correo,
        Cuentas = x.Cuentas.ToList(), // Si quieres incluir las cuentas asociadas
        Categorias = x.Categorias.ToList() // Si quieres incluir las categorías asociadas
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
