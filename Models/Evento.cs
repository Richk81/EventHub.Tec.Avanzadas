using System;
using System.Collections.Generic;

namespace EventHub.Tec.Avanzadas.Models;

public partial class Evento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public DateTime Fecha { get; set; }

    public string? Descripcion { get; set; }

    public DateTime CreadoEn { get; set; }

    public bool Eliminado { get; set; }

    public virtual ICollection<Asistente> Asistentes { get; set; } = new List<Asistente>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
}
