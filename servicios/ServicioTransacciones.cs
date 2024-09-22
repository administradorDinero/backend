
using Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Data;
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

                if ((transaccion.categoria?.Id??0)!=0)
                {
                    var categoriaFind = await db.CategoriaContext.FindAsync(transaccion.categoria.Id);
                    transaccion.categoria = categoriaFind;
                    Console.WriteLine(categoriaFind.Id);
                }
                else
                {
                    var categoriaId = await db.CategoriaContext.Where(x => x.PersonaId == id && x.CategoriaNo == "Default").FirstOrDefaultAsync();
                    var estado = await db.EstadoContext.FindAsync(2);
                    if (categoriaId == null)
                    {
                        transaccion.categoria = db.CategoriaContext.Add(new Categoria { CategoriaNo = "Default", PersonaId = id, Estado = estado }).Entity;
                        db.SaveChanges();
                    }
                    else
                    {
                        transaccion.categoria = categoriaId;
                    }
                }
                
                
                var tipo = await db.TipoContext.FindAsync(0);
                var cuenta= await db.CuentaContext.FindAsync(transaccion.CuentaId);

                if (personaExistente == null|| transaccion.categoria==null|| tipo==null|| cuenta==null)
                {
                    return null;
                }
                transaccion.tipo = tipo;
                transaccion.cuenta = cuenta;
                Transaccion nuevaTransaccion = db.TransaccionContext.Add(transaccion).Entity;
                 db.SaveChanges();
                return nuevaTransaccion;
            }

        }
        public async Task<int> deleteTransaccion(int idTransaccion, int id)
        {
            using (var db = new PostgresContext())
            {
                var personaExistente = await db.PersonaContext.FindAsync(id);
                var transaccionExiste= await db.TransaccionContext.FindAsync(idTransaccion);

                if (personaExistente == null || transaccionExiste== null)
                {
                    return 0;
                }

                var nuevaTransaccion =  db.TransaccionContext.Where(tr=>tr.Id==idTransaccion).ExecuteDelete();
                await db.SaveChangesAsync();
                return nuevaTransaccion;
            }
        }

        public async Task<List<TransaccionDto>> TransaccionesUsuario(int id, int pageNumber, int pageSize)
        {
            using (var db = new PostgresContext())
            {
                var personaExistente = await db.PersonaContext.FindAsync(id);

                if (personaExistente == null)
                {
                    return null;
                }
                var transacciones = await db.CuentaContext
                 .Where(c => c.persona.Id == id)
                 .SelectMany(c => c.Transaccions.Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
                     .OrderByDescending(t => t.fecha).Select(t => new TransaccionDto
                     {
                         Id = t.Id,
                         Cantidad = t.cantidad,
                         Fecha = t.fecha,
                         Descripcion = t.descripcion,
                         CuentaId = c.Id,
                         ColorCuenta=c.color,
                         categoria= t.categoria
                     }).ToList())
                 .ToListAsync();

                return transacciones;
            }
        }
        public async Task<bool> ActualizarTransaccionAsync(TransaccionDto transaccion, int id)
        {
            using (var contexto = new PostgresContext()) 
            {
                var transaccionExistente = await contexto.TransaccionContext.FirstOrDefaultAsync(c => c.Id == transaccion.Id);

                if (transaccionExistente == null)
                {
                    return false;
                }
                if ((transaccion.categoria?.Id ?? 0) != 0)
                {
                    var categoriaFind = await contexto.CategoriaContext.FindAsync(transaccion.categoria.Id);
                    transaccionExistente.categoria =categoriaFind;
                }
                var cuenta = await contexto.CuentaContext.FindAsync(transaccion.CuentaId);

                if (cuenta!=null)
                {
                    transaccionExistente.cuenta =cuenta;
                }
                transaccionExistente.cantidad = transaccion.Cantidad;
                transaccionExistente.descripcion = transaccion.Descripcion;
                transaccionExistente.fecha = transaccion.Fecha;
                await contexto.SaveChangesAsync();

                return true;
            }
        }
    }
}
