namespace FuturoDoTrabalho.Api.DTOs
{
    public class UsuarioDto
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? AreaAtuacao { get; set; }
        public string? NivelCarreira { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}