using apiCuentas.helpers;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace apiCuentas.Controllers
{
    [ApiController]
    [Route("/Transacciones")]
    public class TransaccionesController:Controller
    {
        private readonly ILogger<TransaccionesController> _logger;
        private ServicioTransacciones servicioTransacciones;
        private AuthHelpers authHelpers;

        public TransaccionesController(ServicioTransacciones servicioTransacciones, AuthHelpers authHelpers, ILogger<TransaccionesController> logger)
        {
            this.servicioTransacciones = servicioTransacciones;
            this.authHelpers = authHelpers;
            _logger = logger;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> newTransaccion(Transaccion tr)
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
            _logger.LogInformation(tr.CuentaId.ToString());
            Transaccion nuevatransaccion = await servicioTransacciones.InsertTransaccion(tr,id);
            return Ok(nuevatransaccion);
        }
        [Route("/status")]
        [HttpGet]
        public ActionResult status()
        {
            // Leer los encabezados
            foreach (var header in Request.Headers)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");
            }
            return Ok("All well"); 
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> getTransaciones()
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
            List<TransaccionDto> Transacciones= await servicioTransacciones.TransaccionesUsuario(id);
            return Ok(Transacciones);
        }

        [HttpDelete("{idTransaccion}")]

        [Authorize]
        public async Task<ActionResult> deleteTransaccion(int idTransaccion)
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
            int nuevatransaccion = await servicioTransacciones.deleteTransaccion(idTransaccion, id);
            return Ok(nuevatransaccion);
        }
        }
    }
