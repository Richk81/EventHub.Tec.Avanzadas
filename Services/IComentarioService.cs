using EventHub.Tec.Avanzadas.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventHub.Tec.Avanzadas.Services
{
    public interface IComentarioService
    {
        // CRUD genérico
        Task<IEnumerable<ComentarioResponse>> ListarAsync(); // Todos los comentarios
        Task<ComentarioResponse?> ObtenerAsync(int id);
        Task<ComentarioResponse> CrearAsync(ComentarioRequest request);
        Task<bool> EditarAsync(int id, ComentarioRequest request);
        Task<bool> EliminarAsync(int id);

        // Por evento con paginación
        Task<PaginacionResponse<ComentarioResponse>> ListarPorEventoAsync(int eventoId, int limit, int offset);
        Task<ComentarioResponse> CrearPorEventoAsync(int eventoId, ComentarioRequest req);
    }
}




