using FuturoDoTrabalho.Api.Data;
using FuturoDoTrabalho.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuturoDoTrabalho.Api.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly ApplicationDbContext _context;

        public MatriculaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> JaExisteAsync(long usuarioId, long trilhaId)
        {
            return await _context.Matriculas
                .AnyAsync(m => m.UsuarioId == usuarioId && m.TrilhaId == trilhaId);
        }

        public async Task AddAsync(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
        }

        public async Task<Matricula?> GetAsync(long usuarioId, long trilhaId)
        {
            return await _context.Matriculas
                .FirstOrDefaultAsync(m => m.UsuarioId == usuarioId && m.TrilhaId == trilhaId);
        }

        public async Task RemoveAsync(Matricula matricula)
        {
            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
        }
    }
}
