using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class CoberturaRequest
    {
        [Required(ErrorMessage = "El RiesgoId es obligatorio")]
        public int RiesgoId { get; set; }

        [Required(ErrorMessage = "El PolizaCoberturaId es obligatorio")]
        public int PolizaCoberturaId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
        public string Nombre { get; set; } = string.Empty;
    }
}
