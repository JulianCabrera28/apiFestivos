using apiFestivos.core.servicios;
using apiFestivos.dominio;
using Microsoft.AspNetCore.Mvc;

namespace apiFestivos.presentacion.Controllers
{
    [Route("api/festivos")]
    [ApiController]
    public class FestivosController : Controller
    {
        private readonly IFestivoServicio servicio;

        public FestivosController(IFestivoServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Festivo>))]
        public async Task<ActionResult<IEnumerable<Festivo>>> ObtenerTodos()
        {
            var lista = await servicio.ObtenerTodos();
            return Ok(lista);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Festivo))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]
        public async Task<ActionResult<Festivo>> Agregar([FromBody] Festivo festivo)
        {
            try
            {
                var nuevoFestivo = await servicio.Agregar(festivo);
                return CreatedAtAction(nameof(Obtener), new { Id = nuevoFestivo.Id }, nuevoFestivo);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpGet("buscar/{IndiceDato:int}/{Texto}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Festivo>))]
        public async Task<ActionResult<IEnumerable<Festivo>>> Buscar(int IndiceDato, string Texto)
        {
            var lista = await servicio.Buscar(IndiceDato, Texto);
            return Ok(lista);
        }

        [HttpGet("pais/{IdPais:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Festivo>))]
        public async Task<ActionResult<IEnumerable<Festivo>>> ObtenerPorPais(int IdPais)
        {
            var lista = await servicio.ObtenerPorPais(IdPais);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Festivo))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        public async Task<ActionResult<Festivo>> Obtener(int Id)
        {
            var festivo = await servicio.Obtener(Id);
            if (festivo == null)
            {
                return NotFound(new { mensaje = $"No se encontró el Festivo con ID= {Id}" });
            }
            return Ok(festivo);
        }

        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Festivo))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]
        public async Task<ActionResult<Festivo>> Modificar(int Id, [FromBody] Festivo festivo)
        {
            festivo.Id = Id;

            try
            {
                var festivoModificado = await servicio.Modificar(festivo);
                return Ok(festivoModificado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }
    }
}
