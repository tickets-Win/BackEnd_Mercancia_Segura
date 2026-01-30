using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Correos")]
    public class Correos
    {
        [Key]
        [Column("Correo_ID")]
        public int CorreoId { get; set; }

        [Column("Correo")]
        [MaxLength(200)]
        public string? Correo { get; set; }

        [Column("Tipo_Correo_ID")]
        public int? TipoCorreoId { get; set; }

        [Column("Cliente_ID")]
        public int? ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }

        [ForeignKey(nameof(TipoCorreoId))]
        public TipoCorreo? TipoCorreo { get; set; }

    }
}
