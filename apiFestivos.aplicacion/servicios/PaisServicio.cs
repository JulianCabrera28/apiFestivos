using apiFestivos.core.repositorios;
using apiFestivos.core.servicios;
using apiFestivos.dominio;

namespace apiFestivos.aplicacion.servicios
{
    public class PaisServicio : IPaisServicio
    {
        private readonly IPaisRepositorio repositorio;

        private readonly IFestivoRepositorio festivoRepositorio;

        public PaisServicio(IPaisRepositorio repositorio,
                            IFestivoRepositorio festivoRepositorio)
        {
            this.repositorio = repositorio;
            this.festivoRepositorio = festivoRepositorio;
        }

        public async Task<Pais> Agregar(Pais Pais)
        {
            await ValidarNombreUnico(Pais.Nombre);

            try
            {
                return await repositorio.Agregar(Pais);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"No se pudo registrar el Pais {Pais.Nombre}. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Pais>> Buscar(int IndiceDato, string Texto)
        {
            return await repositorio.Buscar(IndiceDato, Texto);
        }

        public async Task<bool> Eliminar(int Id)
        {
            var pais = await repositorio.Obtener(Id);
            if (pais == null)
            {
                return false;
            }

            var festivos = await festivoRepositorio.ObtenerPorPais(Id);
            if (festivos.Any())
            {
                throw new InvalidOperationException(
                    $"No se puede eliminar el Pais con ID= {Id} porque tiene Festivos asociados.");
            }

            return await repositorio.Eliminar(Id);
        }

        public async Task<Pais> Modificar(Pais Pais)
        {
            try
            {
                return await repositorio.Modificar(Pais);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"No se pudo modificar el Pais {Pais.Nombre}. {ex.Message}", ex);
            }
        }

        public async Task<Pais> Obtener(int Id)
        {
            return await repositorio.Obtener(Id);
        }

        public async Task<IEnumerable<Pais>> ObtenerTodos()
        {
            return await repositorio.ObtenerTodos();
        }

        private async Task ValidarNombreUnico(string Nombre)
        {
            var paises = await repositorio.ObtenerTodos();

            if (paises.Any(pais => string.Equals(pais.Nombre, Nombre, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Ya existe un Pais con Nombre= {Nombre}.");
            }
        }
    }
}
