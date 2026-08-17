namespace MercanciaSegura.RestAPI.Models
{
    public class PlantillaCorreoRequest
    {
        public int CategoriaPlantillaId { get; set; }

        public string? Nombre { get; set; }

        public string? Asunto { get; set; }

        public string? CuerpoHtml { get; set; }

        /// <summary>Si no viene, la plantilla queda activa.</summary>
        public bool? Activa { get; set; }
    }
}
