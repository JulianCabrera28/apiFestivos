using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiFestivos.dominio
{
    [Table("Tipo")]
    public class Tipo
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Tipo")]
        public required string Descripcion { get; set; }
    }
}