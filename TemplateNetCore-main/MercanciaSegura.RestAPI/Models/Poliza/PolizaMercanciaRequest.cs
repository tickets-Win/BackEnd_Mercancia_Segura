using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaMercanciaRequest
    {

        public int AdministracionBienId { get; set; }

        [MaxLength(80)]
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

        // Texto libre que solo se imprime al generar la poliza.
        public string? Especiales { get; set; }

        public string? ExclusionesParticulares { get; set; }

        public string? MedidasDeSeguridad { get; set; }

        public decimal? Medicamentos { get; set; }
        public decimal? CobreAluminioAcero { get; set; }
        public decimal? MedicamentosControlados { get; set; }
        public decimal? EqContratistas { get; set; }

        public decimal? CuotaGeneralPoliza { get; set; }

        // Las cuatro cuotas especiales solo aplican para GMX.
        public bool? NoAplicaCuotasEspeciales { get; set; }

        public decimal? PrimaNeta { get; set; }
        public decimal? DerechoPoliza { get; set; }
        public decimal? OtroPrima { get; set; }
        public decimal? IVA { get; set; }
        public decimal? PrimaTotal { get; set; }

        public List<RiesgoCubiertoRequest> RiesgoCubierto { get; set; } = new();
    }
}