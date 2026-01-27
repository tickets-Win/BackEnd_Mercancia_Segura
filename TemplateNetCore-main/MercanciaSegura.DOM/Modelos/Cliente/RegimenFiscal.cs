using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cliente
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

        [Column("Aplica_Moral")]
        public bool AplicaMoral { get; set; }

        [Column("Aplica_Fisica")]
        public bool AplicaFisica { get; set; }

        [Column("Codigo")]
        public int? Codigo { get; set; }
    }
}
