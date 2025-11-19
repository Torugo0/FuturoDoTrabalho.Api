using FuturoDoTrabalho.Api.Domain.Entities;

namespace FuturoDoTrabalho.Api.Repositories
{
    public interface IMatriculaRepository
    {
        Task<bool> JaExisteAsync(long usuarioId, long trilhaId);
        Task AddAsync(Matricula matricula);

        Task<Matricula?> GetAsync(long usuarioId, long trilhaId);
        Task RemoveAsync(Matricula matricula);
    }
}
