
using Entidades;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ServicioTransacciones
    {


    public async Task<Transaccion>  InsertTransaccion(Transaccion transaccion,int id)
        {

            using(var db = new PostgresContext())
            {
                var personaExistente = await db.PersonaContext.FindAsync(id);
                var categoria = await db.CategoriaContext.FindAsync(0);
                var tipo = await db.TipoContext.FindAsync(0);
                var cuenta= await db.CuentaContext.FindAsync(transaccion.cuenta.Id);

                if (personaExistente == null|| categoria==null|| tipo==null|| cuenta==null)
                {
                    return null;
                }
                transaccion.cuenta.persona = personaExistente;
                transaccion.categoria = categoria;
                transaccion.tipo = tipo;
                transaccion.cuenta = cuenta;
                Transaccion nuevaTransaccion = db.TransaccionContext.Add(transaccion).Entity;
                await db.SaveChangesAsync();
                return nuevaTransaccion;
            }

        }
    }
}
