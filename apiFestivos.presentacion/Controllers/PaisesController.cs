using apiFestivos.core.servicios;
using apiFestivos.dominio;
using Microsoft.AspNetCore.Mvc;

namespace apiFestivos.presentacion.Controllers
{
    [Route("api/paises")]
    [ApiController]
    public class PaisesController : Controller
    {
        private readonly IPaisServicio servicio;

        public PaisesController(IPaisServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pais>))]
        public async Task<ActionResult<IEnumerable<Pais>>> ObtenerTodos()
        {
            var lista = await servicio.ObtenerTodos();
            return Ok(lista);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Pais))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]
        public async Task<ActionResult<Pais>> Agregar([FromBody] Pais pais)
        {
            try
            {
                var nuevoPais = await servicio.Agregar(pais);
                return CreatedAtAction(nameof(Obtener), new { Id = nuevoPais.Id }, nuevoPais);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpGet("buscar/{IndiceDato:int}/{Texto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pais>))]
        public async Task<ActionResult<IEnumerable<Pais>>> Buscar(int IndiceDato, string Texto)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pais))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        public async Task<ActionResult<Pais>> Obtener(int Id)
        {
            var pais = await servicio.Obtener(Id);
            if (pais == null)
            {
                return NotFound(new { mensaje = $"No se encontró el Pais con ID= {Id}" });
            }
            return Ok(pais);
        }

        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pais))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]
        public async Task<ActionResult<Pais>> Modificar(int Id, [FromBody] Pais pais)
        {
            pais.Id = Id;

            try
            {
                var paisModificado = await servicio.Modificar(pais);
                return Ok(paisModificado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }
    }
}
