namespace MercanciaSegura.RestAPI.Models.Catalogos
{
    public class RegimenFiscalResponse
    {
        public int RegimenFiscalId { get; set; }
        public string? Descripcion { get; set; }
        public bool AplicaMoral { get; set; }
        public bool AplicaFisica { get; set; }
        public int? Codigo { get; set; }
    }
}
