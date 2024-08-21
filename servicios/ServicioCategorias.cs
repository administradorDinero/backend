using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Entidades.categoriaDto;

namespace Servicios
{
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
                categoria.Id = idPersona;
                categoria.Persona = persona;
                categoria.Estado = await db.EstadoContext.FindAsync(2);
                categoria.EstadoId = categoria.Estado.Id;
                Categoria categoriaReturn =  db.CategoriaContext.Add(categoria).Entity;
                await db.SaveChangesAsync();
                return categoriaReturn;

            }
        }
        public async Task<Categoria> EliminarCategoria(Categoria categoria, int idPersona)
        {
            using (var db = new PostgresContext())
            {
                Persona? persona = await db.PersonaContext.FirstOrDefaultAsync(x => x.Id == idPersona);
                if (persona == null)
                {
                    return null;
                }
                categoria.Id = idPersona;
                categoria.Persona = persona;
                Categoria categoriaReturn = await db.CategoriaContext.FindAsync(categoria);
                categoriaReturn.Estado = await db.EstadoContext.FindAsync(1);
                db.SaveChangesAsync();
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
                List<categoriaDto> categoriasDto = await db.CategoriaContext.Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
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


    }
}
