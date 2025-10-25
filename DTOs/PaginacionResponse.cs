using System.Collections.Generic;

namespace EventHub.Tec.Avanzadas.DTOs
{
    public class MetaData
    {
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
    }

    public class PaginacionResponse<T>
    {
        public MetaData Meta { get; set; } = new MetaData();
        public List<T> Data { get; set; } = new List<T>();
    }
}

