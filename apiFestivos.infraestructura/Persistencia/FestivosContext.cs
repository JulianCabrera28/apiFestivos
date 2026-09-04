using System;
using Microsoft.EntityFrameworkCore;
using apiFestivos.dominio;

namespace apiFestivos.infraestructura.Persistencia
{
    public class FestivosContext : DbContext
    {
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Festivo> Festivos { get; set; }

        protected override void OnModelCreating(ModelBuilder constructor)
        {
            // Tabla TIPO

            constructor.Entity<Tipo>(entidadTipo =>
            {
                entidadTipo.HasKey(e => e.Id); // Clave primaria
                entidadTipo.HasIndex(e => e.Descripcion).IsUnique(); // Indice
            });

            // Tabla PAIS

            constructor.Entity<Pais>(entidadPais =>
            {
                entidadPais.HasKey(e => e.Id); // Clave primaria
                entidadPais.HasIndex(e => e.Nombre).IsUnique(); // Indice
            });

            // Tabla FESTIVO

            constructor.Entity<Festivo>(entidadFestivo =>
            {
                entidadFestivo.HasKey(e => e.Id); // Clave primaria
            });

            constructor.Entity<Festivo>()
                .HasOne(e => e.Tipo)
                .WithMany()
                .HasForeignKey(e => e.IdTipo); // Clave foránea

            constructor.Entity<Festivo>()
                .HasOne(e => e.Pais)
                .WithMany()
                .HasForeignKey(e => e.IdPais); // Clave foránea
        }
    }
}