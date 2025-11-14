namespace FuturoDoTrabalho.Api.Domain.Entities
{
    public class Competencia
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }

        public ICollection<TrilhaCompetencia> TrilhaCompetencias { get; set; } = new List<TrilhaCompetencia>();
    }
}