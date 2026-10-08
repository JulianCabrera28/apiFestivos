using apiFestivos.core.servicios;
using apiFestivos.dominio;
using Microsoft.AspNetCore.Mvc;

namespace apiFestivos.presentacion.Controllers
{
    [Route("api/tipos")]
    [ApiController]
    public class TiposController : Controller
    {
        private readonly ITipoServicio servicio;

        public TiposController(ITipoServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Tipo>))]
        public async Task<ActionResult<IEnumerable<Tipo>>> ObtenerTodos()
        {
            var lista = await servicio.ObtenerTodos();
            return Ok(lista);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Tipo))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]
        public async Task<ActionResult<Tipo>> Agregar([FromBody] Tipo tipo)
        {
            try
            {
                var nuevoTipo = await servicio.Agregar(tipo);
                return CreatedAtAction(nameof(Obtener), new { Id = nuevoTipo.Id }, nuevoTipo);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpGet("buscar/{IndiceDato:int}/{Texto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Tipo>))]
        public async Task<ActionResult<IEnumerable<Tipo>>> Buscar(int IndiceDato, string Texto)
        {
            var lista = await servicio.Buscar(IndiceDato, Texto);
            return Ok(lista);
        }

        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]
        public async Task<ActionResult<bool>> Eliminar(int Id)
        {
            try
            {
                bool eliminado = await servicio.Eliminar(Id);
                return Ok(eliminado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tipo))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        public async Task<ActionResult<Tipo>> Obtener(int Id)
        {
            var tipo = await servicio.Obtener(Id);
            if (tipo == null)
            {
                return NotFound(new { mensaje = $"No se encontró el Tipo con ID= {Id}" });
            }
            return Ok(tipo);
        }

        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tipo))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]
        public async Task<ActionResult<Tipo>> Modificar(int Id, [FromBody] Tipo tipo)
        {
            tipo.Id = Id;

            try
            {
                var tipoModificado = await servicio.Modificar(tipo);
                return Ok(tipoModificado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }
    }
}
