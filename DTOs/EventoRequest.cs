using System;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Tec.Avanzadas.DTOs
{

    /// <summary>
    /// Datos necesarios para crear o actualizar un evento.
    /// </summary>
    public class EventoRequest
    {
        /// <summary>
        /// Nombre del evento.
        /// </summary>
        [Required]
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Ubicación donde se realizará el evento.
        /// </summary>
        public string? Ubicacion { get; set; }

        /// <summary>
        /// Fecha del evento.
        /// </summary>
        [Required]
        public DateTime? Fecha { get; set; }

        /// <summary>
        /// Descripción del evento.
        /// </summary>
        public string? Descripcion { get; set; }
    }
}
