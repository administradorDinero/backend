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
        public async Task<List<CuentaDto>> obtenerCuentas(int id)
        {
            using (var db = new PostgresContext())
            {

                List<Cuenta> cuentas = await db.CuentaContext.Include(c=> c.Transaccions.OrderByDescending(t => t.fecha).Take(2)).Where(x => x.persona.Id == id).ToListAsync();
                var cuentasDto = cuentas.Select(c => new CuentaDto
                {
                    Id = c.Id,
                    Valor = c.valor,
                    Descripcion = c.descripcion,
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
    }
}
