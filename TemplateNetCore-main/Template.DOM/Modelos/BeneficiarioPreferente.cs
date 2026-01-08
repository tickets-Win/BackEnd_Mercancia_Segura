using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.DOM.Modelos
{
    [Table("Beneficiario_Preferente")]
    public class BeneficiarioPreferente
    {
        [Key]
        [Column("Beneficiario_Preferente_ID")]
        public int BeneficiarioPreferenteId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [Column("Domicilio")]
        [MaxLength(200)]
        public string? Domicilio { get; set; }

        [Column("RFC")]
        [MaxLength(13)]
        public string? RFC { get; set; }
    }
}
