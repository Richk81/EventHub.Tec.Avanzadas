using EventHub.Tec.Avanzadas.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Tec.Avanzadas.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly EventHubDbContext _context;

        public ComentarioRepository(EventHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comentario>> GetAllAsync()
        {
            return await _context.Comentarios.Include(c => c.Evento).ToListAsync();
        }

        public async Task<Comentario?> GetByIdAsync(int id)
        {
            return await _context.Comentarios.Include(c => c.Evento)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comentario> AddAsync(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return comentario; // <- Devuelve el objeto con Id asignado
        }

        public async Task UpdateAsync(Comentario comentario)
        {
            _context.Comentarios.Update(comentario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario != null)
            {
                comentario.Eliminado = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<(IEnumerable<Comentario>, int)> GetComentariosPorEventoAsync(int eventoId, int limit, int offset)
        {
            var q = _context.Comentarios.Where(c => c.EventoId == eventoId && !c.Eliminado);
            var total = await q.CountAsync();
            var items = await q.OrderByDescending(c => c.FechaCreacion)
                               .Skip(offset).Take(limit)
                               .ToListAsync();
            return (items, total);
        }
    }
}

