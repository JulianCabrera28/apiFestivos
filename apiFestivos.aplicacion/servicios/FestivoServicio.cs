using apiFestivos.core.repositorios;
using apiFestivos.core.servicios;
using apiFestivos.dominio;

namespace apiFestivos.aplicacion.servicios
{
    public class FestivoServicio : IFestivoServicio
    {
        private readonly IFestivoRepositorio repositorio;

        public FestivoServicio(IFestivoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<Festivo> Agregar(Festivo Festivo)
        {
            try
            {
                return await repositorio.Agregar(Festivo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"No se pudo registrar el Festivo {Festivo.Nombre}. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Festivo>> Buscar(int IndiceDato, string Texto)
        {
            return await repositorio.Buscar(IndiceDato, Texto);
        }

        public async Task<bool> Eliminar(int Id)
        {
            return await repositorio.Eliminar(Id);
        }

        public async Task<bool> EsFestivo(int IdPais, int Anio, int Mes, int Dia)
        {
            var festivos = await repositorio.ObtenerPorPais(IdPais);

            foreach (var festivo in festivos)
            {
                var fecha = ServicioFechas.ResolverFecha(festivo, Anio);
                if (!fecha.HasValue)
                {
                    continue;
                }

                if (fecha.Value.Year == Anio
                    && fecha.Value.Month == Mes
                    && fecha.Value.Day == Dia)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<Festivo> Modificar(Festivo Festivo)
        {
            try
            {
                return await repositorio.Modificar(Festivo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"No se pudo modificar el Festivo {Festivo.Nombre}. {ex.Message}", ex);
            }
        }

        public async Task<Festivo> Obtener(int Id)
        {
            return await repositorio.Obtener(Id);
        }

        public async Task<IEnumerable<Festivo>> ObtenerFestivosPorAnio(int IdPais, int Anio)
        {
            return await repositorio.ObtenerPorPais(IdPais);
        }

        public async Task<IEnumerable<Festivo>> ObtenerPorPais(int IdPais)
        {
            return await repositorio.ObtenerPorPais(IdPais);
        }

        public async Task<IEnumerable<Festivo>> ObtenerTodos()
        {
            return await repositorio.ObtenerTodos();
        }
    }
}
