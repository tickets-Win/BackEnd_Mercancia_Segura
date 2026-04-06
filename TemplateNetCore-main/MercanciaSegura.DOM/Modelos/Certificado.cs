using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Certificado")]
    public class Certificado
    {
        [Key]
        [Column("Certificado_ID")]
        public int CertificadoId { get; set; }

        [Column("Cotizacion_ID")]
        public int CotizacionId { get; set; }

        [ForeignKey(nameof(CotizacionId))]
        public Cotizacion? Cotizacion { get; set; }

        [Column("Fecha_Certificado")]
        public DateTime FechaCertificado { get; set; }

    }
}
