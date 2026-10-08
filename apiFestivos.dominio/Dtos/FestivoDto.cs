namespace apiFestivos.dominio.Dtos
{
    public class FestivoDto
    {
        public required string Festivo { get; set; }

        public required DateOnly Fecha { get; set; }
    }
}
