namespace FuturoDoTrabalho.Api.Domain.Entities
{
    public class Trilha
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public string Nivel { get; set; } = null!; // INICIANTE, INTERMEDIARIO, AVANCADO
        public int CargaHoraria { get; set; }
        public string? FocoPrincipal { get; set; } // IA, Dados, Soft Skills, etc.

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
        public ICollection<TrilhaCompetencia> TrilhaCompetencias { get; set; } = new List<TrilhaCompetencia>();
    }
}
