using System;

namespace EventHub.Tec.Avanzadas.DTOs
{
    /// <summary>
    /// Representa un evento devuelto por la API.
    /// </summary>
    public class EventoResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Ubicacion { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
    }
}

