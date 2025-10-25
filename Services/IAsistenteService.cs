using EventHub.Tec.Avanzadas.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventHub.Tec.Avanzadas.Services
{
    public interface IAsistenteService
    {
        Task<IEnumerable<AsistenteResponse>> ListarAsync();
        Task<AsistenteResponse?> ObtenerAsync(int id);
        Task<AsistenteResponse> CrearAsync(AsistenteRequest request); // <- cambia Task a Task<Asistent
        Task EditarAsync(int id, AsistenteRequest request);
        Task EliminarAsync(int id);

        // <-- Nuevo método para paginación
        Task<PaginacionResponse<AsistenteResponse>> ListarPaginadoAsync(int limit, int offset);
    }
}



