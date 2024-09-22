using Entidades;
using Entidades.Dtos;
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
        /// <summary>
        /// Obtener las categorias activas de un usuario
        /// </summary>
        /// <returns> arreglo de las categorias del usuario</returns>
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
        /// <summary>
        /// Creacion de una nueva categoria
        /// </summary>
        /// <param name="categoria">Recibe una dto con la informacion de la categoria</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> createCategorias(informacionCategoria categoria)
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

/// <summary>
/// Eliminacion de una categoria apartir del id
/// </summary>
/// <param name="idCategoria"></param>
/// <returns>devuelve la categoria eliminada</returns>
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

        /// <summary>
        /// Obtener categorias con transacciones
        /// </summary>
        /// <param name="Paginacion"> pagina</param>
        /// <param name="tamaño">tamaño de la muestra</param>
        /// <returns>lista de la informacion de las categorias con las transacciones</returns>
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
        /// <summary>
        /// Modificacion de una categoria
        /// </summary>
        /// <param name="categoria">Informacion de la categoria puntual</param>
        /// <returns>True/False dependiendo del resultado</returns>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> putCategoria(informacionCategoria categoria)
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
