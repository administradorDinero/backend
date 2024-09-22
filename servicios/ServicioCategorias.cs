using Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio;
using static Entidades.categoriaDto;

namespace Servicios
{
    /// <summary>
    /// Contiene toda la logica para el tratamiento de categorias 
    /// </summary>
    public class ServicioCategorias
    {
        public async Task<List<Categoria>> getCategorias(int idPersona)
        {
            using (var db = new PostgresContext())
            {
                return await db.CategoriaContext.Where(x => x.PersonaId == idPersona && x.Estado.Id ==2).ToListAsync()?? new List<Categoria>();
            }

        }

        public async Task<Categoria> createCategorias(Categoria categoria,int idPersona)
        {
            using (var db = new PostgresContext())
            {
                Persona? persona = await db.PersonaContext.FirstOrDefaultAsync(x => x.Id == idPersona);

            if (persona == null)
                {
                    return null;
                }
                categoria.PersonaId = idPersona;
                categoria.Persona = persona;
                categoria.Estado = await db.EstadoContext.FindAsync(2);
                categoria.EstadoId = categoria.Estado.Id;
                Categoria categoriaReturn =  db.CategoriaContext.Add(categoria).Entity;
                await db.SaveChangesAsync();
                return categoriaReturn;

            }
        }
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
        public async Task<List<categoriaDto>> CategoriaByTransaccion(int idPersona, int pageNumber, int pageSize)
        {
            using (var db = new PostgresContext())
            {
                Persona? persona = await db.PersonaContext.FirstOrDefaultAsync(x => x.Id == idPersona);
                if (persona == null)
                {
                    return null;
                }
                List<categoriaDto> categoriasDto = await db.CategoriaContext.Where(c=>c.EstadoId==2)
                     .Select(c => new categoriaDto
                     {
                         Id = c.Id,
                         CategoriaNo = c.CategoriaNo,
                         Transacciones = db.TransaccionContext.Where(t => t.categoria.Id==c.Id)
                             .Select(t => new TransaccionDto
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
                    return new List<categoriaDto>();
                }

                return categoriasDto;

            }
        }

        public async Task<bool> Categoriaput(int idPersona,Categoria categoria)
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
