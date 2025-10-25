using System.Threading.Tasks;
using EventHub.Tec.Avanzadas.DTOs;

namespace EventHub.Tec.Avanzadas.Services
{
    public interface IEventosService
    {
        Task<PaginacionResponse<EventoResponse>> ObtenerEventosAsync(string? ubicacion, int limit, int offset);
        Task<EventoResponse?> ObtenerPorIdAsync(int id);
        Task<EventoResponse> CrearEventoAsync(EventoRequest request);
        Task<bool> ActualizarEventoAsync(int id, EventoRequest request);
        Task<bool> EliminarEventoAsync(int id);

        Task<PaginacionResponse<AsistenteResponse>> ObtenerAsistentesAsync(int eventoId, int limit, int offset);
        Task<AsistenteResponse> CrearAsistenteAsync(int eventoId, AsistenteRequest req);

        Task<PaginacionResponse<ComentarioResponse>> ObtenerComentariosAsync(int eventoId, int limit, int offset);
        Task<ComentarioResponse> CrearComentarioAsync(int eventoId, ComentarioRequest req);
    }
}

