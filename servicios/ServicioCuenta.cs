using Entidades;
using Entidades.Dtos;
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

       /// <summary>
       /// Obtener cuentas de un usuario
       /// </summary>
       /// <param name="id"></param>
       /// <returns>Devolvera la informacion de las cuentas asociadas al usuario</returns>
        public async Task<List<InformacionCuentaDto>> obtenerCuentas(int id)
        {
            using (var db = new PostgresContext())
            {
                var persona =await db.PersonaContext.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (persona==null)
                {
                    return null;
                }
                List<Cuenta> cuentas = await db.CuentaContext.Include(c=> c.Transaccions.OrderByDescending(t => t.fecha).Take(2)).Where(x => x.persona.Id == id).ToListAsync();
                var cuentasDto = cuentas.Select(c => new InformacionCuentaDto
                {
                    Id = c.Id,
                    Valor = c.valor??0,
                    Descripcion = c.descripcion,
                    color=c.color,
                    Transacciones = c.Transaccions?.Select(t => new TransaccionDto
                    {
                        Id = t.Id,
                        Cantidad = t.cantidad,
                        Fecha = t.fecha,
                        Descripcion = t.descripcion,
                        CuentaId = c.Id
                    }).ToList()
                }).ToList();
                if (cuentas == null)
                {
                    return null;
                }
                return cuentasDto;
     
            }
        }
        public async Task<List<CuentaDto>> obtenerCuenta(int id,int id_cuenta)
        {
            using (var db = new PostgresContext())
            {

                List<Cuenta> cuentas = await db.CuentaContext.Include(c => c.Transaccions.OrderByDescending(t => t.fecha)).Where(x => x.persona.Id == id && x.Id== id_cuenta).ToListAsync() ;
                var cuentasDto = cuentas.Select(c => new CuentaDto
                {
                    Id = c.Id,
                    Valor = c.valor,
                    Descripcion = c.descripcion,
                    color =c.color,
                    Transacciones = c.Transaccions?.Select(t => new TransaccionDto
                    {
                        Id = t.Id,
                        Cantidad = t.cantidad,
                        Fecha = t.fecha,
                        Descripcion = t.descripcion,
                        CuentaId = c.Id
                    }).ToList()
                }).ToList();
                if (cuentas == null)
                {
                    return null;
                }
                return cuentasDto;

            }
        }
        /// <summary>
        /// Creacion de una Cuenta apartir de un Dto
        /// </summary>
        /// <param name="NuevacuentaDto"></param>
        /// <param name="personaId"></param>
        /// <returns> Cuenta creada</returns>
        public async Task<Cuenta> crearCuentaAsync(CrearCuentaDto NuevacuentaDto,int personaId) {
            using (var db = new PostgresContext())
            {
                var personaExistente = await db.PersonaContext.FindAsync(personaId);
                if (personaExistente == null) {
                    return null;
                }
                Cuenta cuenta = new Cuenta
                {
                    valor=NuevacuentaDto.valor,
                    descripcion=NuevacuentaDto.descripcion
                };
                cuenta.persona = personaExistente;
                Cuenta created =db.CuentaContext.Add(cuenta).Entity;
                await db.SaveChangesAsync();
                return created;
            }
        }
        public async Task<int> deleteCuenta(int idcuenta, int id)
        {

            using (var db = new PostgresContext())
            {
                var personaExistente = await db.PersonaContext.FindAsync(id);
                if (personaExistente == null)
                {
                    return 0;
                }
                var deleted =await db.CuentaContext.Where(c => c.Id == idcuenta).ExecuteDeleteAsync();
                await db.SaveChangesAsync();
                return deleted;
            }
        }
        public async Task<bool> ActualizarCuentaAsync(CuentaDto cuentaActualizada)
        {
            using (var contexto = new PostgresContext()) // Asegúrate de usar tu propio DbContext
            {
                // Buscar la cuenta en la base de datos
                var cuentaExistente = await contexto.CuentaContext.FirstOrDefaultAsync(c => c.Id == cuentaActualizada.Id);

                if (cuentaExistente == null)
                {
                    return false; 
                }
                cuentaExistente.valor = cuentaActualizada.Valor;
                cuentaExistente.descripcion = cuentaActualizada.Descripcion;
                cuentaExistente.color = cuentaActualizada.color;
                await contexto.SaveChangesAsync();

                return true;
            }
        }

    }
}
