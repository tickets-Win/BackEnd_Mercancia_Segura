using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaContenedorRequest
    {
        [MaxLength(80)]
        public string? NombreInternoPoliza { get; set; }

        public decimal? PorContenedor { get; set; }
        public decimal? Ferrocarril { get; set; }
        public decimal? Terrestre { get; set; }
        public decimal? CuotaAplicable { get; set; }
        public decimal? ManiobrasRescate { get; set; }
        public decimal? DanioMaterial { get; set; }
        public decimal? Robo { get; set; }
        public decimal? PerdidaParcial { get; set; }
        public decimal? PerdidaTotal { get; set; }

        [MaxLength(250)]
        public string? TrayectosAsegurados { get; set; }

        [MaxLength(250)]
        public string? MedioTransporte { get; set; }

        // Campos financieros (viven aquí, no en Poliza)
        public decimal? PrimaNeta { get; set; }
        public decimal? DerechoPoliza { get; set; }
        public decimal? OtroPrima { get; set; }
        public decimal? IVA { get; set; }
        public decimal? PrimaTotal { get; set; }

        public List<CoberturaRequest> Cobertura { get; set; } = new();
    }
}