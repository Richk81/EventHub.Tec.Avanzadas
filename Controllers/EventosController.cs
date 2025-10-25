using EventHub.Tec.Avanzadas.DTOs;
using EventHub.Tec.Avanzadas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventHub.Tec.Avanzadas.Controllers
{
    /// <summary>
    /// Controlador para gestionar EVENTOS.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] //requiere token válido
    public class EventosController : ControllerBase
    {
        private readonly IEventosService _service;

        public EventosController(IEventosService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todos los eventos, con filtro opcional por ubicación y paginación.
        /// </summary>
        /// <param name="ubicacion">Opcional. Filtra los eventos por ubicación.</param>
        /// <param name="limit">Cantidad máxima de registros a devolver (default 10).</param>
        /// <param name="offset">Número de registros a saltar (paginación, default 0).</param>
        /// <returns>Lista paginada de eventos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginacionResponse<EventoResponse>), 200)]
        [ProducesResponseType(401)]
        [HttpGet]
        public async Task<IActionResult> GetEventos([FromQuery] string? ubicacion, [FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var result = await _service.ObtenerEventosAsync(ubicacion, limit, offset);
            return Ok(result);
        }


        /// <summary>
        /// Obtiene un evento por su ID.
        /// </summary>
        /// <param name="id">ID del evento.</param>
        /// <returns>Evento encontrado o NotFound si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvento(int id)
        {
            var result = await _service.ObtenerPorIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo evento.
        /// </summary>
        /// <param name="request">Datos del evento a crear.</param>
        /// <returns>Evento creado con su ID.</returns>
        [HttpPost]
        public async Task<IActionResult> CrearEvento([FromBody] EventoRequest request)
        {
            var result = await _service.CrearEventoAsync(request);
            return CreatedAtAction(nameof(GetEvento), new { id = result.Id }, result);
        }

        /// <summary>
        /// Actualiza un evento existente.
        /// </summary>
        /// <param name="id">ID del evento a actualizar.</param>
        /// <param name="request">Datos a actualizar del evento.</param>
        /// <returns>NoContent si se actualizó, NotFound si no existe.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEvento(int id, [FromBody] EventoRequest request)
        {
            var updated = await _service.ActualizarEventoAsync(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Elimina un evento (soft delete).
        /// </summary>
        /// <param name="id">ID del evento a eliminar.</param>
        /// <returns>NoContent si se eliminó, NotFound si no existe.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEvento(int id)
        {
            var deleted = await _service.EliminarEventoAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // --- Sub-resources ---

        // --- Asistentes ---

        /// <summary>
        /// Obtiene los asistentes de un evento con paginación.
        /// </summary>
        /// <param name="eventoId">ID del evento.</param>
        /// <param name="limit">Cantidad máxima de registros a devolver (default 10).</param>
        /// <param name="offset">Número de registros a saltar (default 0).</param>
        /// <returns>Lista paginada de asistentes.</returns>
        [HttpGet("{eventoId}/asistentes")]
        public async Task<IActionResult> GetAsistentes(int eventoId, [FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var result = await _service.ObtenerAsistentesAsync(eventoId, limit, offset);
            return Ok(result);
        }

        /// <summary>
        /// Crea un asistente para un evento.
        /// </summary>
        /// <param name="eventoId">ID del evento.</param>
        /// <param name="request">Datos del asistente a crear.</param>
        /// <returns>Asistente creado.</returns>
        [HttpPost("{eventoId}/asistentes")]
        public async Task<IActionResult> CrearAsistente(int eventoId, [FromBody] AsistenteRequest request)
        {
            var result = await _service.CrearAsistenteAsync(eventoId, request);
            return CreatedAtAction(nameof(GetAsistentes), new { eventoId }, result);
        }

        // --- Comentarios ---

        /// <summary>
        /// Obtiene los comentarios de un evento con paginación.
        /// </summary>
        /// <param name="eventoId">ID del evento.</param>
        /// <param name="limit">Cantidad máxima de registros a devolver (default 10).</param>
        /// <param name="offset">Número de registros a saltar (default 0).</param>
        /// <returns>Lista paginada de comentarios.</returns>
        [HttpGet("{eventoId}/comentarios")]
        public async Task<IActionResult> GetComentarios(int eventoId, [FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var result = await _service.ObtenerComentariosAsync(eventoId, limit, offset);
            return Ok(result);
        }

        /// <summary>
        /// Crea un comentario para un evento.
        /// </summary>
        /// <param name="eventoId">ID del evento.</param>
        /// <param name="request">Datos del comentario a crear.</param>
        /// <returns>Comentario creado.</returns>
        [HttpPost("{eventoId}/comentarios")]
        public async Task<IActionResult> CrearComentario(int eventoId, [FromBody] ComentarioRequest request)
        {
            var result = await _service.CrearComentarioAsync(eventoId, request);
            return CreatedAtAction(nameof(GetComentarios), new { eventoId }, result);
        }
    }
}

