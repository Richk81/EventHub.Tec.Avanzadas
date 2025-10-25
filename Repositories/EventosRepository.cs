using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHub.Tec.Avanzadas.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Tec.Avanzadas.Repositories
{
    public class EventosRepository : IEventosRepository
    {
        private readonly EventHubDbContext _db;
        public EventosRepository(EventHubDbContext db) => _db = db;

        public async Task<(IEnumerable<Evento> Items, int Total)> GetEventosAsync(string? ubicacion, int limit, int offset)
        {
            var q = _db.Eventos.Where(e => !e.Eliminado).AsQueryable();
            if (!string.IsNullOrWhiteSpace(ubicacion))
                q = q.Where(e => e.Ubicacion != null && e.Ubicacion.Contains(ubicacion));

            var total = await q.CountAsync();
            var items = await q.OrderBy(e => e.Nombre).Skip(offset).Take(limit).ToListAsync();
            return (items, total);
        }

        public async Task<Evento?> GetByIdAsync(int id) =>
            await _db.Eventos.FirstOrDefaultAsync(e => e.Id == id && !e.Eliminado);

        public async Task<Evento> AddAsync(Evento evento)
        {
            _db.Eventos.Add(evento);
            await _db.SaveChangesAsync();
            return evento; // <- devuelve Evento creado
        }

        public async Task UpdateAsync(Evento evento)
        {
            _db.Eventos.Update(evento);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Evento evento)
        {
            // soft delete
            evento.Eliminado = true;
            _db.Eventos.Update(evento);
            await _db.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Asistente> Items, int Total)> GetAsistentesAsync(int eventoId, int limit, int offset)
        {
            var q = _db.Asistentes.Where(a => a.EventoId == eventoId).AsQueryable();
            var total = await q.CountAsync();
            var items = await q.OrderBy(a => a.Nombre).Skip(offset).Take(limit).ToListAsync();
            return (items, total);
        }

        public async Task<Asistente> AddAsistenteAsync(Asistente a)
        {
            _db.Asistentes.Add(a);
            await _db.SaveChangesAsync();
            return a; // <- devuelve Asistente creado
        }

        public async Task<(IEnumerable<Comentario> Items, int Total)> GetComentariosAsync(int eventoId, int limit, int offset)
        {
            var q = _db.Comentarios.Where(c => c.EventoId == eventoId && !c.Eliminado).AsQueryable();
            var total = await q.CountAsync();
            var items = await q.OrderByDescending(c => c.FechaCreacion).Skip(offset).Take(limit).ToListAsync();
            return (items, total);
        }

        public async Task<Comentario> AddComentarioAsync(Comentario c)
        {
            _db.Comentarios.Add(c);
            await _db.SaveChangesAsync();
            return c; // <- devuelve Comentario creado
        }
    }
}


