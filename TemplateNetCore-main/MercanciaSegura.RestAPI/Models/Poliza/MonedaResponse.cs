namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class MonedaResponse
    {
        public int MonedaId { get; set; }
        public string Nombre { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal TipoCambioVentanilla { get; set; }
    }
}
