using Entidades;
using Entidades.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System.Security.Claims;

namespace apiCuentas.Controllers
{


    /// <summary>
    /// Controlador de las cuentas de los clientes;Donde podras realizar el proceso de CRUD basico y dto para obtener datos
    /// </summary>

    [ApiController]
    [Route("/Cuentas")]

    public class CuentaController:Controller
    {
        private readonly ILogger<CuentaController> _logger;

        ServicioCuenta servicioCuenta;
        public CuentaController(ServicioCuenta servicioCuenta, ILogger<CuentaController> logger) {
            this.servicioCuenta = servicioCuenta;
            this._logger = logger;
        }


        /// <summary>
        /// Creacion de una nueva por medio de un dto descripcion y cantidad inicial
        /// </summary>
        /// <param name="cuenta">CuentaDto</param>
        /// <returns> Dto</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Cuenta>> postCuenta(CrearCuentaDto cuenta)
        {
            var claims = User.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            }).ToList();
            var claim = claims.Find(x => x.Type == "id");
            if (claim == null)
            {
                return BadRequest();
            }
            var id = int.Parse(claim.Value.ToString());
            var cuentaCreada=await servicioCuenta.crearCuentaAsync(cuenta, id);
            if (cuentaCreada == null)
            {
                return BadRequest("No se creo de forma correcta");
            }
            return Ok(cuentaCreada);
        }
        /// <summary>
        /// Obtener las cuentas asociadas a un usuario por medio del token
        /// </summary>
        /// <returns> retorna las cuentas asociadas al usuario</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> getCuentas()
        {
            var claims = User.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            }).ToList();
            var claim = claims.Find(x => x.Type == "id");
            if (claim == null)
            {
                return BadRequest();
            }
            var result = await servicioCuenta.obtenerCuentas(int.Parse(claim.Value.ToString()));
            return Ok(result);
        }

        [HttpGet("{idcuenta}")]
        [Authorize]
        public async Task<ActionResult> getCuenta(int idcuenta)
        {
            var claims = User.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            }).ToList();
            var claim = claims.Find(x => x.Type == "id");
            if (claim == null)
            {
                return BadRequest();
            }
            var result = await servicioCuenta.obtenerCuenta(int.Parse(claim.Value.ToString()),idcuenta);
            return Ok(result);
        }

        [HttpDelete("{idcuenta}")]
        [Authorize]
        public async Task<ActionResult> RemoveCuenta(int idcuenta)      
        {
            var claims = User.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            }).ToList();
            var claim = claims.Find(x => x.Type == "id");
            if (claim == null)
            {
                return BadRequest();
            }
            var id = int.Parse(claim.Value.ToString());

            var result =await servicioCuenta.deleteCuenta(idcuenta, id);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> PutCuenta(CuentaDto cuenta)
        {
            var claims = User.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            }).ToList();
            var claim = claims.Find(x => x.Type == "id");
            if (claim == null)
            {
                return BadRequest();
            }
            var id = int.Parse(claim.Value.ToString());
           
            var result = await servicioCuenta.ActualizarCuentaAsync(cuenta);
            return Ok(result);
        }
    }
}
