using System;
using Microsoft.EntityFrameworkCore;
using apiFestivos.dominio;

namespace apiFestivos.infraestructura.Persistencia
{
    public class FestivosContext : DbContext
    {
        public FestivosContext(DbContextOptions<FestivosContext> opciones) : base(opciones)
        {
        }

        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Festivo> Festivos { get; set; }

        protected override void OnModelCreating(ModelBuilder constructor)
        {
            constructor.Entity<Tipo>(entidadTipo =>
            {
                entidadTipo.HasKey(e => e.Id);
                entidadTipo.HasIndex(e => e.Descripcion).IsUnique();
            });

            constructor.Entity<Pais>(entidadPais =>
            {
                entidadPais.HasKey(e => e.Id);
                entidadPais.HasIndex(e => e.Nombre).IsUnique();
            });

            constructor.Entity<Festivo>(entidadFestivo =>
            {
                entidadFestivo.HasKey(e => e.Id);
            });

            constructor.Entity<Festivo>()
                .HasOne(e => e.Tipo)
                .WithMany()
                .HasForeignKey(e => e.IdTipo);

            constructor.Entity<Festivo>()
                .HasOne(e => e.Pais)
                .WithMany()
                .HasForeignKey(e => e.IdPais);
        }
    }
}
