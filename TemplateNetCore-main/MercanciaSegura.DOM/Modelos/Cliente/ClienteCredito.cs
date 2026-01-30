using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
     [Table("Cliente_Creditos")]
    public class ClienteCredito
    {
        [Key]
        [Column("Cliente_Creditos_ID")]
        public int ClienteCreditosId { get; set; }

        [Column("Cliente_ID")]
        public int? ClienteId { get; set; }

        [Column("Dias_De_Credito")]
        public int? DiasDeCredito { get; set; }

        [Column("Metodo_De_Pago")]
        [MaxLength(50)]
        public string? MetodoDePago { get; set; }

        [Column("Numero_Cuenta")]
        [MaxLength(20)]
        public string? NumeroCuenta { get; set; }

        [Column("Limite_De_Credito", TypeName = "decimal(10,2)")]
        public decimal? LimiteDeCredito { get; set; }

        [Column("Dias_De_Pago")]
        [MaxLength(50)]
        public string? DiasDePago { get; set; }

        [Column("Dias_De_Revision")]
        [MaxLength(50)]
        public string? DiasDeRevision { get; set; }

        [Column("Saldo", TypeName = "decimal(10,2)")]
        public decimal? Saldo { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }

    }
}
