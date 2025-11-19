using FuturoDoTrabalho.Api.Domain.Entities;

namespace FuturoDoTrabalho.Api.Repositories
{
    public interface ITrilhaRepository
    {
        Task<IEnumerable<Trilha>> GetAllAsync();
        Task<Trilha?> GetByIdAsync(long id);
        Task<Trilha> AddAsync(Trilha trilha);
        Task UpdateAsync(Trilha trilha);
        Task DeleteAsync(Trilha trilha);
        Task<IEnumerable<Usuario>> GetUsuariosByTrilhaIdAsync(long trilhaId);
    }
}