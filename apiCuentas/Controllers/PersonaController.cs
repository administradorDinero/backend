            using apiCuentas.helpers;
using Entidades;
using Entidades.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace apiCuentas.Controllers
{

    /// <summary>
    /// Clase controladora de las acciones con los usuarios dentro de la aplicación
    /// </summary>
    [ApiController]
    [Route("/Usuarios")]
    public class PersonaController : Controller
    {
        ServicePersona servicePersona { get; set; }
        AuthHelpers authHelpers { get; set; }
        private readonly ILogger<PersonaController> _logger;

        public PersonaController(ServicePersona servicePersona, AuthHelpers authHelpers, ILogger<PersonaController> logger)
        {
            this.servicePersona = servicePersona;
            this.authHelpers = authHelpers;
            _logger = logger;
        }

        /// <summary>
        /// Creacion de nuevos usuarios
        /// </summary>
        /// <param name="persona"> Recibe un parametro personas</param>
        /// <returns> Retorna el token para el inicio de sesion del usuario</returns>

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreacionUsuarioDto persona)
        {
            try
            {
                var created = await servicePersona.CreatePersona(persona);
                if ( created!=null)
                {
                    return Ok(new { token = authHelpers.GenerateJWTToken(created) });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating persona");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Control de inicio de sesión para los usuarios
        /// </summary>
        /// <param name="persona"></param>
        /// <returns>Retorna el token en dado caso las credenciales sean correctas</returns>
        [HttpPost("login")]
        public async Task<ActionResult> login([FromBody]CredencialesUsuarioDto persona)
        {
            var auth = new AuthHelpers();
            var logged = await servicePersona.login(persona);

            if (logged != null && logged.Id > 0) {
                return Ok(new{token=auth.GenerateJWTToken(logged)});
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Informacion de operaciones de los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet("info")]
        [Authorize]
        public async Task<ActionResult> informacionPersona()
        {
            var claims = User.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            }).ToList();
            var claim =claims.Find(x => x.Type == "id");
            if (claim== null)
            {
                return BadRequest();
            }
             var result= await servicePersona.informacion(int.Parse(claim.Value.ToString()));
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }


        [HttpGet("/ValidToken")]
        [Authorize]
        public async Task<ActionResult> validarToken()
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
            var result = await servicePersona.informacion(int.Parse(claim.Value.ToString()));
            if (result == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
