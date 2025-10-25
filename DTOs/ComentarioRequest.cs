using System.ComponentModel.DataAnnotations;

namespace EventHub.Tec.Avanzadas.DTOs
{
    /// <summary>
    /// Datos necesarios para crear un comentario de un evento.
    /// </summary>
    public class ComentarioRequest
    {
        /// <summary>
        /// Nombre del autor del comentario.
        /// </summary>
        [Required]
        public string Autor { get; set; } = null!;
        /// <summary>
        /// Calificación del evento (por ejemplo de 1 a 5).
        /// </summary>
        public byte? Rating { get; set; } // 1..5
        /// <summary>
        /// Texto del comentario.
        /// </summary>
        public string? Texto { get; set; }
    }
}
