using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Riesgo_Cubierto")]
    public class RiesgoCubierto
    {
        [Key]
        [Column("Riesgo_Cubierto_ID")]
        public int RiesgoCubiertoId { get; set; }

        [Column("Nombre", TypeName = "nvarchar(max)")]
        public string? Nombre { get; set; }

        [Column("Tipo_Riesgo_ID")]
        public int TipoRiesgoId { get; set; }

        [Column("Poliza_Mercancia_ID")]
        public int PolizaMercanciaId { get; set; }
    }
}
