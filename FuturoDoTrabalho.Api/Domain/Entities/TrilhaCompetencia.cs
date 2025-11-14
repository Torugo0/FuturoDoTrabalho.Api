namespace FuturoDoTrabalho.Api.Domain.Entities
{
    public class TrilhaCompetencia
    {
        public long TrilhaId { get; set; }
        public Trilha Trilha { get; set; } = null!;
        public long CompetenciaId { get; set; }
        public Competencia Competencia { get; set; } = null!;
    }
}