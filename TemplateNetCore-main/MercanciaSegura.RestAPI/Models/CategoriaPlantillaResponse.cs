namespace MercanciaSegura.RestAPI.Models
{
    public class CategoriaPlantillaResponse
    {
        public int CategoriaPlantillaId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public bool EsSistema { get; set; }
    }
}
