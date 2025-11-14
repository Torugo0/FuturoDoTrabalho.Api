using System.ComponentModel.DataAnnotations;

namespace FuturoDoTrabalho.Api.DTOs
{
    public class TrilhaCreateUpdateDto
    {
        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

        [Required]
        [RegularExpression("INICIANTE|INTERMEDIARIO|AVANCADO",
            ErrorMessage = "Nivel deve ser INICIANTE, INTERMEDIARIO ou AVANCADO")]
        public string Nivel { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "CargaHoraria deve ser positiva")]
        public int CargaHoraria { get; set; }

        [StringLength(100)]
        public string? FocoPrincipal { get; set; }
    }
}