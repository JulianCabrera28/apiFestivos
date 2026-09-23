using apiFestivos.core.repositorios;
using apiFestivos.dominio;
using apiFestivos.infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace apiFestivos.infraestructura.Repositorios
{
    public class FestivoRepositorio : IFestivoRepositorio
    {
        private readonly FestivosContext contexto;

        public FestivoRepositorio(FestivosContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Festivo> Agregar(Festivo Festivo)
        {
            contexto.Festivos.Add(Festivo);
            await contexto.SaveChangesAsync();
            return Festivo;
        }

        public async Task<IEnumerable<Festivo>> Buscar(int IndiceDato, string Texto)
        {
            return await contexto.Festivos
                .Where(festivo => IndiceDato == 1 && festivo.Nombre.Contains(Texto))
                .Include(festivo => festivo.Tipo)
                .Include(festivo => festivo.Pais)
                .OrderBy(festivo => festivo.Nombre)
                .ToArrayAsync();
        }

        public async Task<bool> Eliminar(int Id)
        {
            var festivo = await contexto.Festivos.FindAsync(Id);
            if (festivo == null) return false;

            contexto.Festivos.Remove(festivo);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Festivo> Modificar(Festivo Festivo)
        {
            contexto.Festivos.Update(Festivo);
            await contexto.SaveChangesAsync();
            return Festivo;
        }

        public async Task<Festivo> Obtener(int Id)
        {
            return await contexto.Festivos
                .Include(festivo => festivo.Tipo)
                .Include(festivo => festivo.Pais)
                .FirstOrDefaultAsync(festivo => festivo.Id == Id);
        }

        public async Task<IEnumerable<Festivo>> ObtenerPorPais(int IdPais)
        {
            return await contexto.Festivos
                .Where(festivo => festivo.IdPais == IdPais)
                .Include(festivo => festivo.Tipo)
                .Include(festivo => festivo.Pais)
                .OrderBy(festivo => festivo.Nombre)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Festivo>> ObtenerTodos()
        {
            return await contexto.Festivos
                .Include(festivo => festivo.Tipo)
                .Include(festivo => festivo.Pais)
                .OrderBy(festivo => festivo.Nombre)
                .ToArrayAsync();
        }
    }
}