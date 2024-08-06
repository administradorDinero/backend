using entidades;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio;
using Servicios.validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ServicioCuenta
    {

        CuentaValidacion cuentaValidacion;

        public ServicioCuenta()
        {
            this.cuentaValidacion = new CuentaValidacion();
        }
        public async Task<List<Cuenta>> obtenerCuenta(int id)
        {
            using (var db = new PostgresContext())
            {

                List<Cuenta> cuentas = await db.CuentaContext.Where(x => x.persona.Id == id).ToListAsync();

                if (cuentas == null)
                {
                    return null;
                }
                return cuentas;
            }
        }
        public async Task<Cuenta> crearCuentaAsync(Cuenta cuenta,int personaId) {


            var results =cuentaValidacion.Validate(cuenta);

            foreach (var item in results.Errors)
            {
                Console.WriteLine(item.ErrorCode);
            }
            if (!results.IsValid) {
                return null;
            }
            
            using (var db = new PostgresContext())
            {
                var personaExistente = await db.PersonaContext.FindAsync(personaId);
                if (personaExistente == null) {
                    return null;
                }
                cuenta.persona = personaExistente;
                Cuenta created =db.CuentaContext.Add(cuenta).Entity;
                await db.SaveChangesAsync();
                return created;
            }

        }
        public async Task<int> deleteCuenta(Cuenta cuenta, int id)
        {

            using (var db = new PostgresContext())
            {
                var personaExistente = await db.PersonaContext.FindAsync(id);
                if (personaExistente == null)
                {
                    return 0;
                }
                var deleted =await db.CuentaContext.Where(c => c.Id == cuenta.Id).ExecuteDeleteAsync();
                await db.SaveChangesAsync();
                return deleted;
            }
        }
    }
}
