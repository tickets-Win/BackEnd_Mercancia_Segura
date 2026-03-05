namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class RiesgoCubiertoResponse
    {
        public int RiesgoCubiertoId { get; set; }

        public string? Nombre { get; set; }

        public int TipoRiesgoId { get; set; }
        public string? tipoRiesgo { get; set; }

        public int PolizaMercanciaId { get; set; }

        public int? AdministracionBienId { get; set; }
        public string? administracionBien { get; set; }
    }
}