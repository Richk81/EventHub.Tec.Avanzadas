using EventHub.Tec.Avanzadas.DTOs;
using EventHub.Tec.Avanzadas.Models;
using EventHub.Tec.Avanzadas.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventHub.Tec.Avanzadas.Services
{
    public class AsistenteService : IAsistenteService
    {
        private readonly IAsistenteRepository _repo;

        public AsistenteService(IAsistenteRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AsistenteResponse>> ListarAsync()
        {
            var lista = await _repo.GetAllAsync();
            return lista.Select(a => new AsistenteResponse
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Email = a.Email,
                Telefono = a.Telefono,
                EventoId = a.EventoId,
                RegistradoEn = a.RegistradoEn
            });
        }

        public async Task<AsistenteResponse?> ObtenerAsync(int id)
        {
            var a = await _repo.GetByIdAsync(id);
            if (a == null) return null;

            return new AsistenteResponse
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Email = a.Email,
                Telefono = a.Telefono,
                EventoId = a.EventoId,
                RegistradoEn = a.RegistradoEn
            };
        }

        public async Task<AsistenteResponse> CrearAsync(AsistenteRequest request)
        {
            var nuevo = new Asistente
            {
                Nombre = request.Nombre,
                Email = request.Email,
                Telefono = request.Telefono,
                EventoId = request.EventoId,
                RegistradoEn = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(nuevo); // ahora devuelve el objeto creado

            return new AsistenteResponse
            {
                Id = created.Id,
                Nombre = created.Nombre,
                Email = created.Email,
                Telefono = created.Telefono,
                EventoId = created.EventoId,
                RegistradoEn = created.RegistradoEn
            };
        }


        public async Task EditarAsync(int id, AsistenteRequest request)
        {
            var existente = await _repo.GetByIdAsync(id);
            if (existente == null) return;

            existente.Nombre = request.Nombre;
            existente.Email = request.Email;
            existente.Telefono = request.Telefono;
            existente.EventoId = request.EventoId;

            await _repo.UpdateAsync(existente);
        }

        public async Task EliminarAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        // --- Nuevo método paginado sin Paginador ---
        public async Task<PaginacionResponse<AsistenteResponse>> ListarPaginadoAsync(int limit, int offset)
        {
            // Llamamos al repositorio que ya maneja paginación
            var (items, total) = await _repo.GetAsistentesPaginadoAsync(limit, offset);

            var data = items.Select(a => new AsistenteResponse
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Email = a.Email,
                Telefono = a.Telefono,
                EventoId = a.EventoId,
                RegistradoEn = a.RegistradoEn
            }).ToList();

            return new PaginacionResponse<AsistenteResponse>
            {
                Meta = new MetaData
                {
                    TotalRegistros = total,
                    PaginaActual = (offset / (limit == 0 ? 1 : limit)) + 1,
                    TamanoPagina = data.Count
                },
                Data = data
            };
        }

    }
}
