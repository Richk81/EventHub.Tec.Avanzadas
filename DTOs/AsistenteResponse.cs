namespace EventHub.Tec.Avanzadas.DTOs
{
    /// <summary>
    /// Representa un asistente devuelto por la API.
    /// </summary>
    public class AsistenteResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefono { get; set; }
        public int EventoId { get; set; }
        public DateTime RegistradoEn { get; set; }
    }
}

