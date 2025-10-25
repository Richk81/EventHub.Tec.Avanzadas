using System;
using System.Collections.Generic;

namespace EventHub.Tec.Avanzadas.Models;

public partial class Asistente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public int EventoId { get; set; }

    public DateTime RegistradoEn { get; set; }

    public virtual Evento Evento { get; set; } = null!;
}
