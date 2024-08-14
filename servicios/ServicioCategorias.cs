using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ServicioCategorias
    {
        public async Task<List<Categoria>> getCategorias(int idPersona)
        {
            using (var db = new PostgresContext())
            {
                return await db.CategoriaContext.Where(x => x.PersonaId == idPersona).ToListAsync()?? new List<Categoria>();
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
                Categoria categoriaReturn =  db.CategoriaContext.Add(categoria).Entity;
                db.SaveChangesAsync();
                return categoriaReturn;

            }
        }
    }
}
