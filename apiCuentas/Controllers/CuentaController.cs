using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System.Security.Claims;

namespace apiCuentas.Controllers
{
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
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> postCuenta(Cuenta cuenta)
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
            _logger.LogInformation(cuenta.Id.ToString());
            var cuentaCreada=await servicioCuenta.crearCuentaAsync(cuenta, id);
            return Ok(cuentaCreada);
        }

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
    }
}
