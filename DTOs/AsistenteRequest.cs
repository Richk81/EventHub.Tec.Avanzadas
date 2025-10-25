using System.ComponentModel.DataAnnotations;

namespace EventHub.Tec.Avanzadas.DTOs
{
    /// <summary>
    /// Datos necesarios para crear o actualizar un asistente.
    /// </summary>
    public class AsistenteRequest
    {
        /// <summary>
        /// Nombre completo del asistente.
        /// </summary>
        [Required]
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Email del asistente.
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        /// <summary>
        /// Teléfono de contacto del asistente.
        /// </summary>
        public string? Telefono { get; set; }

        [Required]
        public int EventoId { get; set; }
    }
}
