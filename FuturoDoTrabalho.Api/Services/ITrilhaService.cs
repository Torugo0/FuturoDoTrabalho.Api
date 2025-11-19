using FuturoDoTrabalho.Api.DTOs;

namespace FuturoDoTrabalho.Api.Services
{
    public interface ITrilhaService
    {
        Task<IEnumerable<TrilhaDto>> GetAllAsync();
        Task<TrilhaDto?> GetByIdAsync(long id);
        Task<TrilhaDto> CreateAsync(TrilhaCreateUpdateDto dto);
        Task<TrilhaDto?> UpdateAsync(long id, TrilhaCreateUpdateDto dto);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<UsuarioDto>> GetUsuariosDaTrilhaAsync(long trilhaId);
    }
}