using System;
using System.Collections.Generic;
using System.Text;
using apiFestivos.dominio;

namespace apiFestivos.core.servicios
{
    public interface ITipoServicio
    {
        Task<IEnumerable<Tipo>> ObtenerTodos();

        Task<Tipo> Obtener(int Id);

        Task<IEnumerable<Tipo>> Buscar(int IndiceDato, string Texto);

        Task<Tipo> Agregar(Tipo Tipo);

        Task<Tipo> Modificar(Tipo Tipo);

        Task<bool> Eliminar(int Id);
    }
}