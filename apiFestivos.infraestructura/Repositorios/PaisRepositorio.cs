using apiFestivos.core.repositorios;
using apiFestivos.dominio;
using apiFestivos.infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace apiFestivos.infraestructura.Repositorios
{
    public class PaisRepositorio : IPaisRepositorio
    {
        private readonly FestivosContext contexto;

        public PaisRepositorio(FestivosContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Pais> Agregar(Pais Pais)
        {
            contexto.Paises.Add(Pais);
            await contexto.SaveChangesAsync();
            return Pais;
        }

        public async Task<IEnumerable<Pais>> Buscar(int IndiceDato, string Texto)
        {
            return await contexto.Paises
                .Where(pais => IndiceDato == 1 && pais.Nombre.Contains(Texto))
                .OrderBy(pais => pais.Nombre)
                .ToArrayAsync();
        }

        public async Task<bool> Eliminar(int Id)
        {
            var pais = await contexto.Paises.FindAsync(Id);
            if (pais == null) return false;

            contexto.Paises.Remove(pais);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Pais> Modificar(Pais Pais)
        {
            contexto.Paises.Update(Pais);
            await contexto.SaveChangesAsync();
            return Pais;
        }

        public async Task<Pais> Obtener(int Id)
        {
            return await contexto.Paises.FindAsync(Id);
        }

        public async Task<IEnumerable<Pais>> ObtenerTodos()
        {
            return await contexto.Paises
                .OrderBy(pais => pais.Nombre)
                .ToArrayAsync();
        }
    }
}