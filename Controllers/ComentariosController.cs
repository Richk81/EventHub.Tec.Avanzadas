using EventHub.Tec.Avanzadas.DTOs;
using EventHub.Tec.Avanzadas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventHub.Tec.Avanzadas.Controllers
{
    /// <summary>
    /// Controlador para gestionar COMENTARIOS.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] //requiere token válido
    public class ComentariosController : ControllerBase
    {
        private readonly IComentarioService _service;

        public ComentariosController(IComentarioService service)
        {
            _service = service;
        }


        /// <summary>
        /// Obtiene la lista de todos los comentarios con paginación.
        /// </summary>
        /// <param name="limit">Cantidad máxima de registros a devolver.</param>
        /// <param name="offset">Posición inicial de los registros.</param>
        /// <returns>Lista paginada de comentarios.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginacionResponse<ComentarioResponse>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAll([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            // Llamamos al servicio que ya hace la paginación directamente en el repositorio
            var result = await _service.ListarPorEventoAsync(0, limit, offset); // 0 o algún eventoId si corresponde

            return Ok(result);
        }

        /// <summary>
        /// Obtiene la lista de comentarios de un evento específico con paginación.
        /// </summary>
        [HttpGet("eventos/{eventoId}")]
        [ProducesResponseType(typeof(PaginacionResponse<ComentarioResponse>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetPorEvento(int eventoId, [FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var result = await _service.ListarPorEventoAsync(eventoId, limit, offset);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un comentario por su Id.
        /// </summary>
        /// <param name="id">Id del comentario.</param>
        /// <returns>Detalle del comentario.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.ObtenerAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un comentario asociado a un evento.
        /// </summary>
        /// <param name="eventoId">Id del evento al que pertenece el comentario.</param>
        /// <param name="request">Datos del comentario a crear.</param>
        /// <returns>Comentario creado.</returns>
        [HttpPost("{eventoId}")]
        public async Task<IActionResult> CrearPorEvento(int eventoId, [FromBody] ComentarioRequest request)
        {
            var result = await _service.CrearPorEventoAsync(eventoId, request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Edita un comentario existente.
        /// </summary>
        /// <param name="id">Id del comentario a editar.</param>
        /// <param name="request">Nuevos datos del comentario.</param>
        /// <returns>NoContent si se actualiza correctamente.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] ComentarioRequest request)
        {
            await _service.EditarAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Elimina un comentario por su Id.
        /// </summary>
        /// <param name="id">Id del comentario a eliminar.</param>
        /// <returns>NoContent si se elimina correctamente.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.EliminarAsync(id);
            return NoContent();
        }
    }
}

