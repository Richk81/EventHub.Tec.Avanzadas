using System;
using System.Collections.Generic;

namespace EventHub.Tec.Avanzadas.Models;

public partial class Comentario
{
    public int Id { get; set; }

    public string Autor { get; set; } = null!;

    public byte? Rating { get; set; }

    public string? Texto { get; set; }

    public int EventoId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Eliminado { get; set; }

    public virtual Evento Evento { get; set; } = null!;
}
