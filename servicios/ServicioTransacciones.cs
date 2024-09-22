
using Entidades;
using Entidades.Dtos;
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


        /// <summary>
        /// Ingresar una nueva transaccion, en dado caso no exista la categoria se crea una nuev
        /// </summary>
        /// <param name="transaccion">Informacion basica de una transaccion</param>
        /// <param name="id">id del usuario</param>
        /// <returns>Transaccion creada</returns>
    public async Task<Transaccion>  InsertTransaccion(CrearNuevaTransaccionDto transaccion,int id)
        {

            using(var db = new PostgresContext())
            {
                var personaExistente = await db.PersonaContext.FindAsync(id);

                var nuevaTransaccion = new Transaccion();
                if (transaccion.idCategoria!=0)
                {
                    var categoriaFind = await db.CategoriaContext.FindAsync(transaccion.idCategoria);
                    nuevaTransaccion.categoria = categoriaFind;
                }
                else
                {
                    var categoriaId = await db.CategoriaContext.Where(x => x.PersonaId == id && x.CategoriaNo == "Default").FirstOrDefaultAsync();
                    var estado = await db.EstadoContext.FindAsync(2);
                    if (categoriaId == null)
                    {
                        nuevaTransaccion.categoria = db.CategoriaContext.Add(new Categoria { CategoriaNo = "Default", PersonaId = id, Estado = estado }).Entity;
                        db.SaveChanges();
                    }
                    else
                    {
                        nuevaTransaccion.categoria = categoriaId;
                    }
                }
                
                
                var tipo = await db.TipoContext.FindAsync(0);
                var cuenta= await db.CuentaContext.FindAsync(transaccion.idCuenta);

                if (personaExistente == null|| nuevaTransaccion.categoria==null|| tipo==null|| cuenta==null)
                {
                    return null;
                }
                nuevaTransaccion.tipo = tipo;
                nuevaTransaccion.cuenta = cuenta;
                nuevaTransaccion.cantidad = transaccion.cantidad;
                nuevaTransaccion.fecha = transaccion.fecha;
                nuevaTransaccion.descripcion = transaccion.descripcion;
                Transaccion transaccionGuardada = db.TransaccionContext.Add(nuevaTransaccion).Entity;
                 db.SaveChanges();
                return transaccionGuardada;
            }

        }
        /// <summary>
        /// Eliminar transaccion dependiendo del id de la misma
        /// </summary>
        /// <param name="idTransaccion">id de la transaccion</param>
        /// <param name="id">id de la persona</param>
        /// <returns>Depende de la cantidad de filas eliminadas en la base de datos deberia ser siempre 1</returns>
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
        /// <summary>
        /// Obtener las transacciones de un usuario
        /// </summary>
        /// <param name="id">id de un usarios</param>
        /// <param name="pageNumber">pagina de transacciones</param>
        /// <param name="pageSize">tamaño</param>
        /// <returns>Lista con la informacion de las transacciones</returns>
        public async Task<List<InformacionTransaccionDto>> TransaccionesUsuario(int id, int pageNumber, int pageSize)
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
                     .OrderByDescending(t => t.fecha).Select(t => new InformacionTransaccionDto
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
        /// <summary>
        /// Modificacion de una transaccion
        /// </summary>
        /// <param name="transaccion">Informacion especifica para actualizar para una transaccion</param>
        /// <param name="id">id del usuario</param>
        /// <returns>True/False</returns>
        public async Task<bool> ActualizarTransaccionAsync(informacionEspecificaTransaccionDto transaccion, int id)
        {
            using (var contexto = new PostgresContext()) 
            {
                var transaccionExistente = await contexto.TransaccionContext.FirstOrDefaultAsync(c => c.Id == transaccion.Id);

                if (transaccionExistente == null)
                {
                    return false;
                }
                if (transaccion.idCategoria  != 0)
                {
                    var categoriaFind = await contexto.CategoriaContext.FindAsync(transaccion.idCategoria);
                    transaccionExistente.categoria =categoriaFind;
                }
                var cuenta = await contexto.CuentaContext.FindAsync(transaccion.idCuenta);

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
