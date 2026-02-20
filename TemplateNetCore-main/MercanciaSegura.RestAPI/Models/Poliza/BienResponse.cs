namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class BienResponse
    {
        public int BienId { get; set; }

        public int? PolizaId { get; set; }

        public int? AdministracionBienId { get; set; }

        public int? TipoBienId { get; set; }
        public string? NombreTipoBien { get; set; }

        public string? Nombre { get; set; }
    }


}
