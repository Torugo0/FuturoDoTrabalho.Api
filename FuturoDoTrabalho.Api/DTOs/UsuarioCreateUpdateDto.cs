using System.ComponentModel.DataAnnotations;

namespace FuturoDoTrabalho.Api.DTOs
{
    public class UsuarioCreateUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [StringLength(100)]
        public string? AreaAtuacao { get; set; }

        [StringLength(50)]
        public string? NivelCarreira { get; set; }
    }
}