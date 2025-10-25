using System.Collections.Generic;
using System.Threading.Tasks;
using EventHub.Tec.Avanzadas.Models;

namespace EventHub.Tec.Avanzadas.Repositories
{
    public interface IEventosRepository
    {
        Task<(IEnumerable<Evento> Items, int Total)> GetEventosAsync(string? ubicacion, int limit, int offset);
        Task<Evento?> GetByIdAsync(int id);
        Task<Evento> AddAsync(Evento evento); // <- devuelve Evento
        Task UpdateAsync(Evento evento);
        Task DeleteAsync(Evento evento);

        Task<(IEnumerable<Asistente> Items, int Total)> GetAsistentesAsync(int eventoId, int limit, int offset);
        Task<Asistente> AddAsistenteAsync(Asistente a); // <- devuelve Asistente

        Task<(IEnumerable<Comentario> Items, int Total)> GetComentariosAsync(int eventoId, int limit, int offset);
        Task<Comentario> AddComentarioAsync(Comentario c); // <- devuelve Comentario
    }
}

