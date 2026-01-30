namespace MercanciaSegura.RestAPI.Models
{
    public class CuotaRequest
    {
        public int TipoCuotaId { get; set; }
        public int? TipoTarifaId { get; set; }
        public decimal Monto { get; set; }
    }
}
