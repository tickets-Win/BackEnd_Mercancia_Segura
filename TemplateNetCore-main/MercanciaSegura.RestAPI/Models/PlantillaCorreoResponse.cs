using System;

namespace MercanciaSegura.RestAPI.Models
{
    public class PlantillaCorreoResponse
    {
        public int PlantillaCorreoId { get; set; }

        public int CategoriaPlantillaId { get; set; }

        public string? Nombre { get; set; }

        public string? Asunto { get; set; }

        public string? CuerpoHtml { get; set; }

        public bool Activa { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaActualizacion { get; set; }

        // 🔹 Dato enriquecido
        public string? NombreCategoria { get; set; }
    }
}
