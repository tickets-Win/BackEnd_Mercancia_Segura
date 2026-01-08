using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.DOM.Modelos
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("Cliente_ID")]
        public int ClienteId { get; set; }

        [Column("Tipo_Seguro_ID")]
        public int? TipoSeguroId { get; set; }

        [Column("Origen_Cliente_ID")]
        public int? OrigenClienteId { get; set; }

        [Column("Tipo_Cuenta_ID")]
        public int? TipoCuentaId { get; set; }

        [Column("Tipo_Sector_ID")]
        public int? TipoSectorId { get; set; }

        [Column("Regimen_Fiscal_ID")]
        public int? RegimenFiscalId { get; set; }

        [Column("Beneficiario_Preferente_ID")]
        public int? BeneficiarioPreferenteId { get; set; }

        [Column("Tipo_Persona")]
        [StringLength(1)]
        public string? TipoPersona { get; set; }

        [Column("RFC")]
        [MaxLength(13)]
        public string? Rfc { get; set; }

        [Column("RFC_Generico")]
        [MaxLength(13)]
        public string? RfcGenerico { get; set; }

        [Column("Telefono")]
        [MaxLength(13)]
        public string? Telefono { get; set; }

        [Column("Correo_Electronico")]
        [MaxLength(200)]
        public string? CorreoElectronico { get; set; }

        [Column("Nacionalidad")]
        [MaxLength(30)]
        public string? Nacionalidad { get; set; }

        [Column("Calle")]
        [MaxLength(120)]
        public string? Calle { get; set; }

        [Column("Numero_Int")]
        [MaxLength(10)]
        public string? NumeroInt { get; set; }

        [Column("Numero_Ext")]
        [MaxLength(10)]
        public string? NumeroExt { get; set; }

        [Column("Pais")]
        [MaxLength(50)]
        public string? Pais { get; set; }

        [Column("Municipio")]
        [MaxLength(80)]
        public string? Municipio { get; set; }

        [Column("Poblacion")]
        [MaxLength(80)]
        public string? Poblacion { get; set; }

        [Column("Colonia")]
        [MaxLength(60)]
        public string? Colonia { get; set; }

        [Column("Estado")]
        [MaxLength(20)]
        public string? Estado { get; set; }

        [Column("CP")]
        [StringLength(5)]
        public string? Cp { get; set; }

        [Column("Apellido_Paterno")]
        [MaxLength(60)]
        public string? ApellidoPaterno { get; set; }

        [Column("Apellido_Materno")]
        [MaxLength(60)]
        public string? ApellidoMaterno { get; set; }

        [Column("Nombres")]
        [MaxLength(60)]
        public string? Nombres { get; set; }

        [Column("Nombre_Completo")]
        [MaxLength(120)]
        public string? NombreCompleto { get; set; }

        [Column("Cuota_Minima_Internacional", TypeName = "decimal(10,2)")]
        public decimal? CuotaMinimaInternacional { get; set; }

        [Column("Cuota_Minima_Nacional", TypeName = "decimal(10,2)")]
        public decimal? CuotaMinimaNacional { get; set; }

        [Column("Cuota_Aplicable_Internacional", TypeName = "decimal(10,2)")]
        public decimal? CuotaAplicableInternacional { get; set; }

        [Column("Cuota_Aplicable_Nacional", TypeName = "decimal(10,2)")]
        public decimal? CuotaAplicableNacional { get; set; }

        [Column("Fecha_Actualizacion")]
        public DateTime? FechaActualizacion { get; set; }

        [Column("Fecha_Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        //Llaves foraneas

        [ForeignKey(nameof(TipoSeguroId))]
        public TipoSeguro? TipoSeguro { get; set; }

        [ForeignKey(nameof(OrigenClienteId))]
        public OrigenCliente? OrigenCliente { get; set; }

        [ForeignKey(nameof(TipoCuentaId))]
        public TipoCuenta? TipoCuenta { get; set; }

        [ForeignKey(nameof(TipoSectorId))]
        public TipoSector? TipoSector { get; set; }

        [ForeignKey(nameof(RegimenFiscalId))]
        public RegimenFiscal? RegimenFiscal { get; set; }

        [ForeignKey(nameof(BeneficiarioPreferenteId))]
        public BeneficiarioPreferente? BeneficiarioPreferente { get; set; }

    }
}
