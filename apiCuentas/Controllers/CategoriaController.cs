using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace apiCuentas.Controllers
{

    /// <summary>
    /// Controllador para la categorias que representan una transacción
    /// </summary>

    [ApiController]
    [Route("/Categorias")]
    public class CategoriaController : Controller
    {

        private readonly ILogger<CategoriaController> _logger;
        /// <value>
        /// Servicio de categorias
        /// </value>
        ServicioCategorias servicioCategoria;
        public CategoriaController(ServicioCategorias servicioCuenta, ILogger<CategoriaController> logger)
        {
            this.servicioCategoria = servicioCuenta;
            this._logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> getCategorias()
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
            var idPersona = int.Parse(claim.Value.ToString());

            var categoriasPersona = await servicioCategoria.getCategorias(idPersona);
            return Ok(categoriasPersona);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> createCategorias(Categoria categoria)
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
            var idPersona = int.Parse(claim.Value.ToString());

            var categoriaPersona = await servicioCategoria.createCategorias(categoria,idPersona);
            return Ok(categoriaPersona);
        }

      /// <param name="id">ID de la categoria.</param>
      /// <returns>Categoria eliminada.</returns>
      /// <response code="200">Devuelve la cuenta si fue eliminada.</response>
      /// <response code="404">Si no se usa jwt.</response>
        [HttpDelete("{idCategoria}")]
        [Authorize]
        public async Task<ActionResult> DeleteCategoria(int idCategoria)
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
            var idPersona = int.Parse(claim.Value.ToString());
            var categoriaPersona = await servicioCategoria.EliminarCategoria(idCategoria, idPersona);
            return Ok(categoriaPersona);
        }
        [HttpGet("ByTransacciones")]
        [Authorize]
        public async Task<ActionResult> categoriaByTransaccion(int Paginacion=1,int tamaño=20)
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
            var idPersona = int.Parse(claim.Value.ToString());
            var categoriaPersona = await servicioCategoria.CategoriaByTransaccion(idPersona,Paginacion,tamaño);
            return Ok(categoriaPersona);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> putCategoria(Categoria categoria)
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
            var idPersona = int.Parse(claim.Value.ToString());

            var categoriaPersona = await servicioCategoria.Categoriaput(idPersona, categoria);
            if (categoriaPersona == true) { 
                return Ok();
            }
            return BadRequest();
        }

    }
}
