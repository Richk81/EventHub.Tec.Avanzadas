using EventHub.Tec.Avanzadas.Models;

namespace EventHub.Tec.Avanzadas.Repositories
{
    public interface IComentarioRepository
    {
        Task<IEnumerable<Comentario>> GetAllAsync();
        Task<Comentario?> GetByIdAsync(int id);
        Task<Comentario> AddAsync(Comentario comentario); // <- Cambiado a Task<Comentario>
        Task UpdateAsync(Comentario comentario);
        Task DeleteAsync(int id);
        Task<(IEnumerable<Comentario>, int)> GetComentariosPorEventoAsync(int eventoId, int limit, int offset);

    }
}

