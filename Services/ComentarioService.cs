using EventHub.Tec.Avanzadas.DTOs;
using EventHub.Tec.Avanzadas.Models;
using EventHub.Tec.Avanzadas.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EventHub.Tec.Avanzadas.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _repo;

        public ComentarioService(IComentarioRepository repo)
        {
            _repo = repo;
        }

        // --- CRUD global ---
        public async Task<IEnumerable<ComentarioResponse>> ListarAsync()
        {
            var lista = await _repo.GetAllAsync();
            return lista.Select(c => Map(c));
        }

        public async Task<ComentarioResponse?> ObtenerAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;
            return Map(c);
        }

        public async Task<ComentarioResponse> CrearAsync(ComentarioRequest request)
        {
            var c = new Comentario
            {
                Autor = request.Autor,
                Rating = request.Rating,
                Texto = request.Texto,
                FechaCreacion = DateTime.UtcNow,
                Eliminado = false
            };

            var created = await _repo.AddAsync(c);
            return Map(created);
        }

        public async Task<bool> EditarAsync(int id, ComentarioRequest request)
        {
            var existente = await _repo.GetByIdAsync(id);
            if (existente == null) return false;

            existente.Autor = request.Autor;
            existente.Rating = request.Rating;
            existente.Texto = request.Texto;

            await _repo.UpdateAsync(existente);
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existe = await _repo.GetByIdAsync(id);
            if (existe == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }

        // --- Por evento ---
        public async Task<PaginacionResponse<ComentarioResponse>> ListarPorEventoAsync(int eventoId, int limit, int offset)
        {
            var (items, total) = await _repo.GetComentariosPorEventoAsync(eventoId, limit, offset);
            var data = items.Select(c => Map(c)).ToList();

            return new PaginacionResponse<ComentarioResponse>
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

        public async Task<ComentarioResponse> CrearPorEventoAsync(int eventoId, ComentarioRequest req)
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

            var created = await _repo.AddAsync(c);
            return Map(created);
        }

        // --- Mapeo entidad → DTO ---
        private static ComentarioResponse Map(Comentario c) => new ComentarioResponse
        {
            Id = c.Id,
            Autor = c.Autor,
            Rating = c.Rating,
            Texto = c.Texto,
            FechaCreacion = c.FechaCreacion,
            EventoId = c.EventoId
        };
    }
}



