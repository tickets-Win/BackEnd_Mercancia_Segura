using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Tipo_Sector")]
    public class TipoSector
    {
        [Key]
        [Column("Tipo_Sector_ID")]
        public int TipoSectorId { get; set; }

        [Column("Tipo")]
        [MaxLength(100)]
        public string? Tipo { get; set; }
    }
}
