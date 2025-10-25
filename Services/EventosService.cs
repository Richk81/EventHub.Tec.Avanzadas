using System;
using System.Linq;
using System.Threading.Tasks;
using EventHub.Tec.Avanzadas.DTOs;
using EventHub.Tec.Avanzadas.Models;
using EventHub.Tec.Avanzadas.Repositories;

namespace EventHub.Tec.Avanzadas.Services
{
    public class EventosService : IEventosService
    {
        private readonly IEventosRepository _repo;
        public EventosService(IEventosRepository repo) => _repo = repo;

        public async Task<PaginacionResponse<EventoResponse>> ObtenerEventosAsync(string? ubicacion, int limit, int offset)
        {
            var (items, total) = await _repo.GetEventosAsync(ubicacion, limit, offset);
            var data = items.Select(e => new EventoResponse
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Ubicacion = e.Ubicacion,
                Fecha = e.Fecha,
                Descripcion = e.Descripcion
            }).ToList();

            return new PaginacionResponse<EventoResponse>
            {
                Meta = new MetaData { TotalRegistros = total, PaginaActual = (offset / (limit == 0 ? 1 : limit)) + 1, TamanoPagina = limit },
                Data = data
            };
        }

        public async Task<EventoResponse?> ObtenerPorIdAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return null;
            return new EventoResponse
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Ubicacion = e.Ubicacion,
                Fecha = e.Fecha,
                Descripcion = e.Descripcion
            };
        }

        public async Task<EventoResponse> CrearEventoAsync(EventoRequest request)
        {
            var e = new Evento
            {
                Nombre = request.Nombre,
                Ubicacion = request.Ubicacion,
                Fecha = request.Fecha!.Value,
                Descripcion = request.Descripcion,
                CreadoEn = DateTime.UtcNow,
                Eliminado = false
            };
            var created = await _repo.AddAsync(e);
            return new EventoResponse
            {
                Id = created.Id,
                Nombre = created.Nombre,
                Ubicacion = created.Ubicacion,
                Fecha = created.Fecha,
                Descripcion = created.Descripcion
            };
        }

        public async Task<bool> ActualizarEventoAsync(int id, EventoRequest request)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return false;

            if (!string.IsNullOrWhiteSpace(request.Nombre)) e.Nombre = request.Nombre;
            if (!string.IsNullOrWhiteSpace(request.Ubicacion)) e.Ubicacion = request.Ubicacion;
            if (request.Fecha.HasValue) e.Fecha = request.Fecha.Value;
            if (request.Descripcion != null) e.Descripcion = request.Descripcion;

            await _repo.UpdateAsync(e);
            return true;
        }

        public async Task<bool> EliminarEventoAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return false;
            await _repo.DeleteAsync(e);
            return true;
        }

        // Asistentes
        public async Task<PaginacionResponse<AsistenteResponse>> ObtenerAsistentesAsync(int eventoId, int limit, int offset)
        {
            var (items, total) = await _repo.GetAsistentesAsync(eventoId, limit, offset);
            var data = items.Select(a => new AsistenteResponse
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Email = a.Email,
                Telefono = a.Telefono,
                EventoId = a.EventoId
            }).ToList();

            return new PaginacionResponse<AsistenteResponse>
            {
                Meta = new MetaData { TotalRegistros = total, PaginaActual = (offset / (limit == 0 ? 1 : limit)) + 1, TamanoPagina = limit },
                Data = data
            };
        }

        public async Task<AsistenteResponse> CrearAsistenteAsync(int eventoId, AsistenteRequest req)
        {
            var a = new Asistente
            {
                Nombre = req.Nombre,
                Email = req.Email,
                Telefono = req.Telefono,
                EventoId = eventoId,
                RegistradoEn = DateTime.UtcNow
            };
            var created = await _repo.AddAsistenteAsync(a);
            return new AsistenteResponse
            {
                Id = created.Id,
                Nombre = created.Nombre,
                Email = created.Email,
                Telefono = created.Telefono,
                EventoId = created.EventoId
            };
        }

        // Comentarios
        public async Task<PaginacionResponse<ComentarioResponse>> ObtenerComentariosAsync(int eventoId, int limit, int offset)
        {
            var (items, total) = await _repo.GetComentariosAsync(eventoId, limit, offset);
            var data = items.Select(c => new ComentarioResponse
            {
                Id = c.Id,
                Autor = c.Autor,
                Rating = c.Rating,
                Texto = c.Texto,
                FechaCreacion = c.FechaCreacion,
                EventoId = c.EventoId
            }).ToList();

            return new PaginacionResponse<ComentarioResponse>
            {
                Meta = new MetaData { TotalRegistros = total, PaginaActual = (offset / (limit == 0 ? 1 : limit)) + 1, TamanoPagina = limit },
                Data = data
            };
        }

        public async Task<ComentarioResponse> CrearComentarioAsync(int eventoId, ComentarioRequest req)
        {
            var c = new Comentario
            {
                Autor = req.Autor,
                Rating = req.Rating,
                Texto = req.Texto,
                EventoId = eventoId,
                FechaCreacion = DateTime.UtcNow,
                Eliminado = false
            };
            var created = await _repo.AddComentarioAsync(c);
            return new ComentarioResponse
            {
                Id = created.Id,
                Autor = created.Autor,
                Rating = created.Rating,
                Texto = created.Texto,
                FechaCreacion = created.FechaCreacion,
                EventoId = created.EventoId
            };
        }
    }
}

