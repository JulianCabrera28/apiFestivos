using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiFestivos.dominio
{
    [Table("Festivo")]
    public class Festivo
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nombre")]
        public required string Nombre { get; set; }

        [Column("Dia")]
        public int Dia { get; set; }

        [Column("Mes")]
        public int Mes { get; set; }

        [Column("DiasPascua")]
        public int DiasPascua { get; set; }

        [Column("IdTipo")]
        public int IdTipo { get; set; }

        [Column("IdPais")]
        public int IdPais { get; set; }

        // relaciones
        public Tipo? Tipo { get; set; }
        public Pais? Pais { get; set; }
    }
}