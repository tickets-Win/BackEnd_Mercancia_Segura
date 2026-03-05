namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class RiesgoCubiertoRequest
    {
        public string? Nombre { get; set; }

        public int TipoRiesgoId { get; set; }

        public int PolizaMercanciaId { get; set; }

        public int? AdministracionBienId { get; set; }
    }
}