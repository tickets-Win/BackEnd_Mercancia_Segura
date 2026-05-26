using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercanciaSegura.DOM.Modelos.Poliza;

namespace MercanciaSegura.DOM.Modelos.Cotizacion
{
    [Table("Bien_Cotizacion")]
    public class BienCotizacion
    {
        [Key]
        [Column("Bien_Cotizacion_ID")]
        public int BienCotizacionId { get; set; }

        [Column("Cotizacion_ID")]
        public int? CotizacionId { get; set; }

        [Column("Administracion_Bien_ID")]
        public int? AdministracionBienId { get; set; }

        [Column("Tipo_Bien")]
        public int? TipoBienId { get; set; }

        [Column("Nombre", TypeName = "nvarchar(max)")]
        public string? Nombre { get; set; }

        [ForeignKey(nameof(CotizacionId))]
        public Cotizacion? Cotizacion { get; set; }

        [ForeignKey(nameof(TipoBienId))]
        public TipoBien? TipoBien { get; set; }

        [ForeignKey(nameof(AdministracionBienId))]
        public AdministracionBien? AdministracionBien { get; set; }
    }
}
