namespace MercanciaSegura.RestAPI.Models.Cliente
{
    public class CuotaResponse
    {
        public int CuotaId { get; set; }

        public int? TipoCuotaId { get; set; }
        public int? TipoTarifaId { get; set; }

        public decimal? Monto { get; set; }

        // Descriptivos (opcionales pero recomendados)
        public string? TipoCuota { get; set; }
        public string? TipoTarifa { get; set; }
    }
}
