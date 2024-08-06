using apiCuentas.helpers;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace apiCuentas.Controllers
{
    [ApiController]
    [Route("/personas")]
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


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Persona persona)
        {
            try
            {
                var created = await servicePersona.CreatePersona(persona);
                if ( created!=null)
                {
                    return Ok(authHelpers.GenerateJWTToken(created));
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

        [HttpPost("/login")]
        public async Task<ActionResult> login([FromBody]Persona persona)
        {
            var auth = new AuthHelpers();
            var logged = await servicePersona.login(persona);

            if (logged != null && logged.Id > 0) {
                return Ok(auth.GenerateJWTToken(logged));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("/info")]
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
    }
}
