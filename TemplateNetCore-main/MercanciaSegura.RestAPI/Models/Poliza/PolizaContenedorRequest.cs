using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaContenedorRequest
    {
        [MaxLength(250)]
        public string? BienesAsegurados { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PorContenedor { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Ferrocarril { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Terrestre { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? CuotaAplicable { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? DanioMaterial { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Robo { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PerdidaParcial { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PerdidaTotal { get; set; }

        [MaxLength(250)]
        public string? TrayectosAsegurados { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? MedioTransporte { get; set; }
    }
}
