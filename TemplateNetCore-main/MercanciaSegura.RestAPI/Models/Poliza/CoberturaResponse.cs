using System.Collections.Generic;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class CoberturaResponse
    {
        public int CoberturaId { get; set; }
        public int PolizaContenedorId { get; set; }
        public string Nombre { get; set; }
    }
}
