using apiCuentas.helpers;
using Entidades;
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
        [HttpPost]
        public async Task<ActionResult> newTransaccion(Transaccion tr)
        {

            return Ok(tr);
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
    }
}
