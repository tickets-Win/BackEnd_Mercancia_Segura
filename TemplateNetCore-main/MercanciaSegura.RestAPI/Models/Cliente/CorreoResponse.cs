namespace MercanciaSegura.RestAPI.Models.Cliente
{
    public class CorreoResponse
    {
        public int CorreoId { get; set; }
        public string? Correo { get; set; }
        public int? TipoCorreoId { get; set; }

        public string? TipoCorreo { get; set; }
    }
}
