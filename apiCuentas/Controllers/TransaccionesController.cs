using apiCuentas.helpers;
using Entidades;
using Entidades.Dtos;
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
        /// <summary>
        /// Creacion de una nueva transaccion 
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> newTransaccion(CrearNuevaTransaccionDto tr)
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
            Transaccion nuevatransaccion = await servicioTransacciones.InsertTransaccion(tr,id);
            return Ok(nuevatransaccion);
        }
        /// <summary>
        /// Estado del servicio
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Obtener transacciones de un usuario
        /// </summary>
        /// <param name="Paginacion">pagina de las transacciones</param>
        /// <param name="tamaño">tamaño dela muestra</param>
        /// <returns>Arreglo informacion de las transacciones</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> getTransaciones(int Paginacion = 1, int tamaño = 20)
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
            List<InformacionTransaccionDto> Transacciones= await servicioTransacciones.TransaccionesUsuario(id, Paginacion, tamaño);
            if (Transacciones == null)
            {
                return NoContent();
            }
            return Ok(Transacciones);
        }

        /// <summary>
        /// Elimiar una transaccion apartir del id de la transaccion
        /// </summary>
        /// <param name="idTransaccion">id de la transaccion</param>
        /// <returns>Booleano de la transaccion</returns>
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
            if (nuevatransaccion == 1)
            {
                return Ok(nuevatransaccion);

            }
            return NoContent();

        }

        /// <summary>
        /// Modificacion de una transaccion dependiendo de propiedades especificas
        /// </summary>
        /// <param name="transaccion">informacion especifica de la transaccion</param>
        /// <returns>True/False</returns>
        [HttpPut]

        [Authorize]
        public async Task<ActionResult> putTransaccion(informacionEspecificaTransaccionDto transaccion)
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
            bool nuevatransaccion = await servicioTransacciones.ActualizarTransaccionAsync(transaccion, id);


            if (nuevatransaccion == false)
            {
                return NoContent();
            }
            return Ok(nuevatransaccion);
        }
    }
}
