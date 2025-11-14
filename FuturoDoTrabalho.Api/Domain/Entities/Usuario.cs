namespace FuturoDoTrabalho.Api.Domain.Entities
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? AreaAtuacao { get; set; }
        public string? NivelCarreira { get; set; } // Junior, Pleno, etc.
        public DateTime DataCadastro { get; set; }
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
