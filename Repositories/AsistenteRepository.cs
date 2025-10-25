using EventHub.Tec.Avanzadas.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Tec.Avanzadas.Repositories
{
    public class AsistenteRepository : IAsistenteRepository
    {
        private readonly EventHubDbContext _context;

        public AsistenteRepository(EventHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asistente>> GetAllAsync()
        {
            return await _context.Asistentes.Include(a => a.Evento).ToListAsync();
        }

        public async Task<Asistente?> GetByIdAsync(int id)
        {
            return await _context.Asistentes.Include(a => a.Evento)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Asistente> AddAsync(Asistente asistente)
        {
            _context.Asistentes.Add(asistente);
            await _context.SaveChangesAsync();
            return asistente; // <- devuelve el objeto creado
        }

        public async Task UpdateAsync(Asistente asistente)
        {
            _context.Asistentes.Update(asistente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var asistente = await _context.Asistentes.FindAsync(id);
            if (asistente != null)
            {
                _context.Asistentes.Remove(asistente);
                await _context.SaveChangesAsync();
            }
        }

        // --- Nuevo método paginado ---
        public async Task<(IEnumerable<Asistente>, int)> GetAsistentesPaginadoAsync(int limit, int offset)
        {
            var query = _context.Asistentes.Include(a => a.Evento).AsQueryable();

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(a => a.Nombre) // o el criterio que quieras
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }
    }
}

