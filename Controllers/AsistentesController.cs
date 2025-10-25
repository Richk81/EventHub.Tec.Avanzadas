using EventHub.Tec.Avanzadas.DTOs;
using EventHub.Tec.Avanzadas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace EventHub.Tec.Avanzadas.Controllers
{
    /// <summary>
    /// Controlador para gestionar ASISTENTES.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] //requiere token válido
    public class AsistentesController : ControllerBase
    {
        private readonly IAsistenteService _service;

        public AsistentesController(IAsistenteService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene la lista de todos los asistentes.
        /// </summary>
        /// <param name="limit">Cantidad máxima de registros a devolver.</param>
        /// <param name="offset">Posición inicial de los registros.</param>
        /// <returns>Lista paginada de asistentes.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginacionResponse<AsistenteResponse>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAll([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            // El servicio devuelve la lista paginada
            var result = await _service.ListarPaginadoAsync(limit, offset);
            return Ok(result);
        }


        /// <summary>
        /// Obtiene un asistente por su Id.
        /// </summary>
        /// <param name="id">Id del asistente.</param>
        /// <returns>Detalles del asistente.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.ObtenerAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo asistente.
        /// </summary>
        /// <param name="request">Datos del asistente a crear.</param>
        /// <returns>Resultado de la creación.</returns>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AsistenteRequest request)
        {
            await _service.CrearAsync(request);
            return Ok(); // Si querés, podés devolver un DTO con Id
        }

        /// <summary>
        /// Edita los datos de un asistente existente.
        /// </summary>
        /// <param name="id">Id del asistente a editar.</param>
        /// <param name="request">Nuevos datos del asistente.</param>
        /// <returns>NoContent si se actualiza correctamente.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] AsistenteRequest request)
        {
            await _service.EditarAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Elimina un asistente por su Id.
        /// </summary>
        /// <param name="id">Id del asistente a eliminar.</param>
        /// <returns>NoContent si se elimina correctamente.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.EliminarAsync(id);
            return NoContent();
        }
    }
}

