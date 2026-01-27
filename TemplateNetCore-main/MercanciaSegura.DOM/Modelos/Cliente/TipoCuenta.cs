using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Tipo_Cuenta")]
    public class TipoCuenta
    {
        [Key]
        [Column("Tipo_Cuenta_ID")]
        public int TipoCuentaId { get; set; }

        [Column("Tipo")]
        [MaxLength(100)]
        public string? Tipo { get; set; }
    }
}
