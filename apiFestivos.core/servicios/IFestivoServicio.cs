using System;
using System.Collections.Generic;
using System.Text;
using apiFestivos.dominio;

namespace apiFestivos.core.servicios
{
    public interface IFestivoServicio
    {
        Task<IEnumerable<Festivo>> ObtenerTodos();

        Task<IEnumerable<Festivo>> ObtenerPorPais(int IdPais);

        Task<Festivo> Obtener(int Id);

        Task<IEnumerable<Festivo>> Buscar(int IndiceDato, string Texto);

        Task<Festivo> Agregar(Festivo Festivo);

        Task<Festivo> Modificar(Festivo Festivo);

        Task<bool> Eliminar(int Id);

        // Logica de negocio especifica
        Task<bool> EsFestivo(int IdPais, int Anio, int Mes, int Dia);

        Task<IEnumerable<Festivo>> ObtenerFestivosPorAnio(int IdPais, int Anio);
    }
}