using apiFestivos.aplicacion.servicios;
using apiFestivos.core.servicios;
using apiFestivos.dominio.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace apiFestivos.presentacion.Controllers
{
    [Route("api/calendario")]
    [ApiController]
    public class CalendarioController : Controller
    {
        private readonly IFestivoServicio festivoServicio;
        private readonly IPaisServicio paisServicio;

        public CalendarioController(IFestivoServicio festivoServicio,
                                     IPaisServicio paisServicio)
        {
            this.festivoServicio = festivoServicio;
            this.paisServicio = paisServicio;
        }

        [HttpGet("verificar/{IdPais:int}/{Anio:int}/{Mes:int}/{Dia:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        public async Task<ActionResult> Verificar(int IdPais, int Anio, int Mes, int Dia)
        {
            if (!EsFechaValida(Anio, Mes, Dia))
            {
                return Content("Fecha no válida", "text/plain");
            }

            var pais = await paisServicio.Obtener(IdPais);
            if (pais == null)
            {
                return NotFound(new { mensaje = $"No se encontró el Pais con ID= {IdPais}" });
            }

            try
            {
                bool esFestivo = await festivoServicio.EsFestivo(IdPais, Anio, Mes, Dia);

                return Content($"{IdPais} {(esFestivo ? "Es Festivo" : "No es festivo")}", "text/plain");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpGet("festivos/{IdPais:int}/{Anio:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FestivoDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        public async Task<ActionResult<IEnumerable<FestivoDto>>> ObtenerFestivos(int IdPais, int Anio)
        {
            if (!EsAnioValido(Anio))
            {
                return Content("Fecha no válida", "text/plain");
            }

            var pais = await paisServicio.Obtener(IdPais);
            if (pais == null)
            {
                return NotFound(new { mensaje = $"No se encontró el Pais con ID= {IdPais}" });
            }

            try
            {
                var festivos = await festivoServicio.ObtenerFestivosPorAnio(IdPais, Anio);

                var proyectados = new List<(DateTime Fecha, FestivoDto Dto)>();
                foreach (var festivo in festivos)
                {
                    var fecha = ServicioFechas.ResolverFecha(festivo, Anio);
                    if (!fecha.HasValue)
                    {
                        continue;
                    }

                    proyectados.Add((fecha.Value, new FestivoDto
                    {
                        Festivo = festivo.Nombre,
                        Fecha = DateOnly.FromDateTime(fecha.Value)
                    }));
                }

                return Ok(proyectados.OrderBy(item => item.Fecha).Select(item => item.Dto));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        private static bool EsAnioValido(int Anio) => Anio >= 1 && Anio <= 9998;

        private static bool EsFechaValida(int Anio, int Mes, int Dia)
        {
            return EsAnioValido(Anio)
                && Mes >= 1 && Mes <= 12
                && Dia >= 1 && Dia <= DateTime.DaysInMonth(Anio, Mes);
        }
    }
}
