using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.DOM.Modelos
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
