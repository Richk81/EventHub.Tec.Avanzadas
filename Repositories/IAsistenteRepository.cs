using EventHub.Tec.Avanzadas.Models;

namespace EventHub.Tec.Avanzadas.Repositories
{
    public interface IAsistenteRepository
    {
        Task<IEnumerable<Asistente>> GetAllAsync();
        Task<Asistente?> GetByIdAsync(int id);
        Task<Asistente> AddAsync(Asistente asistente); // <- devuelve Asistente
        Task UpdateAsync(Asistente asistente);
        Task DeleteAsync(int id);

        // Nuevo método para paginación
        Task<(IEnumerable<Asistente>, int)> GetAsistentesPaginadoAsync(int limit, int offset);
    }
}
