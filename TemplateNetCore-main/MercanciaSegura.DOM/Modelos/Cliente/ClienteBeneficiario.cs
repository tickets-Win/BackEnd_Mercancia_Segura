using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Cliente_Beneficiario")]
    public class ClienteBeneficiario
    {
        [Key]
        [Column("Cliente_Beneficiario_ID")]
        public int ClienteBeneficiarioId { get; set; }

        [Column("Cliente_ID")]
        public int? ClienteId { get; set; }

        [Column("Beneficiario_Preferente_ID")]
        public int? BeneficiarioPreferenteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }

        [ForeignKey(nameof(BeneficiarioPreferenteId))]
        public BeneficiarioPreferente? BeneficiarioPreferente { get; set; }
    }
}
