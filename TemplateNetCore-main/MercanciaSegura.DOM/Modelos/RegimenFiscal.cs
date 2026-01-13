using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Regimen_Fiscal")]
    public class RegimenFiscal
    {
        [Key]
        [Column("Regimen_Fiscal_ID")]
        public int RegimenFiscalId { get; set; }

        [Column("Descripcion")]
        [MaxLength(100)]
        public string? Descripcion { get; set; }

        [Column("Tipo_Persona")]
        [MaxLength(1)]
        public string? TipoPersona { get; set; }
    }
}
