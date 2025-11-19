using FuturoDoTrabalho.Api.Domain.Entities;

namespace FuturoDoTrabalho.Api.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(long id);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<Usuario> AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Usuario usuario);
        Task<IEnumerable<Trilha>> GetTrilhasByUsuarioIdAsync(long usuarioId);
    }
}