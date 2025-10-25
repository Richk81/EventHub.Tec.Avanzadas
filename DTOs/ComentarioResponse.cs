using System;

namespace EventHub.Tec.Avanzadas.DTOs
{
    /// <summary>
    /// Representa un comentario de un evento devuelto por la API.
    /// </summary>
    public class ComentarioResponse
    {
        public int Id { get; set; }
        public string Autor { get; set; } = null!;
        public byte? Rating { get; set; }
        public string? Texto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int EventoId { get; set; }
    }
}

