namespace FuturoDoTrabalho.Api.DTOs
{
    public class TrilhaDto
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public string Nivel { get; set; } = null!;
        public int CargaHoraria { get; set; }
        public string? FocoPrincipal { get; set; }
    }
}