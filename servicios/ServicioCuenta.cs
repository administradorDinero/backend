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
                    Transacciones = c.Transaccions?.Select(t => new InformacionTransaccionDto
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
        /// Obtener informacion en especifico de una cuenta
        /// </summary>
        /// <param name="id">identificacion de la persona</param>
        /// <param name="id_cuenta">Identificacion de la cuenta asociada</param>
        /// <returns>Retornara la informacion asociada a la cuenta</returns>
        public async Task<InformacionCuentaDto> obtenerCuenta(int id,int id_cuenta)
        {
            using (var db = new PostgresContext())
            {
                Cuenta? cuenta = await db.CuentaContext.Include(c => c.Transaccions.OrderByDescending(t => t.fecha)).Where(x => x.persona.Id == id && x.Id== id_cuenta).FirstOrDefaultAsync() ;

                if (cuenta == null)
                {
                    return null;
                }
                var cuentasDto = new InformacionCuentaDto
                {
                    Id = cuenta.Id,
                    Valor = cuenta.valor ?? 0,
                    Descripcion = cuenta.descripcion,
                    color = cuenta.color,
                    Transacciones = cuenta.Transaccions?.Select(t => new InformacionTransaccionDto
                    {
                        Id = t.Id,
                        Cantidad = t.cantidad,
                        Fecha = t.fecha,
                        Descripcion = t.descripcion,
                        CuentaId = cuenta.Id
                    }).ToList()
                };

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
        /// <summary>
        /// Eliminar cuenta apartir del id
        /// </summary>
        /// <param name="idcuenta">Id de la cuenta a eliminar</param>
        /// <param name="id"></param>
        /// <returns>Retorna la cuenta eliminada</returns>
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
        /// <summary>
        /// Modificar la cuenta con las propiedades nuevas
        /// </summary>
        /// <param name="cuentaActualizada">Propiedades a actualizar</param>
        /// <param name="idUser"> id del usuario</param>
        /// <returns>Retorna True/False</returns>
        public async Task<bool> ActualizarCuentaAsync( ModificarCuentaDto cuentaActualizada, int idUser)
        {
            using (var contexto = new PostgresContext()) 
            {
                 var personaExistente = await contexto.PersonaContext.FindAsync(idUser);
                if (personaExistente == null)
                {
                    return false;
                }

                var cuentaExistente = await contexto.CuentaContext.FirstOrDefaultAsync(c => c.Id == cuentaActualizada.Id);

                if (cuentaExistente == null)
                {
                    return false; 
                }
                cuentaExistente.valor = cuentaActualizada.valor;
                cuentaExistente.descripcion = cuentaActualizada.descripcion;
                cuentaExistente.color = cuentaActualizada.color;
                await contexto.SaveChangesAsync();

                return true;
            }
        }

    }
}
