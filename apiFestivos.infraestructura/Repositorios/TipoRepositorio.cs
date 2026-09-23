using apiFestivos.core.repositorios;
using apiFestivos.dominio;
using apiFestivos.infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace apiFestivos.infraestructura.Repositorios
{
    public class TipoRepositorio : ITipoRepositorio
    {
        private readonly FestivosContext contexto;

        public TipoRepositorio(FestivosContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Tipo> Agregar(Tipo Tipo)
        {
            contexto.Tipos.Add(Tipo);
            await contexto.SaveChangesAsync();
            return Tipo;
        }

        public async Task<IEnumerable<Tipo>> Buscar(int IndiceDato, string Texto)
        {
            return await contexto.Tipos
                .Where(tipo => IndiceDato == 1 && tipo.Descripcion.Contains(Texto))
                .OrderBy(tipo => tipo.Descripcion)
                .ToArrayAsync();
        }

        public async Task<bool> Eliminar(int Id)
        {
            var tipo = await contexto.Tipos.FindAsync(Id);
            if (tipo == null) return false;

            contexto.Tipos.Remove(tipo);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Tipo> Modificar(Tipo Tipo)
        {
            contexto.Tipos.Update(Tipo);
            await contexto.SaveChangesAsync();
            return Tipo;
        }

        public async Task<Tipo> Obtener(int Id)
        {
            return await contexto.Tipos.FindAsync(Id);
        }

        public async Task<IEnumerable<Tipo>> ObtenerTodos()
        {
            return await contexto.Tipos
                .OrderBy(tipo => tipo.Descripcion)
                .ToArrayAsync();
        }
    }
}