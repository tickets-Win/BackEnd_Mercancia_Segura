using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Administracion_Bien")]
    public class AdministracionBien
    {
        [Key]
        [Column("Administracion_Bien_ID")]
        public int AdministracionBienId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }
    }
}
