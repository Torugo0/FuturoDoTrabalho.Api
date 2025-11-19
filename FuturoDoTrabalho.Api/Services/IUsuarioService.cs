using FuturoDoTrabalho.Api.DTOs;

namespace FuturoDoTrabalho.Api.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto?> GetByIdAsync(long id);
        Task<UsuarioDto> CreateAsync(UsuarioCreateUpdateDto dto);
        Task<UsuarioDto?> UpdateAsync(long id, UsuarioCreateUpdateDto dto);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<TrilhaDto>> GetTrilhasDoUsuarioAsync(long usuarioId);
        Task MatricularAsync(long usuarioId, long trilhaId);
        Task RemoverTrilhaAsync(long usuarioId, long trilhaId);
    }
}