using FuturoDoTrabalho.Api.Data;
using FuturoDoTrabalho.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuturoDoTrabalho.Api.Repositories
{
    public class TrilhaRepository : ITrilhaRepository
    {
        private readonly ApplicationDbContext _context;

        public TrilhaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trilha>> GetAllAsync()
        {
            return await _context.Trilhas.AsNoTracking().ToListAsync();
        }

        public async Task<Trilha?> GetByIdAsync(long id)
        {
            return await _context.Trilhas.FindAsync(id);
        }

        public async Task<Trilha> AddAsync(Trilha trilha)
        {
            _context.Trilhas.Add(trilha);
            await _context.SaveChangesAsync();
            return trilha;
        }

        public async Task UpdateAsync(Trilha trilha)
        {
            _context.Trilhas.Update(trilha);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Trilha trilha)
        {
            _context.Trilhas.Remove(trilha);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Usuario>> GetUsuariosByTrilhaIdAsync(long trilhaId)
        {
            return await _context.Matriculas
                .Where(m => m.TrilhaId == trilhaId)
                .Include(m => m.Usuario)
                .Select(m => m.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}