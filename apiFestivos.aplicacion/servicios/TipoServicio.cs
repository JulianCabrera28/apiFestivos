using apiFestivos.core.repositorios;
using apiFestivos.core.servicios;
using apiFestivos.dominio;

namespace apiFestivos.aplicacion.servicios
{
    public class TipoServicio : ITipoServicio
    {
        private readonly ITipoRepositorio repositorio;

        private readonly IFestivoRepositorio festivoRepositorio;

        public TipoServicio(ITipoRepositorio repositorio,
                            IFestivoRepositorio festivoRepositorio)
        {
            this.repositorio = repositorio;
            this.festivoRepositorio = festivoRepositorio;
        }

        public async Task<Tipo> Agregar(Tipo Tipo)
        {
            await ValidarDescripcionUnica(Tipo.Descripcion);

            try
            {
                return await repositorio.Agregar(Tipo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"No se pudo registrar el Tipo {Tipo.Descripcion}. {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Tipo>> Buscar(int IndiceDato, string Texto)
        {
            return await repositorio.Buscar(IndiceDato, Texto);
        }

        public async Task<bool> Eliminar(int Id)
        {
            var tipo = await repositorio.Obtener(Id);
            if (tipo == null)
            {
                return false;
            }

            var todos = await festivoRepositorio.ObtenerTodos();
            if (todos.Any(festivo => festivo.IdTipo == Id))
            {
                throw new InvalidOperationException(
                    $"No se puede eliminar el Tipo con ID= {Id} porque tiene Festivos asociados.");
            }

            return await repositorio.Eliminar(Id);
        }

        public async Task<Tipo> Modificar(Tipo Tipo)
        {
            try
            {
                return await repositorio.Modificar(Tipo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"No se pudo modificar el Tipo {Tipo.Descripcion}. {ex.Message}", ex);
            }
        }

        public async Task<Tipo> Obtener(int Id)
        {
            return await repositorio.Obtener(Id);
        }

        public async Task<IEnumerable<Tipo>> ObtenerTodos()
        {
            return await repositorio.ObtenerTodos();
        }

        private async Task ValidarDescripcionUnica(string Descripcion)
        {
            var tipos = await repositorio.ObtenerTodos();

            if (tipos.Any(tipo => string.Equals(tipo.Descripcion, Descripcion, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Ya existe un Tipo con Descripcion= {Descripcion}.");
            }
        }
    }
}
