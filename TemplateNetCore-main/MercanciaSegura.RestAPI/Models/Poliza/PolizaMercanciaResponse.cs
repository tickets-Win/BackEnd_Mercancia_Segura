using System.Collections.Generic;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaMercanciaResponse
    {
        public int PolizaMercanciaId { get; set; }
        public int? PolizaId { get; set; }

        public int AdministracionBienId { get; set; }

        public string? NombreInternoPoliza { get; set; }

        public decimal? TerrestreAereo { get; set; }
        public decimal? Maritimo { get; set; }
        public decimal? PaqueteriaMensajeria { get; set; }

        public string? Deducibles { get; set; }
        public string? Compras { get; set; }
        public string? Ventas { get; set; }
        public string? Maquila { get; set; }
        public string? BienesUsados { get; set; }
        public string? EmbarqueFiliales { get; set; }
        public string? IndemnizacionOtros { get; set; }

        public decimal? Medicamentos { get; set; }
        public decimal? CobreAluminioAcero { get; set; }
        public decimal? MedicamentosControlados { get; set; }
        public decimal? EqContratistas { get; set; }

        public decimal? CuotaGeneralPoliza { get; set; }

        // Campos financieros
        public decimal? PrimaNeta { get; set; }
        public decimal? DerechoPoliza { get; set; }
        public decimal? OtroPrima { get; set; }
        public decimal? IVA { get; set; }
        public decimal? PrimaTotal { get; set; }

        public List<RiesgoCubiertoResponse> RiesgoCubierto { get; set; } = new();
    }
}