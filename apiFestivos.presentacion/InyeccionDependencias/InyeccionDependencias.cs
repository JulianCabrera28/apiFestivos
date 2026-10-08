using apiFestivos.aplicacion.servicios;
using apiFestivos.core.repositorios;
using apiFestivos.core.servicios;
using apiFestivos.infraestructura.Persistencia;
using apiFestivos.infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace apiFestivos.presentacion.InyeccionDependencias
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AgregarDependencias(this IServiceCollection servicios,
                                                              IConfiguration configuracion)
        {
            servicios.AddDbContext<FestivosContext>(opciones =>
            {
                opciones.UseSqlServer(configuracion.GetConnectionString("Festivos"));
            });

            servicios.AddTransient<IFestivoRepositorio, FestivoRepositorio>();
            servicios.AddTransient<IPaisRepositorio, PaisRepositorio>();
            servicios.AddTransient<ITipoRepositorio, TipoRepositorio>();

            servicios.AddTransient<IFestivoServicio, FestivoServicio>();
            servicios.AddTransient<IPaisServicio, PaisServicio>();
            servicios.AddTransient<ITipoServicio, TipoServicio>();

            return servicios;
        }
    }
}
