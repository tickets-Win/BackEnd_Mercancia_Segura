using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaContenedorRequest
    {
        [MaxLength(250)]
        public string? BienesAsegurados { get; set; }

        public decimal? PorContenedor { get; set; }
        public decimal? Ferrocarril { get; set; }
        public decimal? Terrestre { get; set; }
        public decimal? CuotaAplicable { get; set; }

        public decimal? DanioMaterial { get; set; }
        public decimal? Robo { get; set; }
        public decimal? PerdidaParcial { get; set; }
        public decimal? PerdidaTotal { get; set; }

        [MaxLength(250)]
        public string? TrayectosAsegurados { get; set; }

        public decimal? MedioTransporte { get; set; }
    }
}
