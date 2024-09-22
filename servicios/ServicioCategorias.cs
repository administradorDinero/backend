using Entidades;
using Entidades.Dtos;
using Microsoft.EntityFrameworkCore;
using Repositorio;

namespace Servicios
{
    /// <summary>
    /// Contiene toda la logica para el tratamiento de categorias 
    /// </summary>
    public class ServicioCategorias
    {
        /// <summary>
        /// Informacion de las categorias asociadas a cada usuari
        /// </summary>
        /// <param name="idPersona"> id del usuario</param>
        /// <returns>Arreglo de la informacion de los usuarios</returns>
        public async Task<List<informacionCategoria>> getCategorias(int idPersona)
        {
            using (var db = new PostgresContext())
            {
                return await db.CategoriaContext.Where(x => x.PersonaId == idPersona && x.Estado.Id ==2).Select(x=> new informacionCategoria {color=x.color,CategoriaNo=x.CategoriaNo,Id=x.Id }).ToListAsync()?? new List<informacionCategoria>();
            }

        }
        /// <summary>
        /// Agregar categoaria al usuario
        /// </summary>
        /// <param name="categoria">Categoria a añadir</param>
        /// <param name="idPersona">id del usuario</param>
        /// <returns></returns>
        public async Task<Categoria> createCategorias(informacionCategoria categoria,int idPersona)
        {
            using (var db = new PostgresContext())
            {
                Persona? persona = await db.PersonaContext.FirstOrDefaultAsync(x => x.Id == idPersona);

            if (persona == null)
                {
                    return null;
                }
               var nuevaCategoria= new Categoria
                {
                    Persona = persona,
                    PersonaId = persona.Id,
                    EstadoId=2,
                    Estado = await db.EstadoContext.FindAsync(2),
                    CategoriaNo=categoria.CategoriaNo,
                    color=categoria.color
                };
                Categoria categoriaReturn =  db.CategoriaContext.Add(nuevaCategoria).Entity;
                await db.SaveChangesAsync();
                return categoriaReturn;
            }
        }/// <summary>
        /// Eliminar categoria apartir del id
        /// </summary>
        /// <param name="id"> id categoria</param>
        /// <param name="idPersona">id persona</param>
        /// <returns>null en caso de fallos</returns>
        public async Task<Categoria> EliminarCategoria(int id, int idPersona)
        {
            using (var db = new PostgresContext())
            {
                Persona? persona = await db.PersonaContext.FirstOrDefaultAsync(x => x.Id == idPersona);
                if (persona == null)
                {
                    return null;
                }
                Categoria? categoriaReturn = await db.CategoriaContext.Where(c => c.Id==id && c.PersonaId==idPersona).FirstOrDefaultAsync();
                if (categoriaReturn == null) {
                    return null;
                }
                categoriaReturn.Estado = await db.EstadoContext.FindAsync(1);
                await db.SaveChangesAsync();
                return categoriaReturn;

            }
        }
        public async Task<List<informacionCategoriaTransacciones>> CategoriaByTransaccion(int idPersona, int pageNumber, int pageSize)
        {
            using (var db = new PostgresContext())
            {
                Persona? persona = await db.PersonaContext.FirstOrDefaultAsync(x => x.Id == idPersona);
                if (persona == null)
                {
                    return null;
                }
                List<informacionCategoriaTransacciones> categoriasDto = await db.CategoriaContext.Where(c=>c.EstadoId==2)
                     .Select(c => new informacionCategoriaTransacciones
                     {
                         Id = c.Id,
                         CategoriaNo = c.CategoriaNo,
                         Transacciones = db.TransaccionContext.Where(t => t.categoria.Id==c.Id)
                             .Select(t => new InformacionTransaccionDto
                             {
                                 Id = t.Id,
                                 Cantidad = t.cantidad,
                                 Fecha = t.fecha,
                                 Descripcion = t.descripcion,
                                 CuentaId = t.CuentaId
                             }).ToList()
                     })
                     .ToListAsync();
                if (categoriasDto == null)
                {
                    return new List<informacionCategoriaTransacciones>();
                }

                return categoriasDto;

            }
        }

        public async Task<bool> Categoriaput(int idPersona,informacionCategoria categoria)
        {
            using (var contexto = new PostgresContext())
            {
                var categoriaExiste = await contexto.CategoriaContext.FirstOrDefaultAsync(c => c.Id == categoria.Id && c.PersonaId == idPersona && c.Estado.Id == 2);

                if (categoriaExiste == null)
                {
                    return false;
                }

                categoriaExiste.CategoriaNo = categoria.CategoriaNo;
                categoriaExiste.color = categoria.color;
                await contexto.SaveChangesAsync();

                return true;
            }


        }


    }
}
